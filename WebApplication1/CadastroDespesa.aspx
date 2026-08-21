<%@ Page Title="Cadastro de Despesas" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CadastroDespesa.aspx.cs" Inherits="WebApplication1.CadastroDespesa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="card shadow">
            <div class="card-header bg-primary text-white">
                <h2 class="mb-0">Cadastro de Despesas</h2>
            </div>
            <div class="card-body">
                <div class="mb-3">
                    <label class="form-label">Descrição:</label>
                    <asp:TextBox runat="server" ID="txtDescricao" placeholder="Digite a descrição da despesa" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Categoria:</label>
                    <asp:TextBox runat="server" ID="txtCategoria" CssClass="form-control" placeholder="Digite a categoria" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Valor:</label>
                    <asp:TextBox runat="server" ID="txtValor" CssClass="form-control" TextMode="Number" step="0.01" placeholder="Digite o valor" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Data:</label>
                    <asp:TextBox runat="server" ID="txtData" CssClass="form-control" TextMode="Date" />
                </div>
                <div class="mb-3">
                    <label class="form-label">Projeto:</label>
                    <asp:DropDownList ID="ddlProjeto" runat="server" CssClass="form-select">
                    </asp:DropDownList>
                </div>

                <hr />
                <asp:Button ID="btnSalvar" runat="server" CssClass="btn btn-success" OnClick="btnSalvar_Click" Text="Salvar" />
                
                <asp:Panel 
                    ID="pnlResultado" 
                    runat="server" 
                    Visible="false" 
                    CssClass="alert alert-success mt-3 shadow-sm">
                    <asp:Label ID="lblTitulo" runat="server" CssClass="fw-bold d-block" />
                    <asp:Label ID="lblMensagem" runat="server" />
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>