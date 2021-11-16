using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class ELibro
    {
        #region Atributos
        private string claveLibro;
        private string titulo;
        private string claveAutor;
        private ECategoria claveCategoria;
        private bool existe;

        #endregion

        #region Propiedades
        //
        public string ClaveLibro { get => claveLibro; set => claveLibro = value; }
        public string Titulo { 
            get { return titulo; }
            set { titulo = value; } 
        }
        public string ClaveAutor { get; set; }
        public ECategoria Clavecategoria { get; set; }
        public bool Existe { get; set; }

        #endregion

        #region Constructores
        public ELibro() {
            ClaveLibro = string.Empty;
            Titulo = string.Empty;
            ClaveAutor = string.Empty;
            Clavecategoria = new ECategoria();
            Existe = false;
        }
        public ELibro(string claLibro, string tit, string claAutor, ECategoria cate, bool ext) {
            ClaveLibro = claLibro;
            Titulo = tit;
            ClaveAutor = claAutor;
            Clavecategoria = cate;
            Existe = ext;
        }
        #endregion
    }
}
