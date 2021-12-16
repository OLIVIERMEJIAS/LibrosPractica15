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
    public partial class wfrListarEjemplares : System.Web.UI.Page
    {
        LNEjemplar lnE = new LNEjemplar(Config.getCadConec);
        protected void cargarLibros(string condicion)
        {
            DataTable dt;
            try
            {
                dt = lnE.listarTodos(condicion);
                gdvEjemplares.DataSource = dt;
                gdvEjemplares.DataBind();
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarLibros($"'{Session["_claveEditorial"]}'");
        }

        protected void gdvEjemplares_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvEjemplares.PageIndex = e.NewPageIndex;
            cargarLibros($"'{Session["_claveEditorial"]}'");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrListarEditoriales.aspx");
        }
    }
}