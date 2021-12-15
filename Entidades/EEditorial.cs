using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class EEditorial
    {
        public string ClaveEditorial { get; set; }
        public string Nombre { get; set; }

        public EEditorial()
        {
            ClaveEditorial = "";
            Nombre = "";
        }

        public EEditorial(string clave, string nom)
        {
            ClaveEditorial = clave;
            Nombre = nom;
        }
    }
}
