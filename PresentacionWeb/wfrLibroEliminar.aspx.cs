using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using LogicaNegocio;

namespace PresentacionWeb
{
    public partial class wfrLibroEliminar : System.Web.UI.Page
    {
        ELibro libro;
        LNLibro lnL = new LNLibro(Config.getCadConec);
        protected void Page_Load(object sender, EventArgs e)
        {   
            //todo try
            if(Session["_claveLibro"] != null)
            {
                recuperarLibro(Session["_clavLibro"].ToString());
            }
        }

        private void recuperarLibro(string claveLibro)
        {
            string condicion = $"claveLibro = '{claveLibro}'";
            
        }
    }
}