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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["_claveEditorial"] != null)
                {
                    DataTable dt;
                    dt = lnE.listarTodos($"claveEditorial = '{Session["_claveEditorial"]}'");
                    if (dt != null)
                    {
                        txtClaveEditorial.Text = dt.Rows[0][0].ToString();
                        txtNomEdit.Text = dt.Rows[0][1].ToString();
                        HttpCookie cookie = new HttpCookie("MyCookie");
                        cookie["_claveEditorial"] = dt.Rows[0][0].ToString();
                        cookie["_nombre"] = dt.Rows[0][1].ToString();
                        Response.Cookies.Add(cookie);
                    }
                    else
                    {
                        Session["_wrn"] = "Ya no existe esta editorial, no es posible modificarla!";
                    }
                }
            }
        }

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
            else
            {
                modificar();
            }
        }

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
       
   
protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["_claveEditorial"] != null) 
                {
                    bool bCE = false;
                    bool bNom = false;
                    if (hayCambios(ref bCE, ref bNom))
                    {
                        if (bCE)
                        {
                            if (!lnE.existeRegistro($"claveEditorial = '{txtClaveEditorial.Text}'"))
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
                                else
                                {
                                    modificar();
                                }
                            }
                            else
                                Session["_wrn"] = "Ya existe una Editorial con esta clave, por favor cambiela";

                        }
                        else
                        {
                            verificarNombreYModificar(bNom);
                        }
                    }
                    else
                        Session["_wrn"] = "No hay cambios para modificar la editorial";
                }
                else
                {
                    if (!lnE.existeRegistro($"claveEditorial = '{txtClaveEditorial.Text}'"))
                    {
                        if (!lnE.existeRegistro($"nombre = '{txtNomEdit.Text}'"))
                        {
                            EEditorial edit = new EEditorial(txtClaveEditorial.Text, txtNomEdit.Text);
                            if (lnE.insertar(edit) != -1)
                            {
                                Session["_exito"] = "Nueva Editorial agregada exitosamente!!";
                                txtClaveEditorial.Text = string.Empty;
                                txtNomEdit.Text = string.Empty;
                            }
                        }
                        else
                            Session["_wrn"] = "Nombre de Editorial repetido, cambielo, por favor.";
                    }
                    else
                        Session["_wrn"] = "Clave de Editorial repetida, cambiela, por favor.";
                }
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;

            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            if (Session["_claveEditorial"] != null)
                Session["_claveEditorial"] = null;
            Response.Redirect("wfrListarEditoriales.aspx");
        }
    }
}