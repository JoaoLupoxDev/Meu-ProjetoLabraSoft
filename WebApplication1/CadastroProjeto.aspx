<%@ Page Title="Cadastro de Projetos" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CadastroProjeto.aspx.cs" Inherits="WebApplication1.CadastroProjeto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <!-- CARD 1: FORMULÁRIO DE CADASTRO -->
    <div class="container mt-5">
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
                    <label class="form-label" for="<%= txtBoxAreaConhecimento.ClientID %>">Área do Conhecimento:</label>
                    <asp:TextBox runat="server" ID="txtBoxAreaConhecimento" CssClass="form-control" placeholder="Digite a área do conhecimento" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= ddlCoordenadores.ClientID %>">Coordenador Responsável:</label>
                    <asp:DropDownList runat="server" ID="ddlCoordenadores" CssClass="form-select">
                        <asp:ListItem Value="">-- Selecione um Coordenador --</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label runat="server" ID="lblCoordenadoresEmpty" Text="Nenhum coordenador cadastrado" Visible="false" CssClass="text-danger small mt-1 d-block" />
                </div>

                <div class="mb-3">
                    <label class="form-label" for="<%= lstBoxBolsistas.ClientID %>">Selecione o(s) Bolsista(s):</label>
                    <asp:ListBox ID="lstBoxBolsistas" runat="server" SelectionMode="Multiple" CssClass="form-select" Rows="5">
                    </asp:ListBox>
                    <asp:Label runat="server" ID="lblBolsistasEmpty" Text="Nenhum bolsista cadastrado" Visible="false" CssClass="text-danger small mt-1 d-block" />
                </div>

                <hr />

                <asp:Button runat="server" ID="btnEnviar" CssClass="btn btn-success" OnClick="btnEnviar_Click" Text="Salvar" />

                <asp:Panel ID="pnlSalvar" runat="server" Visible="false" CssClass="alert alert-success mt-3 shadow-sm">
                    <asp:Label ID="lblSalvar" runat="server" Text="Cadastro Realizado!" CssClass="fw-bold" />
                    <br />
                    <asp:Label ID="lblResultado" runat="server" />
                </asp:Panel>

            </div>
        </div>
    </div>

    <!-- CARD 2: LISTA DE PROJETOS CADASTRADOS -->
    <div class="container mt-5 mb-5">
        <div class="card shadow">
            <div class="card-header bg-primary text-white">
                <h2 class="mb-0 fs-4">Projetos Cadastrados</h2>
            </div>
            <div class="card-body">                        
                <div class="table-responsive">

                    <asp:GridView ID="gvProjetos" runat="server" AutoGenerateColumns="true" 
                        DataKeyNames="ID"
                        CssClass="table table-striped table-hover table-bordered align-middle"
                        OnRowCommand="gvProjetos_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Ações">
                                <ItemTemplate>
                                    <asp:Button ID="btnDetalhes" runat="server" Text="🔍 Detalhes" 
                                        CommandName="VerDetalhes" 
                                        CommandArgument='<%# Container.DataItemIndex %>' 
                                        CssClass="btn btn-sm btn-info text-white me-1" />

                                    <asp:Button ID="btnEditar" runat="server" Text="✏ Editar" 
                                        CommandName="Editar" 
                                        CommandArgument='<%# Container.DataItemIndex %>' 
                                        CssClass="btn btn-sm btn-warning" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="text-center text-muted p-3">
                                Nenhum projeto cadastrado no momento.
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView> 

                    <!-- CARD DE EDIÇÃO DOS BOLSISTAS DO PROJETO -->
                    <asp:Panel ID="pnlEditarProjeto" runat="server" Visible="false" CssClass="card shadow mt-4">
                        <div class="card-header bg-warning text-dark">
                            <h3 class="mb-0 fs-5 fw-bold">EDITAR BOLSISTAS DO PROJETO: <asp:Label ID="lblNomeProjetoEditar" runat="server" /></h3>
                        </div>
                        <div class="card-body">
                            <asp:HiddenField ID="hfProjetoID" runat="server" />

                            <div class="mb-3">
                                <label class="form-label fw-bold">Selecione/Desmarque os Bolsistas Vinculados:</label>
                                <asp:ListBox ID="lstEditarBolsistas" runat="server" SelectionMode="Multiple" CssClass="form-select" Rows="6">
                                </asp:ListBox>
                                <small class="form-text text-muted">Mantenha a tecla <strong>Ctrl</strong> pressionada para selecionar ou remover múltiplos bolsistas.</small>
                            </div>

                            <div class="d-flex justify-content-between mt-4">
                                <asp:Button ID="btnExcluirEditar" runat="server" Text="EXCLUIR PROJETO" 
                                    CssClass="btn btn-danger" 
                                    OnClick="btnExcluirEditar_Click" 
                                    OnClientClick="return confirm('Deseja realmente excluir este projeto inteiro?');" />

                                <div>
                                    <asp:Button ID="btnCancelarEditar" runat="server" Text="CANCELAR" 
                                        CssClass="btn btn-secondary me-2" 
                                        OnClick="btnCancelarEditar_Click" />

                                    <asp:Button ID="btnSalvarEditar" runat="server" Text="SALVAR ALTERAÇÕES" 
                                        CssClass="btn btn-success" 
                                        OnClick="btnSalvarEditar_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- PAINEL DE DETALHES -->
                    <asp:Panel runat="server" ID="pnlDetails" Visible="false" CssClass="card border-info mt-4 shadow-sm">
                        <div class="card-header bg-info text-white d-flex justify-content-between align-items-center">
                            <h5 class="mb-0">📋 Detalhes do Projeto</h5>
                            <asp:Button ID="btnFecharDetalhes" runat="server" Text="✖ Fechar" CssClass="btn btn-sm btn-light" OnClick="btnFecharDetalhes_Click" />
                        </div>
                        <div class="card-body">
                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <p><strong>Título:</strong> <asp:Label ID="lblDetalheTitulo" runat="server" /></p>
                                    <p><strong>Área do Conhecimento:</strong> <asp:Label ID="lblDetalheArea" runat="server" /></p>
                                    <p><strong>Coordenador:</strong> <asp:Label ID="lblDetalheCoordenador" runat="server" /></p>
                                </div>
                                <div class="col-md-6">
                                    <p><strong>Verba Destinada:</strong> R$ <asp:Label ID="lblDetalheVerba" runat="server" /></p>
                                    <p><strong>Valor da Bolsa:</strong> R$ <asp:Label ID="lblDetalheValorBolsa" runat="server" /></p>
                                </div>
                            </div>

                            <h6 class="fw-bold mt-4 mb-3 text-primary">🎓 Bolsistas Vinculados</h6>
            
                            <asp:Repeater ID="rptBolsistas" runat="server">
                                <HeaderTemplate>
                                    <table class="table table-sm table-bordered table-striped">
                                        <thead class="table-light">
                                            <tr>
                                                <th>Nome</th>
                                                <th>Matrícula</th>
                                                <th>Sexo</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("Nome") %></td>
                                        <td><%# Eval("Matricula") %></td>
                                        <td><%# Eval("Sexo") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                        </tbody>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>

                            <asp:Label ID="lblSemBolsistas" runat="server" Text="Nenhum bolsista vinculado a este projeto." CssClass="text-muted fst-italic" Visible="false" />
                        </div>
                    </asp:Panel>

                </div>
            </div>
        </div>
    </div>
</asp:Content>