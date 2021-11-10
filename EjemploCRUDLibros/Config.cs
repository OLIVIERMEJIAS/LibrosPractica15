using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploCRUDLibros
{
    public static class Config
    {
       public static string getCadConexion {
            get { return Properties.Settings.Default.CadConexion; }
        }

    }
}
