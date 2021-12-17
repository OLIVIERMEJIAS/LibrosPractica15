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
        protected void Page_Load(object sender, EventArgs e)
        {
            txtClaveEditorial.Text += Session["_claveEditorial"];
            txtNombreEdit.Text += Session["_nombreEdit"];
        }

        protected void limpiar()
        {
            txtClaveEditorial.Text = "";
            txtNombreEdit.Text = "";
            Session.Remove("_claveEditorial");
            Session.Remove("_nombreEdit");

        }

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