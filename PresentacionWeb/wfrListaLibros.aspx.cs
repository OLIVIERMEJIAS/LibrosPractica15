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
            txtTitulo.Text = string.Empty;
            txtTitulo.Focus();

            cargarDataGrid("");
        }

       
    }
}