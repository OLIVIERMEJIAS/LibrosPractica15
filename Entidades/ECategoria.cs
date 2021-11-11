using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class ECategoria
    {
        string claveCategoria;
        string descripcion;
        
        //Propiedades
        public string ClaveCategoria { get; set; }
        public string Descripcion { get; set; }

        //Constructores
        public ECategoria() {
            ClaveCategoria = string.Empty;
            Descripcion = string.Empty;
        }
        public ECategoria(string clave, string desc) {
            ClaveCategoria = clave;
            Descripcion = desc;
        }
    }
}
