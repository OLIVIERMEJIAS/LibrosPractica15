<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrListaLibros.aspx.cs" Inherits="PresentacionWeb.wfrListaLibros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="frmhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="frmBody" runat="server">
    <div class="container">
        <div class="card-header text-center">
            <h1>
                Gestionar Libros
            </h1>
            <br />
           <%--Alerts--%>

            <% if (Session["_err"] != null) { %>
                <div class="alert alert-danger"                 role="alert">
                <%=Session["_err"]%>
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </div>
              
             <%}%>
           

            
            <% if (Session["_wrn"] != null) { %>
                <div class="alert alert-danger"                 role="alert">
                <%=Session["_wrn"]%>
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </div>
              
             <%}%>



            <div class="row mt-3">
                <div class="col-auto">
                    <asp:Label ID="Label1" runat="server" Text="Título del Libro"></asp:Label>
                </div>

               <div class="col-auto">
                    <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" ToolTip="Ingrese una parte del título a buscar"></asp:TextBox>
                   <asp:RequiredFieldValidator CssClass="text-start" ID="rfvTxtTitulo" runat="server" ErrorMessage="Debe agregar una parte del título antes de buscar" ControlToValidate ="txtTitulo" ForeColor="Red" ValidationGroup="1">*</asp:RequiredFieldValidator >
                </div>
                
                <div class="col-auto">
                    <asp:Button Cssclass="btn btn-primary" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" ToolTip="Permite buscar el título del un libro" ValidationGroup="1" />
                    
                </div>
                 <div class="col-auto">
                    <asp:Button Cssclass="btn btn-secondary" ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" ToolTip="Permite limpiar el buscador y carga los libros" />
                    
                </div>
                <div class="col-auto">
                    <asp:Button Cssclass="btn btn-info" ID="btnNuevo" runat="server" Text="Nuevo Libro" ToolTip="Permite agregar un nuevo libro" OnClick="btnNuevo_Click" />
                </div>
            </div>

            
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="1" ForeColor="Red" />
            <br />
            <asp:GridView  Cssclass="container text-start" ID="grdLibros" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" EmptyDataText="No hay datos de Libros  que mostrar, inserte uno nuevo." OnPageIndexChanging="grdLibros_PageIndexChanging" PageSize="15">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkModificar" runat="server" CommandArgument='<%# Eval("Clave").ToString() %>' OnCommand="lnkModificar_Command" ForeColor="Blue">Modificar<i class="fas fa-pen-alt"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            
                            <asp:LinkButton ID="lnkEliminar" runat="server" CommandArgument='<%# Eval("Clave").ToString() %>' OnClick="lnkEliminar_Click" ForeColor="Red" OnCommand="lnkEliminar_Command">Eliminar<i class="fas fa-trash"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Clave" HeaderText="Clave" />
                    <asp:BoundField DataField="Titulo" HeaderText="Título" />
                    <asp:BoundField DataField="Autor" HeaderText="Autor" />
                    <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                </Columns>
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
