<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrEditorialEliminar.aspx.cs" Inherits="PresentacionWeb.wfrEditorialEliminar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="frmhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="frmBody" runat="server">
    <div class="container">
        <div class="card-header">
            <h1>Confirmación de Eliminación</h1>
        </div>
        <div class="card">
        <div class="card-body">
            <asp:Label ID="lblClaveEditorial" runat="server" Text="Clave de la Editorial: "></asp:Label>
            <br />
            <asp:Label ID="lblNombreEdit" runat="server" Text="Nombre de la Editorial: "></asp:Label>
            <p class="card-text">La Editorial será ELIMINADA! Confirma la eliminación???</p>
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" />
            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-warning" OnClick="btnRegresar_Click"/>
        </div>
    </div>
    </div>
</asp:Content>
