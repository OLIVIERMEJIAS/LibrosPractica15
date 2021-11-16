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
        LNLibro ln = new LNLibro(Config.getCadConexion);
        ELibro libro;


        public frmLibros()
        {
            InitializeComponent();
        }
        #region Metodos
        private void limpiaTextos()
        {
            txtClaveLibro.Text = string.Empty;
            txtTitulo.Text = string.Empty;
            txtClaveAutor.Clear();
            txtCategoria.Clear();
            txtClaveLibro.Focus();

            libro = null;

            btnEliminar.Enabled = false;

            llenarDGV();

        }
        //******************************************

        private void llenarDGV(string condicion="") {
            LNLibro ln = new LNLibro(Config.getCadConexion);
            DataSet ds;

            try
            {
                ds = ln.listarTodos(condicion);
                //ds = ln.listarTodos("titulo like %amor%");

                dgvLibros.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }

            dgvLibros.Columns[0].HeaderText = "Clave de Libro";
            dgvLibros.Columns[1].HeaderText = "Título";
            dgvLibros.Columns[2].HeaderText = "Clave Autor";
            dgvLibros.Columns[3].HeaderText = "Clave Categoría";

            //dgvLibros.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);
        }

        //**************************************
        private void mensajeError(Exception ex) {
            MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiaTextos();
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
            string clave = "", titulo = "", autor = "";

            if (textosLlenos())
            {
                //Este IF termina con un LIBRO lleno!
                if (libro == null)
                    libro = new ELibro(txtClaveLibro.Text,
                        txtTitulo.Text, txtClaveAutor.Text,
                       categoria, false);
                else
                    if (hayCambios(ref clave, ref titulo, ref autor)) {
                    {
                        libro.ClaveLibro = txtClaveLibro.Text;
                        libro.Titulo = txtTitulo.Text;
                        libro.ClaveAutor = txtClaveAutor.Text;
                        libro.Clavecategoria.ClaveCategoria = txtCategoria.Text;
                    }
                }

                if (!libro.Existe) //Insert
                    insertarLibro();
                else
                { //Update
                    if (string.IsNullOrEmpty(clave) == false)
                    {
                        if (ln.claveLibroRepetida(libro.ClaveLibro) == false)
                        {
                            cambiosTituloAutor(clave, autor, titulo);
                        }
                        else
                        {
                            MessageBox.Show("La nueva clave de libro ya está en uso");
                            txtClaveLibro.Focus();
                        }
                    }
                    else {
                        cambiosTituloAutor("", autor, titulo);
                    }

                }
            }
        }

        private void cambiosTituloAutor(string clave, string autor, string titulo)
        {
            if (!string.IsNullOrEmpty(titulo) || !string.IsNullOrEmpty(autor))
            {
                if (ln.libroRepetido(libro) == false)
                {
                    hacerModificacion(clave);
                }
                else
                {
                    MessageBox.Show("No se puede actualizar porque el título y autor ya existen");
                    limpiaTextos();
                }
            }
            else
            {
                hacerModificacion(clave);
            }
        }

        private void hacerModificacion(string clave)
        {
            if (ln.modificar(libro, clave) > 0)
                MessageBox.Show("Actualización realizada");
            else
                MessageBox.Show("No se pudo modificar");

            limpiaTextos();
        }

        private bool hayCambios(ref string clave, ref string titulo, ref string autor)
        {
            bool result = false;

            if (txtClaveLibro.Text != libro.ClaveLibro) {
                result = true;
                clave = libro.ClaveLibro;//Clave Vieja
            }

            if (txtTitulo.Text != libro.Titulo) {
                result = true;
                titulo = libro.Titulo; //Título viejo
            }

            if (txtClaveAutor.Text != libro.ClaveAutor) {
                result = true;
                autor = libro.ClaveAutor;
            }

            return result;
        }

        private void insertarLibro()
        {
            try
            {
                if (!ln.libroRepetido(libro))
                {
                    if (!ln.claveLibroRepetida(libro.ClaveLibro))
                    {
                        if (ln.insertar(libro) > 0)
                        {
                            MessageBox.Show("Guardado con éxito!");
                            limpiaTextos();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Esa Clave de Libro ya está" +
                            " asignada a otro libro");
                        txtClaveLibro.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Ese título ya existe para el " +
                        "autor indicado");
                    txtTitulo.Focus();
                }
            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }
        }

        private void frmLibros_Load(object sender, EventArgs e)
        {
            llenarDGV();
        }

        private void dgvLibros_DoubleClick(object sender, EventArgs e)
        {
            int fila = dgvLibros.CurrentRow.Index;            
            string clave = dgvLibros[0, fila].Value.ToString();
            string condicion = $"claveLibro='{clave}'";

            try
            {
                libro = ln.buscarRegistro(condicion);

                if (libro != null) {
                    libro.Existe = true;

                    txtClaveLibro.Text = libro.ClaveLibro;
                    txtTitulo.Text = libro.Titulo;
                    txtClaveAutor.Text = libro.ClaveAutor;
                    txtCategoria.Text = libro.Clavecategoria.ClaveCategoria;

                    btnEliminar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                mensajeError(ex);
            }
        }

        private void dgvLibros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resp;
            int resultado;
            string msj;

            if (libro != null && libro.Existe) {
                resp = MessageBox.Show($"Confirma que desea Eliminar el libro {libro.Titulo} " +
                    $"con código {libro.ClaveLibro}?","Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resp == DialogResult.Yes)
                {
                    msj = ln.eliminarProcedure(libro);
                    MessageBox.Show(msj);
                    limpiaTextos();

                    //resultado = ln.eliminar(libro);
                    //if (resultado > 0)
                    //{
                    //    MessageBox.Show("Libro Eliminado", "Eeeeexito!");
                    //    limpiaTextos();
                    //}
                    //else if (ln.eliminar(libro) == -1)
                    //    MessageBox.Show("Se presentó une error", "ERROR");
                }
                else
                    limpiaTextos();
            }
        }
    }
}
