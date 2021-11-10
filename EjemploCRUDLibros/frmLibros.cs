using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using LogicaNegocio;

namespace EjemploCRUDLibros
{
    public partial class frmLibros : Form
    {

        ECategoria categoria = new ECategoria("C0001", "Comic");

        public frmLibros()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiaTextos();
        }

        private void limpiaTextos() {
            txtClaveLibro.Text = string.Empty;
            txtTitulo.Text = string.Empty;
            txtClaveAutor.Text = "A0023";
            txtCategoria.Text = categoria.ClaveCategoria;
            txtClaveLibro.Focus();
        }

        private bool textosLlenos() {
            bool result=false;
            string msj = "";

            if (string.IsNullOrEmpty(txtClaveLibro.Text))
            {
                msj = "Debe agregar una Clave de Libro";
                txtClaveLibro.Focus();
            }
            else if (string.IsNullOrEmpty(txtTitulo.Text))
            {
                msj = "De escribir un Título";
                txtTitulo.Focus();
            }
            else if (string.IsNullOrEmpty(txtClaveAutor.Text))
            {
                msj = "Debe agregar la clave del Autor";
                txtClaveAutor.Focus();
            }
            else if (string.IsNullOrEmpty(txtCategoria.Text))
            {
                msj = "Agregue la Clave de Categoría";
                txtCategoria.Focus();
            }
            else
                result = true;
            
            if(!result)
                MessageBox.Show(msj, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return result;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ELibro libro;
            LNLibro ln = new LNLibro();

            if (textosLlenos()) {
                libro = new ELibro(txtClaveLibro.Text,
                    txtTitulo.Text, txtClaveAutor.Text,
                   categoria, false);

                try
                {
                    if (!ln.libroRepetido(libro)) {
                        if (!ln.claveLibroRepetida(libro.ClaveLibro)) {
                            //TODO: Agregar acceso a capa de Lógica
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                }
            }
        }
    }
}
