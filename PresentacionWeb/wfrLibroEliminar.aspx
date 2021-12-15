<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrLibroEliminar.aspx.cs" Inherits="PresentacionWeb.wfrLibroEliminar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="frmhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="frmBody" runat="server">
    <div class="container">
        <div class="card-header">
            <h1>Confirmación de Eliminación</h1>
        </div>
        <div class="card">
        <div class="card-body">
            <h5 class="card-title">Título: </h5>
            <h6 class="card-subtitle">Clave: </h6>
            <h6 class="card-subtitle">Título: </h6>
            <h6 class="card-subtitle">Autor: </h6>
            <h6 class="card-subtitle">Categoría: </h6>
            <p class="card-text">El libro será ELIMINADO! Confirma la eliminación???</p>
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" />
            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-warning"/>
        </div>
    </div>
    </div>
</asp:Content>
