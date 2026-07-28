<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ExemploBolsista.aspx.cs" Inherits="WebApplication1.ExemploBolsista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        
        <div class="card shadow-sm">
            <div class="card-header bg-primary text-white">
                <h3 class="mb-0">Teste Instancia</h3>
            </div>
            
            <div class="card-body bg-light">
                <h5 class="card-title text-muted mb-3">Instancia</h5>
                <hr />
                
                <div class="p-4 border rounded bg-white text-dark">
                    <asp:Label ID="lblResultado" runat="server" CssClass="h5 font-weight-normal lead"></asp:Label>
                </div>
            </div>            
        </div>        
    </div>

</asp:Content>