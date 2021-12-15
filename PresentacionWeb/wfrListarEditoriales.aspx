<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrListarEditoriales.aspx.cs" Inherits="PresentacionWeb.wfrListarEditoriales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="frmhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="frmBody" runat="server">
   <div class="container">
        <div class="card-header text-center">
            <h1>
                Gestionar Editoriales
            </h1>
        </div>
            <br />
    <% if (Session["_wrn"] != null) { %>
            <div class="alert alert-warning" role="alert">
                <%= Session["_wrn"]%>
                  <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
<% Session["_wrn"] = null;
          }%>
        <% if (Session["_err"] != null) { %>
            <div class="alert alert-danger" role="alert">
                <%= Session["_err"]%>
                  <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
<% Session["_err"] = null;
          }%>
     <div class="row mt-3 " >
                <div class="col-auto">
                    <asp:Label ID="Label1" runat="server" Text="Nombre de la Editorial"></asp:Label>
                </div>

               <div class="col-auto">
                    <asp:TextBox ID="txtNomEdit" runat="server" CssClass="form-control" ToolTip="Ingrese una parte del nombre a buscar"></asp:TextBox>
                   <asp:RequiredFieldValidator CssClass="text-start" ID="rfvTxtNomEdit" runat="server" ErrorMessage="Debe agregar una parte del nombre de la editorial antes de buscar" ControlToValidate ="txtNomEdit" ForeColor="Red" ValidationGroup="1">*</asp:RequiredFieldValidator >
                </div>
                
                <div class="col-auto">
                    <asp:Button Cssclass="btn btn-primary" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" ToolTip="Permite buscar el nombre de una editorial" ValidationGroup="1" />
                    
                </div>
                 <div class="col-auto">
                    <asp:Button Cssclass="btn btn-secondary" ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" ToolTip="Permite limpiar el buscador y carga las editoriales" />
                    
                </div>
                <div class="col-auto">
                    <asp:Button Cssclass="btn btn-info" ID="btnNuevo" runat="server" Text="Nueva Editorial" ToolTip="Permite agregar una nueva Editorial" OnClick="btnNuevo_Click"  />
                </div>
            </div>

            
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="1" ForeColor="Red" />
            <br />
            <asp:GridView CssClass="text-start" ID="gdvEditoriales" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" Width="100%" AllowPaging="True" OnPageIndexChanging="gdvEditoriales_PageIndexChanging">
                <AlternatingRowStyle BackColor="PaleGoldenrod" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkModificar" runat="server" CommandArgument='<%# Eval("claveEditorial").ToString() %>'>Modificar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEliminar" runat="server" CommandArgument='<%# Eval("claveEditorial").ToString() %>'>Eliminar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkVerEjemplares" runat="server" CommandArgument='<%# Eval("claveEditorial").ToString() %>'>Ver Ejemplares</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="claveEditorial" HeaderText="Clave Editorial" Visible="False" />
                    <asp:BoundField DataField="nombre" HeaderText="Editorial" />
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
</asp:Content>
