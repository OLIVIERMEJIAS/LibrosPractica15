using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using LogicaNegocio;
using System.Data;

namespace PresentacionWeb
{
    public partial class wfrLibroEliminar : System.Web.UI.Page
    {
        ELibro libro;
        LNLibro lnL = new LNLibro(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {
            //todo try
            try
            {
                if (Session["_claveLibro"] != null)
                {
                    recuperarLibro(Session["_claveLibro"].ToString());
                }
                else
                {
                    Session["_wrn"] = "No se ha seleccionado un libro que eliminar";
                    btnEliminar.Enabled = false;

                }
            }
            catch (Exception ex)
            {

                Session["_err"] = $"Error: {ex.Message}";
            }
        }

        private void recuperarLibro(string claveLibro)
        {
            DataTable dt;
            string condicion = $"Clave = '{claveLibro}'";

            dt = lnL.listarTodos(condicion, true);
            if (dt != null)
            {
                ViewState["_titulo"] = dt.Rows[0][1];
                ViewState["_autor"] = dt.Rows[0][2];
                ViewState["_categoria"] = dt.Rows[0][3];
            }
            else
            {
                Session["_wrn"] = "El libro seleccionado ya no está en la base de datos";
                btnEliminar.Enabled = false;

            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Session.Remove("_claveLibro");
            Session.Remove("_wrn");
            Session.Remove("_err");
            Response.Redirect("wfrListaLibros.aspx",false);

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int result = -1;
            if(Session["_claveLibro"] != null)
            {
                try
                {
                    result = lnL.eliminar(Session["_claveLibro"].ToString());
                    if(result > 0)
                    {
                        Session.Remove("_err");
                        Session.Remove("_wrn");
                        Session["_exito"] = "El libro se ha eliminado de forma exitosa";
                        Response.Redirect("wfrListaLibros.aspx", false);

                    }
                    else
                        Session["_wrn"] = "El libro seleccionado ya no está en la base de datos";

                }
                catch (Exception ex)
                {

                    Session["_err"] = ex.Message;
                }
            }
        }
    }
}