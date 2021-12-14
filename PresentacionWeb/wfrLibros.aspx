<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrLibros.aspx.cs" Inherits="PresentacionWeb.wfrLibros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="frmhead" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="miScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="frmBody" runat="server">
    <div class="container">
        <div class="card-header text-center">   
            <h1>Mantenimiento de Libros</h1>

        </div>
        <br />
        <div class="row mt-3">
            <div class="col-2">
                <asp:Label ID="Label1" runat="server" Text="Clave Libro"></asp:Label>
                <asp:TextBox ID="txtClaveLibro" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-4">
                <asp:Label ID="Label2" runat="server" Text="    Título"></asp:Label>
                <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="col-3">
                <asp:TextBox ID="txtIdAutor" runat="server" CssClass="form-control" ReadOnly="True" Visible="False"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text="    Autor"></asp:Label>

                <div class="input-group mb-3">
                 <asp:TextBox CssClass="form-control" ID="txtClaveAutor" runat="server"  ReadOnly="True" aria-describedby="btnModalAutor"></asp:TextBox>
                
                    <button class="btn btn-outline-primary" type="button" id="btnModalAutor"
                    data-bs-toggle="modal"
                    data-bs-target="#autorModal">Buscar</button>
                
                
                </div>
                
            </div>

            <div class="col-3">
                <asp:TextBox ID="txtIdCategoria" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" Text="    Categoría"></asp:Label>
                <div class="input-group mb-3">
                 <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server"  ReadOnly="True" aria-describedby="btnModalCategoria"></asp:TextBox>

                <button class="btn btn-outline-primary" type="button" id="btnModalCatgoria"
                    data-bs-toggle="modal"
                    data-bs-target="#modalCategorias">Buscar</button>
                </div>
            </div>

        </div>
        <br />
         <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary"/>
         <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-warning"/>
    </div> <%--Fin del container--%>
    <%--modales--%>    
    <%--modal categorias--%>   
    <div class="modal" tabindex="-1" id="modalCategorias">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Buscar Categoría</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <div class="row mt-3">
            <div class="col-auto">
                <asp:Label ID="Label6" runat="server" Text="Categoría"></asp:Label>
            </div>
            <div class="col-auto">
                <asp:TextBox ID="txtCategoria" runat="server" CccClass="form-control" ValidationGroup="2"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="2" ControlToValidate="txtCategoria"></asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="2" />
            </div>
            <div class="col-auto">
                <asp:Button ID="btnBuscarCategoria" runat="server" Text="Filtrar" CssClass="btn btn-secondary" OnClick="btnBuscarCategoria_Click"/>
            </div>
            <br />
            <asp:GridView ID="gdvCategorias" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" Width="100%">
                <AlternatingRowStyle BackColor="PaleGoldenrod" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkSeleccionarCategoria" runat="server" CommandArgument='<%# Eval("claveCategoria").ToString() %>'>Seleccionar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="claveCategoria" HeaderText="Claave Categoría" Visible="False" />
                    <asp:BoundField DataField="descripcion" HeaderText="Categoría" />
                </Columns>
                <FooterStyle BackColor="Tan" />
                <HeaderStyle BackColor="Tan" Font-Bold="True" />
                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite"  />
                <SortedAscendingCellStyle BackColor="#FAFAE7" />
                <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                <SortedDescendingCellStyle BackColor="#E1DB9C" />
                <SortedDescendingHeaderStyle BackColor="#C2A47B" />
            </asp:GridView>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
      </div>
    </div>
  </div>
</div>
    <%--modal autor--%>
<div class="modal" tabindex="-1" id="autorModal">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Buscar Autor</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <div class="row mt-3">
            <div class="col-auto">
                <asp:Label ID="Label5" runat="server" Text="Autor"></asp:Label>
            </div>
            <div class="col-auto">
                <asp:TextBox ID="txtNombreAutor" runat="server" CccClass="form-control" ValidationGroup="1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="1" ControlToValidate="txtNombreAutor"></asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="1" />
            </div>
            <div class="col-auto">
                <asp:Button ID="btnBuscarAutor" runat="server" Text="Filtrar" CssClass="btn btn-secondary" OnClick="btnBuscarAutor_Click"/>
            </div>
        </div>
            <br />
            <asp:GridView ID="grdAutores" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" Width="100%">
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkSeleccionarAutor" runat="server" CommandArgument='<%# Eval("claveAutor").ToString() %>'>Seleccionar</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="claveAutor" HeaderText="Clave Autor" Visible="False" />
                <asp:BoundField DataField="apPaterno" HeaderText="Apellido" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
            </Columns>
            <FooterStyle BackColor="Tan" />
            <HeaderStyle BackColor="Tan" Font-Bold="True" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <SortedAscendingCellStyle BackColor="#FAFAE7" />
            <SortedAscendingHeaderStyle BackColor="#DAC09E" />
            <SortedDescendingCellStyle BackColor="#E1DB9C" />
            <SortedDescendingHeaderStyle BackColor="#C2A47B" />
        </asp:GridView>
        </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
      </div>
    </div>
  </div>
</div>
</asp:Content>
