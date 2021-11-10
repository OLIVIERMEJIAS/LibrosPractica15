using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class ELibro
    {
        #region Atributos
        string claveLibro;
        string titulo;
        string claveAutor;
        ECategoria claveCategoria;
        bool existe;

        #endregion

        #region Propiedades
        public string ClaveLibro { get; set; }
        public string Titulo { get; set; }
        public string ClaveAutor { get; set; }
        public ECategoria Clavecategoria { get; set; }
        public bool Existe { get; set; }

        #endregion

        #region Constructores
        public ELibro() {
            claveLibro = string.Empty;
            titulo = string.Empty;
            claveAutor = string.Empty;
            claveCategoria = new ECategoria();
            existe = false;
        }
        public ELibro(string claLibro, string tit, string claAutor, ECategoria cate, bool ext) {
            claveLibro = claLibro;
            titulo = tit;
            claveAutor = claAutor;
            claveCategoria = cate;
            existe = ext;
        }
        #endregion
    }
}
