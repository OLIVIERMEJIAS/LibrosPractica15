using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using System.Data;

namespace PresentacionWeb
{
    public partial class wfrListarEditoriales : System.Web.UI.Page
    {
        LNEditorial lnE = new LNEditorial(Config.getCadConec);
        LNEjemplar lnEj = new LNEjemplar(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarEditoriales();
            }
        }
        /// <summary>
        /// Carga todoas las editoriales
        /// </summary>
        /// <param name="condicion"></param>
        protected void cargarEditoriales(string condicion = "")
        {
            DataTable dt;
            try
            {
                dt = lnE.listarTodos(condicion);
                if(dt != null)
                {
                    gdvEditoriales.DataSource = dt;
                    gdvEditoriales.DataBind();
                }
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }
        /// <summary>
        /// Limpia el buscador de editoriales y las carga de nuevo
        /// </summary>
        private void limpiar()
        {
            txtNomEdit.Text = string.Empty;
            txtNomEdit.Focus();
            cargarEditoriales();
        }
        /// <summary>
        /// Carga las editoriales con uan condición de filtro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarEditoriales($"nombre like '%{txtNomEdit.Text}%'");
        }
        /// <summary>
        /// Llama al método limpiar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        /// <summary>
        /// Cambia los indices de las páginas del grid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gdvEditoriales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvEditoriales.PageIndex = e.NewPageIndex;
            btnBuscar_Click(sender, e);
        }
        /// <summary>
        /// Redirecciona al formulario editoriales en función de agregar un nuevo registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrEditoriales.aspx");
        }
        /// <summary>
        /// Cuando se da click en eliminar antes de redirijir al
        /// formulario de eliminado, revisa que no esté la editorial asociada
        /// a ejemplares, de estarlo, ni siquiera redirecciona, muestra alerta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkEliminar_Command(object sender, CommandEventArgs e)
        {
            //primero se valida que el registro aún exista
            if (lnE.existeRegistro($"claveEditorial = '{e.CommandArgument}'"))
            {
                //de existir se valida que no hayan ejemplares asociados
                if (!lnEj.existenEjemplares($"'{e.CommandArgument}'"))
                {
                    try
                    {
                        //de no haberlos, los datos de la editorial se pasan al formulario de eliminación para confirmación
                        //mediante dos variable de sessión
                        Session["_nombreEdit"] = lnE.recuperarNombre($"claveEditorial = '{e.CommandArgument}'");
                        Session["_claveEditorial"] = e.CommandArgument;
                        Response.Redirect("wfrEditorialEliminar.aspx");
                    }
                    catch (Exception ex)
                    {

                        Session["_err"] = ex.Message;
                    }
                }
                else
                    //de haber ejemplares asociados
                    Session["_err"] = "Esta Editorial está relacionada a Ejemplares, no puede eliminarse!!";
                
            }
            else
                //de no existir ya la editorial
                Session["_err"] = "Esta editorial no se puede  eliminar, puesto que ya fue eliminada!!";
        }
        /// <summary>
        /// Redirecciona al formulario de lista de ejemplares y envía en una variable de sesión la clave de la editorial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkVerEjemplares_Command(object sender, CommandEventArgs e)
        {
            Session["_claveEditorial"] = e.CommandArgument;
            Response.Redirect("wfrListarEjemplares.aspx");
        }
        /// <summary>
        /// Envía la variable de sesión con la clave de la editorial y redirecciona al formulario de editoriales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkModificar_Command(object sender, CommandEventArgs e)
        {
            Session["_claveEditorial"] = e.CommandArgument;
            Response.Redirect("wfrEditoriales.aspx", false);
        }
    }
}