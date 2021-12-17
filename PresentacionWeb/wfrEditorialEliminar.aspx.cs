using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;

namespace PresentacionWeb
{
    public partial class wfrEditorialEliminar : System.Web.UI.Page
    {
        LNEditorial lnE = new LNEditorial(Config.getCadConec);
        /// <summary>
        /// Lllena los label con la información de las variables 
        /// de sessión con los datos de la lista de editoriales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            lblClaveEditorial.Text += Session["_claveEditorial"];
            lblNombreEdit.Text += Session["_nombreEdit"];
        }

        protected void limpiar()
        {
            lblClaveEditorial.Text = "";
            lblNombreEdit.Text = "";
            Session.Remove("_claveEditorial");
            Session.Remove("_nombreEdit");

        }
        /// <summary>
        /// Eliminación de una editorial, cuando ya pasó por la validación de si tiene
        /// ejemplares asociados en el formulario de la lsta de editoriales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                if (lnE.eliminar($"claveEditorial = '{Session["_claveEditorial"]}'") != -1)
                {
                    Session["_exito"] = "Editorial Eliminada con Éxito!!!";
                    Response.Redirect("wfrListarEditoriales.aspx",false);
                }
                else
                {
                    //si la editorial ya no existe
                    Session["_err"] = "No se pude borrar la Editorial, esta ya no existe!";
                    Response.Redirect("wfrListarEditoriales.aspx",false);
                }
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
                Response.Redirect("wfrListarEditoriales.aspx",false);
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrListarEditoriales.aspx",false);
        }
    }
}