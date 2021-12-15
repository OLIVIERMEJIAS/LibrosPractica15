using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Entidades;
using LogicaNegocio;

namespace PresentacionWeb
{
    public partial class wfrLibros : System.Web.UI.Page
    {
        LNLibro lnL = new LNLibro(Config.getCadConec);
        LNAutor lnA = new LNAutor(Config.getCadConec);
        LNCategoria lnC = new LNCategoria(Config.getCadConec);
        ELibro libro;

        private void limpiar()
        {
            txtClaveLibro.Text = string.Empty;
            txtTitulo.Text = string.Empty;
            txtClaveAutor.Text = string.Empty;
            txtClaveCategoria.Text = string.Empty;
            txtNombreAutor.Text = string.Empty;
            txtCategoria.Text = string.Empty;
        }
        private void cargarCategorias(string condicion = "")
        {
            DataTable dt;
            try
            {
                dt = lnC.ListarRegistros(condicion);
                if(dt != null)
                {
                    gdvCategorias.DataSource = dt;
                    gdvCategorias.DataBind();
                }
            }
            catch (Exception ex)
            {

                Session["_err"] = $"Error: {ex.Message}";
            }
        }
        private void cargarAutores(string condicion = "")
        {
            DataTable dt;
            try
            {
                dt = lnA.ListarRegistros(condicion);
                if(dt != null)
                {
                    grdAutores.DataSource = dt;
                    grdAutores.DataBind();
                }
            }
            catch (Exception ex)
            {

                Session["_err"] = $"Error: {ex.Message}";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarAutores();
            cargarCategorias();
        }

        protected void btnBuscarAutor_Click(object sender, EventArgs e)
        {
            string javaScript = "AbrirModal();";
            cargarAutores($"nombre like '%{txtNombreAutor.Text}%'");
            ScriptManager.RegisterStartupScript(this,this.GetType(),"script",javaScript,true);
        }

        protected void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            string javaScript = "AbrirModalCat();";
            cargarCategorias($"descripcion like '%{txtCategoria.Text}%'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
        }

        protected void grdAutores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAutores.PageIndex = e.NewPageIndex;
            btnBuscarAutor_Click(sender, e);
        }

        protected void gdvCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvCategorias.PageIndex = e.NewPageIndex;
            btnBuscarCategoria_Click(sender, e);
        }

        protected void lnkSeleccionarAutor_Command(object sender, CommandEventArgs e)
        {
            recuperarAutor(e.CommandArgument.ToString());
        }

        private void recuperarAutor(string claveAutor)
        {
            string condicion = $"claveAutor = '{claveAutor}'";
            EAutor autor;
            try
            {
                autor = lnA.BuscarRegistro(condicion);
                if(autor != null)
                {
                    txtIdAutor.Text = autor.ClaveAutor;
                    txtClaveAutor.Text = autor.ApPaterno + " " + autor.Nombre;

                }
            }
            catch (Exception ex)
            {

                Session["_err"] = $"Error: {ex.Message}";
            }
        }

        protected void lnkSeleccionarCategoria_Command(object sender, CommandEventArgs e)
        {
            recuperarCategoria(e.CommandArgument.ToString());
        }

        private void recuperarCategoria(string claveCategoria)
        {
            string condicion = $"claveCategoria = '{claveCategoria}'";
            ECategoria categoria;
            try
            {
                categoria = lnC.BuscarRegistro(condicion);
                if(categoria != null)
                {
                    txtIdCategoria.Text = categoria.ClaveCategoria;
                    txtClaveCategoria.Text = categoria.Descripcion;
                }
            }
            catch (Exception ex)
            {

                Session["_err"] = $"Error: {ex.Message}";
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Session.Remove("_err");
            Session.Remove("_wrn");
            Session.Remove("_exito");
            Response.Redirect("wfrListaLibros.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
                try
                {
                    if (lnL.claveLibroRepetida(txtClaveLibro.Text) == false)
                    {
                        //modificar y revisar cuando haya que actualizar
                        ECategoria cate = new ECategoria();
                        cate.ClaveCategoria = txtIdCategoria.Text;
                        libro = new ELibro(txtClaveLibro.Text, txtTitulo.Text, txtIdAutor.Text, cate, false);
                        if (lnL.libroRepetido(libro) == false) //TODO: CARGAR LIBRO
                        {
                            if (lnL.insertar(libro) > 0)
                            {
                                Session["_exito"] = "El libro se ha insertado de manera exitosa";
                            limpiar();
                            }
                            else
                                Session["_wrn"] = "No se ha podido guardar el libro!!";
                        }
                        else
                            Session["_wrn"] = "Ese título ya existe para el autor seleccionado!!";
                    }
                else
                        Session["_wrn"] = "Atención la clave del Libro ya está en uso. Debe cambiarla!!";
             }
                catch (Exception ex)
                {

                Session["_err"] = "Error al guardar el libro";
                }
        }
    }
}