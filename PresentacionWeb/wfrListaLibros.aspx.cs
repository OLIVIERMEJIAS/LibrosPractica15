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
    public partial class wfrListaLibros : System.Web.UI.Page
    {
        LNLibro lnL = new LNLibro(Config.getCadConec); 
        private void cargarDataGrid(string condicion = "")
        {
            DataTable dt;
            try
            {
                dt = lnL.listarTodos(condicion, true);
                if(dt != null)
                {
                    grdLibros.DataSource = dt;
                    grdLibros.DataBind();
                }
                else
                {
                    Session["_wrn"] = "No se encontraron Libros";
                }
            }
            catch (Exception ex)
            {

                Session["_err"] = $"Error: {ex.Message}";
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                limpiar();
            }
        }

        private void limpiar()
        {
            txtTitulo.Text = string.Empty;
            txtTitulo.Focus();
            cargarDataGrid("");
        }

        protected void lnkModificar_Command(object sender, CommandEventArgs e)
        {
            //Session["_wrn"] = e.CommandArgument.ToString();
        }

        protected void grdLibros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdLibros.PageIndex = e.NewPageIndex;
            cargarDataGrid();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarDataGrid($"titulo like '%{txtTitulo.Text}%'");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrLibros.aspx");
        }
    }
}