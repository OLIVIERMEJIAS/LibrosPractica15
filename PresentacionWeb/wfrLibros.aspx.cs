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
            cargarAutores();
            cargarCategorias();
        }
        private void cargarCategorias(string condicion = "")
        {
            DataTable dt;
            try
            {
                dt = lnC.ListarRegistros(condicion);
                if (dt != null)
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
                if (dt != null)
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
        /// <summary>
        /// Método al cargar el formulario, al cuales tiene 
        /// instrucciones solo cuando no es PostBack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string condicion = "";
            if (!IsPostBack)
            {
              
                limpiar();
                //se evalúa la existencia de la variable de sesión, 
                //puesto que lo siguiente solo es para modificar
                if (Session["_claveLibro"] != null)
                {
                    //se crea una condición para buscar el libro
                    condicion = $"claveLibro = '{Session["_claveLibro"].ToString()}'";
                    try
                    {
                        libro = lnL.buscarRegistro(condicion);
                        //si se encontró el libro se llenan los cuadros de texto con los datos
                        if (libro != null)
                        {
                            txtClaveLibro.Text = libro.ClaveLibro;
                            txtTitulo.Text = libro.Titulo;
                            recuperarAutor(libro.ClaveAutor);
                            recuperarCategoria(libro.Clavecategoria.ClaveCategoria);
                            //se crea una cookie y se llena con los datos del libro,
                            //para poder leerlos después de un PostBacj
                            HttpCookie cookie = new HttpCookie("MyCookie");
                            cookie["_claveLibro"] = libro.ClaveLibro;
                            cookie["_titulo"] = libro.Titulo;
                            cookie["_claveAutor"] = libro.ClaveAutor;
                            cookie["_claveCategoria"] = libro.Clavecategoria.ClaveCategoria;
                            Response.Cookies.Add(cookie);
                        }
                        //si el libro ya no existe!!
                        else
                        {
                            Session["_wrn"] = "Este libro ya no existe!!";
                            btnRegresar_Click(sender, e);
                        }
                    }
                    catch (Exception ex)
                    {

                        Session["_err"] = $"Error: {ex.Message}";
                    }



                }
            }

        }

        protected void btnBuscarAutor_Click(object sender, EventArgs e)
        {
            string javaScript = "AbrirModal();";
            cargarAutores($"nombre like '%{txtNombreAutor.Text}%'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", javaScript, true);
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
                if (autor != null)
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
                if (categoria != null)
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
            Session.Remove("_claveLibro");
            Response.Redirect("wfrListaLibros.aspx", false);
        }

        /// <summary>
        /// Este método busca cambios en los campos requeridos, claveLibro, titulo,
        /// claveAutor y claveCategoria se incializan variables con los datos 
        /// que traen las cookies enviadas desde este mismo formulario
        /// </summary>
        /// <param name="bCL"></param>
        /// <param name="bT"></param>
        /// <param name="bAu"></param>
        /// <param name="bCat"></param>
        /// <returns></returns>
        protected bool hayCambios(ref bool bCL, ref bool bT,
            ref bool bAu,
            ref bool bCat)
        {
            string claveL = Request.Cookies["MyCookie"]["_claveLibro"];
            string tit = Request.Cookies["MyCookie"]["_titulo"];
            string claveA = Request.Cookies["MyCookie"]["_claveAutor"];
            string claveC = Request.Cookies["MyCookie"]["_claveCategoria"];
            if (txtClaveLibro.Text != claveL)
                bCL = true;
            if (txtTitulo.Text != tit)
                bT = true;
            if (txtIdAutor.Text != claveA)
                bAu = true;
            if (txtIdCategoria.Text != claveC)
                bCat = true;
            if (!bCL && !bT && !bAu && !bCat)
                return false;
            else
                return true;
        }
        /// <summary>
        /// Se crea un objeto ELibro con los datos de los campos del 
        /// formulario, que son los más actualizados
        /// , este método se llama cuando se necesite un 
        /// objeto de este tipo para validar o modificar
        /// </summary>
        /// <returns></returns>
        protected ELibro crearLibro()
        {
            ECategoria cate = new ECategoria();
            cate.ClaveCategoria = txtIdCategoria.Text;
            return libro = new ELibro(txtClaveLibro.Text, txtTitulo.Text, txtIdAutor.Text, cate, false);
        }

        /// <summary>
        /// Este método efectúa lo que es solo el modificado del libro,
        /// junto con la remoción de la variable de sesión y la redirección a la lista de libros
        /// </summary>
        /// <param name="lib"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void modificar(ELibro lib, Object sender, EventArgs e)
        {

            if (lnL.modificar(lib, Request.Cookies["MyCookie"]["_claveLibro"]) > 0)
            {
                Session["_exito"] = "El libro se ha modificado de manera exitosa";
                Session.Remove("_claveLibro");
                Response.Redirect("wfrListaLibros.aspx", false);
            }
            else
                Session["_wrn"] = "No se ha podido modificar el libro!!";

        }
        /// <summary>
        /// Este método guarda un nuevo registro e igualmente modifica un registro 
        /// ya existente, todo dependiendo que la variable de sesión claveLibro exista o no
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //si la variable existe
                if (Session["_claveLibro"] != null)
                {
                    //se crean variables bool para verificar cambios en los cuadros de texto del formulario
                    bool bCL = false;
                    bool bT = false;
                    bool bAu = false;
                    bool bCat = false;
                    //si hay cambios
                    if (hayCambios(ref bCL, ref bT, ref bAu, ref bCat))
                    {

                        //si hubo cambios específicamente en la clave del libro
                        if (bCL)
                        {
                            //se verifica que no exista ya
                            if (lnL.claveLibroRepetida(txtClaveLibro.Text) == false)
                            {
                                //de no existir se verifica si hubo cambios en el título o claveAutor
                                if (bT || bAu)
                                {
                                    //se crea un libro para pasarlo por argumento
                                    //para ver si el título existe para ese autor
                                    ELibro lib = crearLibro();
                                    //si no existe esa combinación se puede modificar el libro
                                    if (lnL.libroRepetido(lib) == false)
                                    {
                                        modificar(lib, sender, e);
                                    }
                                    //cuando ya exista
                                    else
                                        Session["_wrn"] = "Atención este título de Libro ya se encuentra con este Autor. Debe cambiarlo!!";

                                }
                                //si no se modificó el título o autor, pero la clave ya se validó se modifica
                                else
                                {
                                    ELibro lib = crearLibro();
                                    modificar(lib, sender, e);
                                }
                            }
                            // si la clave existe
                            else
                                Session["_wrn"] = "Atención la clave del Libro ya está en uso. Debe cambiarla!!";
                        }
                        //si no se modificó la claveLibro
                        else
                            //si se han modificado el título o autor
                            if (bT || bAu)
                            {
                            //se crea un objeto ELibro, para modificar la combinación de título y autor
                                ELibro lib = crearLibro();
                            //si el título y autor no están repetidos
                            //se modifica el libro
                                if (lnL.libroRepetido(lib) == false)
                                {
                                    modificar(lib, sender, e);
                                }
                                //si extá repetida la combinación
                                else
                                    Session["_wrn"] = "Atención este título de Libro ya se encuentra con este Autor. Debe cambiarlo!!";

                            }
                        //sino se modificó la claveLibro, ni el título o el autor, aún el método hayCambios devolvió true
                        //es porque la categoría fue cambiada, y como esta no necesita validación
                        //se procede a modificar directamente
                            else
                            {
                                ELibro lib = crearLibro();
                                modificar(lib, sender, e);
                            }
                    }
                    //sino hubo cambios de ningún tipo
                    else
                        Session["_wrn"] = "No hay cambios que actualizar!!";


                }
                //sino existe la variable de sesión claveLibro, es porque se está guardando un libro nuevo
                else
                {
                    //se valida la clave
                    if (lnL.claveLibroRepetida(txtClaveLibro.Text) == false)
                    {
                        ELibro libro = crearLibro();
                        //se valida el título con el autor
                        if (lnL.libroRepetido(libro) == false)
                        {
                            //se inserta
                             if (lnL.insertar(libro) > 0)
                                {
                                    Session["_exito"] = "El libro se ha insertado de manera exitosa";
                                    limpiar();
                                }
                             //si falla la insersión
                                else
                                    Session["_wrn"] = "No se ha podido guardar el libro!!";
                        }
                        //si está repetido el libro con el autor
                        else
                            Session["_wrn"] = "Ese título ya existe para el autor seleccionado!!";
                    }
                    //si está repetida la clave del libro
                    else
                        Session["_wrn"] = "Atención la clave del Libro ya está en uso. Debe cambiarla!!";
                }
            }
            //tratamiento de excepciones
            catch (Exception ex)
            {

                Session["_err"] = "Error al guardar el libro";
            }
        }
    }
}