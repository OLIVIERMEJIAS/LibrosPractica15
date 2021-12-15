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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarEditoriales();
            }
        }

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

        private void limpiar()
        {
            txtNomEdit.Text = string.Empty;
            txtNomEdit.Focus();
            cargarEditoriales();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarEditoriales($"nombre like '%{txtNomEdit.Text}%'");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        protected void gdvEditoriales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvEditoriales.PageIndex = e.NewPageIndex;
            btnBuscar_Click(sender, e);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrEditoriales.aspx");
        }
    }
}