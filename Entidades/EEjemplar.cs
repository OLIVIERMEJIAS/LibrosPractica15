using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class EEjemplar
    {
      
        public  string ClaveEjemplar { get; set; }
        public string ClaveLibro { get; set; }
        public string ClaveCondicion { get; set; }
        public string ClaveEstado { get; set; }
        public string Edicion { get; set; }
        public string ClaveEditorial { get; set; }
        public int NumeroPaginas { get; set; }

        public EEjemplar()
        {
            ClaveEjemplar = string.Empty;
            ClaveLibro = string.Empty;
            ClaveCondicion = string.Empty;
            ClaveEstado = string.Empty;
            Edicion = string.Empty;
            ClaveEditorial = string.Empty;
            NumeroPaginas = 0;
         }

        public EEjemplar(string claveEj, string claveL, string claveCond, string claveEst,
            string edicion, string claveEdit, int num)
        {
            ClaveEjemplar = claveEj;
            ClaveLibro = claveL;
            ClaveCondicion = claveCond;
            ClaveEstado = claveEst;
            Edicion = edicion;
            ClaveEditorial = claveEdit;
            NumeroPaginas = num;
        }
    }
}
