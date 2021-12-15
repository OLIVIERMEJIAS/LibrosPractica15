using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;
using Entidades;

namespace PresentacionWeb
{
    public partial class wfrEditoriales : System.Web.UI.Page
    {
        LNEditorial lnE = new LNEditorial(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!lnE.existeRegistro($"claveEditorial = '{txtClaveEditorial.Text}'"))
                {
                    if (!lnE.existeRegistro($"nombre = '{txtNomEdit.Text}'"))
                    {
                        EEditorial edit = new EEditorial(txtClaveEditorial.Text, txtNomEdit.Text);
                        if(lnE.insertar(edit) != -1)
                        {
                            Session["_exito"] = "Nueva Editorial agregada exitosamente!!";

                        }
                    }
                    else
                        Session["_wrn"] = "Nombre de Editorial repetido, cambielo, por favor.";
                }
                else
                    Session["_wrn"] = "Clave de Editorial repetida, cambiela, por favor.";
            }
            catch (Exception ex)
            {

                Session["_err"] = ex.Message;

            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfrListarEditoriales.aspx");
        }
    }
}