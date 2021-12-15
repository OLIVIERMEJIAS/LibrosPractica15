<%@ Page Title="" Language="C#" MasterPageFile="~/wfrPlantilla.Master" AutoEventWireup="true" CodeBehind="wfrEditoriales.aspx.cs" Inherits="PresentacionWeb.wfrEditoriales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="frmhead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="frmBody" runat="server">
     <div class="container">
        <div class="card-header text-center">   
            <h1>Mantenimiento de Editoriales</h1>
        </div>
        <br />
        <%--alerts--%>
        <% if (Session["_exito"] != null) { %>
            <div class="alert alert-success" role="alert">
                <%= Session["_exito"]%>
                  <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
        <% Session["_exito"] = null;
          }%>

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
        <div class="row mt-3">
            <div class="col-4">
                <asp:Label ID="Label1" runat="server" Text="Clave Editorial"></asp:Label>
                <asp:TextBox ID="txtClaveEditorial" runat="server" CssClass="form-control" ValidationGroup="5"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Clave de Editorial incompleta!! Por favor, complétela." ValidationGroup="5" ControlToValidate="txtClaveEditorial" ForeColor="Red">*</asp:RequiredFieldValidator>
            </div>

            <div class="col-8">
                <asp:Label ID="Label2" runat="server" Text="Nombre Editorial"></asp:Label>
                <asp:TextBox ID="txtNomEdit" runat="server" CssClass="form-control" ValidationGroup="5"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Nombre de Editorial incompleto!! Por favor, complételo." ValidationGroup="5" ControlToValidate="txtNomEdit" ForeColor="Red">*</asp:RequiredFieldValidator>
            </div>
        </div>
        <br />
         <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" ValidationGroup="5" OnClick="btnGuardar_Click" />
         <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-warning" OnClick="btnRegresar_Click"/>
        
        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="5" ForeColor="Red" Font-Italic="True" />
    </div>
</asp:Content>
