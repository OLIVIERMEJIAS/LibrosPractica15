<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrListarEjemplares.aspx.cs" Inherits="PresentacionWeb.wfrListarEjemplares" %>
<asp:Content ID="Content1" ContentPlaceHolderID="frmhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="frmBody" runat="server">
    <div class="container">
      <asp:Label CssClass="text-center" ID="txtNombreEdit" runat="server" Text="Ejemplares"></asp:Label>
       <asp:GridView  Cssclass="container text-start" ID="gdvEjemplares" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" OnPageIndexChanging="gdvEjemplares_PageIndexChanging" Width="100%">
           <AlternatingRowStyle BackColor="PaleGoldenrod" />
           <Columns>
               <asp:BoundField DataField="Titulo" HeaderText="Título" />
               <asp:BoundField DataField="Condicion" HeaderText="Condición" />
               <asp:BoundField DataField="Estado" HeaderText="Estado" />
               <asp:BoundField DataField="Edicion" HeaderText="Edición" />
               <asp:BoundField DataField="Editorial" HeaderText="Editorial" />
               <asp:BoundField DataField="Paginas" HeaderText="Número de Páginas" />
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
      <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-warning" OnClick="btnRegresar_Click"/>

    </div>
</asp:Content>
