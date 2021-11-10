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
            claveCategoria = string.Empty;
            descripcion = string.Empty;
        }
        public ECategoria(string clave, string desc) {
            claveCategoria = clave;
            descripcion = desc;
        }
    }
}
