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
        /// <summary>
        /// Carga la lista de ejemplares
        /// </summary>
        /// <param name="condicion"></param>
        protected void cargarEjem(string condicion)
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
        /// <summary>
        /// Recibe la clave de la editorial y carga los ejemplares, si los hay
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarEjem($"'{Session["_claveEditorial"]}'");
        }
        /// <summary>
        /// Cambia el índice de las páginas de grid view de ejemplares
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gdvEjemplares_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvEjemplares.PageIndex = e.NewPageIndex;
            cargarEjem($"'{Session["_claveEditorial"]}'");
        }
        /// <summary>
        /// Redirecciona a la lista de editoriales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrListarEditoriales.aspx");
        }
    }
}