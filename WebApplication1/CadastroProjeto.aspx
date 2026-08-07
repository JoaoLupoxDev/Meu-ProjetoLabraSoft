<%@ Page Title="Cadastro de Projetos" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CadastroProjeto.aspx.cs" Inherits="WebApplication1.CadastroProjeto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container mt-5 mb-5">
        <div class="card shadow">
            <div class="card-header bg-primary text-white">
                <h2 class="mb-0 fs-4">Cadastro de Projeto</h2>
            </div>
            <div class="card-body">
                
                <div class="mb-3">
                    <label class="form-label" for="<%= txtBoxTitulo.ClientID %>">Título do Projeto:</label>
                    <asp:TextBox runat="server" ID="txtBoxTitulo" placeholder="Digite o título do projeto" CssClass="form-control" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= txtBoxVerba.ClientID %>">Verba Destinada:</label>
                    <asp:TextBox runat="server" ID="txtBoxVerba" CssClass="form-control" placeholder="Digite o valor da verba (R$)" />
                </div>
                 <div class="mb-3">
                    <label class="form-label" for="<%= txtBoxValorBolsa.ClientID %>">Valor da Bolsa:</label>
                    <asp:TextBox runat="server" ID="txtBoxValorBolsa" CssClass="form-control" placeholder="Digite o valor da bolsa (R$)" />
                </div>
                 <div class="mb-3">
                    <label class="form-label" for="<%= txtBoxValorBolsa.ClientID %>">Área do Conhecimento:</label>
                    <asp:TextBox runat="server" ID="txtBoxAreaConhecimento" CssClass="form-control" placeholder="Digite a area do conhecimento" />
                </div>
                <div class="mb-3">
                    <label class="form-label" for="<%= ddlCoordenadores.ClientID %>">Coordenador Responsável:</label>
                    <asp:DropDownList runat="server" ID="ddlCoordenadores" CssClass="form-select">
                        <asp:ListItem Value="">-- Selecione um Coordenador --</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= lstBoxBolsistas.ClientID %>">Selecione o(s) Bolsista:</label>
                    <asp:ListBox ID="lstBoxBolsistas" runat="server" SelectionMode="Multiple" CssClass="form-select" Rows="5">
                    </asp:ListBox>
                </div>

                <hr />

                <asp:Button runat="server" ID="btnEnviar" CssClass="btn btn-success" OnClick="btnEnviar_Click" Text="Salvar" />

                <asp:Panel ID="pnlSalvar" runat="server" Visible="true" CssClass="alert alert-success mt-3 shadow-sm">
                    <asp:Label ID="lblSalvar" runat="server" Text="Cadastro Realizado!" CssClass="fw-bold" />
                    <br />
                    <asp:Label ID="lblResultado" runat="server" />
                </asp:Panel>
                 <div class="container mt-5">
                 <div class="card shadow">
                     <div class="card-header bg-primary text-white">
                         <h2 class="mb-0">Projetos Cadastrados</h2>
                     </div>
                     <div class="card-body">                        
                             <div class="card-body table-responsive">

                                 <asp:GridView ID="gvProjetos" runat="server" AutoGenerateColumns="true" 
                                       CssClass="table table-striped table-hover table-bordered align-middle"
                                        OnRowCommand="gvProjetos_RowCommand">
                                        <Columns>
                                             <asp:TemplateField HeaderText="Ações">
                                                <ItemTemplate>
                                                    <asp:Button ID="Button1" runat="server" Text="🔍 Detalhes" 
                                                    CommandName="VerDetalhes" 
                                                    CommandArgument='<%# Container.DataItemIndex %>' 
                                                    CssClass="btn btn-sm btn-info text-white" />
                                                    </ItemTemplate>
                                               </asp:TemplateField>
                                        </Columns>
                                 <EmptyDataTemplate>
                                     <div class="text-center text-muted p-3">
                                         Nenhum projeto cadastrado no momento.
                                     </div>
                                 </EmptyDataTemplate>
                               </asp:GridView> 
                               <asp:Panel runat="server" ID="pnl" Visible="false">

                               </asp:Panel>
                            </div>
                     </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>