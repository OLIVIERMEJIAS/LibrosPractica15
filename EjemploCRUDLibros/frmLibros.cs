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
            libro.Titulo = txtTitulo.Text;
            ln.modificar(libro, "");
            
            //if (textosLlenos()) {

            //    if(libro == null)
            //        libro = new ELibro(txtClaveLibro.Text,
            //            txtTitulo.Text, txtClaveAutor.Text,
            //           categoria, false);
            //    else 

            //    if(!libro.Existe)
            //        insertarLibro();
            //}
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
