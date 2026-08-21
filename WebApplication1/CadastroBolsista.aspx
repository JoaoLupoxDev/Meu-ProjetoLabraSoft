<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CadastroBolsista.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="card shadow">
            <div class="card-header bg-primary text-white">
                <h2 class="mb-0">Cadastro de Bolsista</h2>
            </div>
            <div class="card-body">
                <div class="mb-3">
                    <label class="form-label">Nome Completo:</label>
                    <asp:TextBox runat="server" ID="txtBoxNome" placeholder="Digite o nome" CssClass="form-control"/>
                </div>
                <div class="mb-3">
                    <label class="form-label">CPF:</label>
                    <asp:TextBox runat="server" ID="txtBoxCPF" CssClass="form-control" placeholder="Digite o CPF"/>
                </div>
                <div class="mb-3">
                    <label class="form-label">Matricula:</label>
                    <asp:TextBox runat="server" ID="txtBoxMatricula" CssClass="form-control" placeholder="Digite o numero da matricula"/>
                </div>
                <div class="mb-3">
                    <label class="form-label">Data de Nascimento:</label>
                    <asp:TextBox runat="server" ID="txtBoxDate" TextMode="Date" CssClass="form-control" placeholder="Digite a sua data de nascimento"/>
                </div>
                <div class="mb-3">
                    <label class="form-label d-block">Selecione o seu gênero:</label>
                    <asp:DropDownList runat="server" ID="ddlGenero" CssClass="form-select">
                        <asp:ListItem Value="M">Masculino</asp:ListItem>
                        <asp:ListItem Value="F">Feminino</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <hr />
                <asp:Button runat="server" ID="btnEnviar" CssClass="btn btn-success" OnClick="btnEnviar_Click" Text="Salvar"/>
                <asp:Button runat="server" ID="btnLimpar" CssClass="btn btn-secondary" OnClick="btnLimpar_Click" Text="Limpar"/>
                
                <asp:Panel
                    ID="pnlCadastro"
                    runat="server"
                    Visible="false"
                    CssClass="alert alert-success mt-3 shadow-sm">
                    <asp:Label ID="lblCadastro" runat="server" Text="Cadastro Realizado!" CssClass="fw-bold"></asp:Label>
                    <br />
                    <asp:Label ID="lblResultado" runat="server" />
                </asp:Panel>
            </div>
        </div>
    </div>
    
    <div class="container mt-5 mb-5">
        <div class="card shadow">
            <div class="card-header bg-primary text-white">
                <h2 class="mb-0">Bolsistas Cadastrados</h2>
            </div>
            <div class="card-body">
                <asp:Panel ID="pnlFiltros" runat="server" Visible="false" CssClass="mb-3">
                    <asp:Button ID="btnFiltroMulheres" runat="server" Text="Filtrar Mulheres" CssClass="btn btn-outline-primary me-2" OnClick="btn_FiltroMulheres"/>
                    <asp:Button ID="btnOrdemAlfabetica" runat="server" Text="Filtrar em Ordem Alfabética" CssClass="btn btn-outline-primary me-2" OnClick="btn_OrdemAlfabetica" />
                    <asp:Button ID="btnOrdemDeCadastro" runat="server" Text="Filtrar em Ordem de Cadastro" CssClass="btn btn-outline-secondary" OnClick="btn_FiltroOrdemDeCadastro" />
                </asp:Panel>
                <div class="table-responsive">
                    <asp:GridView ID="gvBolsistas" runat="server" AutoGenerateColumns="true" 
                          CssClass="table table-striped table-hover table-bordered align-middle">
                        <EmptyDataTemplate>
                            <div class="text-center text-muted p-3">
                                Nenhum bolsista cadastrado no momento.
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>              
                </div>
            </div>
        </div>
    </div>
</asp:Content>