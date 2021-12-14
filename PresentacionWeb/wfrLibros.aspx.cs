using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Entidades;
using LogicaNegocio;

namespace PresentacionWeb
{
    public partial class wfrLibros : System.Web.UI.Page
    {
        LNLibro lnL = new LNLibro(Config.getCadConec);
        LNAutor lnA = new LNAutor(Config.getCadConec);
        LNCategoria lnC = new LNCategoria(Config.getCadConec);
        ELibro libro;
        private void cargarCategorias(string condicion = "")
        {
            DataTable dt;
            try
            {
                dt = lnC.ListarRegistros(condicion);
                if(dt != null)
                {
                    gdvCategorias.DataSource = dt;
                    gdvCategorias.DataBind();
                }
            }
            catch (Exception ex)
            {

                Session["_err"] = $"Error: {ex.Message}";
            }
        }
        private void cargarAutores(string condicion = "")
        {
            DataTable dt;
            try
            {
                dt = lnA.ListarRegistros(condicion);
                if(dt != null)
                {
                    grdAutores.DataSource = dt;
                    grdAutores.DataBind();
                }
            }
            catch (Exception ex)
            {

                Session["_err"] = $"Error: {ex.Message}";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarAutores();
            cargarCategorias();
        }

        protected void btnBuscarAutor_Click(object sender, EventArgs e)
        {
            string javaScript = "AbrirModal();";
            cargarAutores($"nombre like '%{txtNombreAutor.Text}%'");
            ScriptManager.RegisterStartupScript(this,this.GetType(),"script",javaScript,true);

        }

        protected void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            string javaScript = "AbrirModalCat();";
            cargarCategorias($"descripcion like '%{txtCategoria.Text}%'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
        }
    }
}