using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Entidades;
using System.Data;

namespace PresentacionWeb
{
    public partial class wfrEditoriales : System.Web.UI.Page
    {
        LNEditorial lnE = new LNEditorial(Config.getCadConec);
        /// <summary>
        /// Instrucciones para cargar lel formulario cuando es por primera vez
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //las instrucciones se basan en la existencia de la variable de sesión
                //claveEditorial, la cual solo existe cuando se va a modificar una Editorial
                if (Session["_claveEditorial"] != null)
                {
                    //se lista la editorial con una codición el el método de recuperación
                    DataTable dt;
                    dt = lnE.listarTodos($"claveEditorial = '{Session["_claveEditorial"]}'");
                    if (dt != null)
                    {
                        //si existe el registro este rellena los cuadros de texto, y llena una cookie,
                        //para cuando se haga PostBac no se borren los datos del registro de la edifotiral que se va a modificar
                        txtClaveEditorial.Text = dt.Rows[0][0].ToString();
                        txtNomEdit.Text = dt.Rows[0][1].ToString();
                        HttpCookie cookie = new HttpCookie("MyCookie");
                        cookie["_claveEditorial"] = dt.Rows[0][0].ToString();
                        cookie["_nombre"] = dt.Rows[0][1].ToString();
                        Response.Cookies.Add(cookie);
                    }
                    else
                    {
                        //si la editorial ya no existe
                        Session["_wrn"] = "Ya no existe esta editorial, no es posible modificarla!";
                    }
                }
            }
        }
        /// <summary>
        /// Permite saber si hubo cambios en el formulario
        /// se tomando dos variable por referencia, para validar cambios y el método en sí también devuelve un bool
        /// se comparan los cuadros de textos con los datos que vienen de la cookie
        /// , creada anteriormente en el mismo formulario
        /// </summary>
        /// <param name="bCE"></param>
        /// <param name="bNom"></param>
        /// <returns></returns>
        protected bool hayCambios(ref bool bCE, ref bool bNom)
        {
            if(txtClaveEditorial.Text != Request.Cookies["MyCookie"]["_claveEditorial"])
                bCE = true;
            if (txtNomEdit.Text != Request.Cookies["MyCookie"]["_nombre"])
                bNom = true;
            if(!bCE && !bNom)
                return false;
            else
                return true;
        }
        /// <summary>
        /// Permite verificar si el nombre de la editorial ya existe
        /// , al saber que hubo cambios en el nombre de esta, de no ser así se modifica llamando a un método
        /// de no existir, si no hay cambios también se modifica ya que había otras validaciones anteriores
        /// </summary>
        /// <param name="bNom"></param>
        protected void verificarNombreYModificar(bool bNom)
        {
            if (bNom)
            {
                if (!lnE.existeRegistro($"nombre = '{txtNomEdit.Text}'"))
                {
                    modificar();
                }
                else
                    Session["_wrn"] = "Ya existe una Editorial con este nombre, por favor cambielo";
            }
            
        }
        /// <summary>
        /// Permite modificar el registro de la editorial, asi como manda un mensaje
        /// con la variable de sesión y redirijir a la lista de editoriales
        /// </summary>
        protected void modificar()
        {
            try
            {
                 EEditorial edit = new EEditorial(txtClaveEditorial.Text, txtNomEdit.Text);
                if (lnE.modificar(edit,Session["_claveEditorial"].ToString()) != -1)
                {
                    Session["_exito"] = "Editorial modificada exitosamente!!";
                    Response.Redirect("wfrListarEditoriales.aspx", false);
                }
            }
            catch (Exception ex)
            {

                Session["_err"] = $"Error: {ex.Message}";
            }


        }
       
        /// <summary>
        /// Permite guardar un registro nuevo o modificar uno existente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //evalúa que existe la claveEditorial,
                //que es una variabloe de sesión que solo existe cuando se desea modificar
                if (Session["_claveEditorial"] != null) 
                {
                    //variable bool para hacer la revisión de cambios en los cuadros de texto
                    bool bCE = false;
                    bool bNom = false;
                    //si hubo cambios
                    if (hayCambios(ref bCE, ref bNom))
                    {
                        //si hubo cambios en la clave de la editorial
                        if (bCE)
                        {
                            //se revisa que la clave no este repetida
                            if (!lnE.existeRegistro($"claveEditorial = '{txtClaveEditorial.Text}'"))
                            {
                                //si hubo cambios en el nombre se valida que no este repetido
                                if (bNom)
                                {
                                    if (!lnE.existeRegistro($"nombre = '{txtNomEdit.Text}'"))
                                    {
                                        //de no estar ya se puede modificar
                                        modificar();
                                    }
                                    //al estar repetido el nombre
                                    else
                                        Session["_wrn"] = "Ya existe una Editorial con este nombre, por favor cambielo";
                                }
                                else
                                //de no haber cambios en el nombre, pero la clave como ya había pasado validación se modifica
                                {
                                    modificar();
                                }
                            }
                            else
                                //al estar repetida la clave
                                Session["_wrn"] = "Ya existe una Editorial con esta clave, por favor cambiela";

                        }
                        //al no haber cambios en la clave
                        else
                        {
                            //se verifican cambios en el nombre y si está repetido, de no estarlo se modifica 
                            verificarNombreYModificar(bNom);
                        }
                    }
                    //al no haber ningún cambio
                    else
                        Session["_wrn"] = "No hay cambios para modificar la editorial";
                }
                else
                //al ser un nuevo registro
                {
                    //se valida la existencia de la clave de la editorial
                    if (!lnE.existeRegistro($"claveEditorial = '{txtClaveEditorial.Text}'"))
                    {
                        //se valida la existencia del nombre de la editorial
                        if (!lnE.existeRegistro($"nombre = '{txtNomEdit.Text}'"))
                        {
                            //se creo objeto tipo editorial y se inserta
                            EEditorial edit = new EEditorial(txtClaveEditorial.Text, txtNomEdit.Text);
                            if (lnE.insertar(edit) != -1)
                            {
                                Session["_exito"] = "Nueva Editorial agregada exitosamente!!";
                                //notificación y se limpiar espacios
                                txtClaveEditorial.Text = string.Empty;
                                txtNomEdit.Text = string.Empty;
                            }
                        }
                        //al estar repetido el nombre de la editorial
                        else
                            Session["_wrn"] = "Nombre de Editorial repetido, cambielo, por favor.";
                    }
                    //al estar repetida la clave de la editorial
                    else
                        Session["_wrn"] = "Clave de Editorial repetida, cambiela, por favor.";
                }
            }
            //tratamiento de excepción
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;

            }
        }
        /// <summary>
        /// Método que limpiar la variable de sesión claveEditorial si existe y redirecciona a la lista de editoriales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            if (Session["_claveEditorial"] != null)
                Session["_claveEditorial"] = null;
            Response.Redirect("wfrListarEditoriales.aspx");
        }
    }
}