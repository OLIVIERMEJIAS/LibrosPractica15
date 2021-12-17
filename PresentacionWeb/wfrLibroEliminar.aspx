<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrLibroEliminar.aspx.cs" Inherits="PresentacionWeb.wfrLibroEliminar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="frmhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="frmBody" runat="server">
     <% if (Session["_err"] != null) { %>
                <div class="alert alert-danger"                 role="alert">
                <%=Session["_err"]%>
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </div>
              
             <%Session["_err"] = null;
                         }%>
           

            
            <% if (Session["_wrn"] != null) { %>
                <div class="alert alert-danger"                 role="alert">
                <%=Session["_wrn"]%>
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </div>
              
             <%Session["_wrn"] = null;
                         }%>
    
    <div class="container">
        <div class="card-header">
            <h1 class="text-center">Confirmación de Eliminación</h1>
        </div>
        <div class="row">
        <div class="card">
        <div class="card-body">
            <h5 class="card-title">Título: <%= ViewState["_titulo"] %></h5>
            <h6 class="card-subtitle">Clave: <%= Session["_claveLibro"] %></h6>
            <h6 class="card-subtitle">Autor: <%= ViewState["_autor"] %></h6>
            <h6 class="card-subtitle">Categoría: <%= ViewState["_categoria"] %></h6>
            <p class="card-text">El libro será ELIMINADO! Confirma la eliminación???</p>
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" />
            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-warning" OnClick="btnRegresar_Click"/>
        </div>

        </div>
    </div>
    </div>
</asp:Content>
