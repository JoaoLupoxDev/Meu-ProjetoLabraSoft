<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CadastroCoordenador.aspx.cs" Inherits="WebApplication1.CadastroCoordenador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container mt-5">
        <div class="card shadow">
            <div class="card-header bg-primary text-white">
                <h2 class="mb-0">Cadastro de Coordenadores</h2>
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
                        <label class="form-label">Titulação:</label>
                        <asp:TextBox runat="server" ID="txtBoxTitulacao" CssClass="form-control" placeholder="Digite a titulação:" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Área de Atuação:</label>
                        <asp:TextBox runat="server" ID="txtBoxAreaAtuacao" CssClass="form-control" placeholder="Digite a sua área de atuação"/>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Email:</label>
                        <asp:TextBox runat="server" ID="txtBoxEmail" CssClass="form-control" placeholder="Digite seu email"/>
                    </div>

                    <hr />
                    <asp:Button ID="btnEnviar" runat="server" CssClass="btn btn-success" OnClick="btnEnviar_Click" Text="Salvar"/>
                        <asp:Panel 
                            ID="pnlCadastro"
                            runat="server"
                            Visible="false"
                            CssClass="alert alert-success mt-3 shadow-sm">

                            <asp:Label ID="lblCadastro" runat="server" Text="Cadastro Realizado!" CssClass="fw-bold" Visible="True"></asp:Label>
                            <br />

                            <asp:Label ID="lblResultado" runat="server" Visible="True" />
                        </asp:Panel>
                </div>
        </div>
    </div>
    
    <div class="container mt-5">
        <div class="card shadow">
            <div class="card-header bg-primary text-white">
                <h2 class="mb-0">Coordenadores Cadastrados</h2>
            </div>
            <div class="card-body">
                    <div class="card-body table-responsive">
                   <asp:Panel ID="pnlFiltros" runat="server" Visible="True">
                      <div class="mb-3">
                        <asp:Label runat="server" ID="lblFiltro" Text="Filtro:" class="form-label" Visible="True" ></asp:Label>
                        <asp:TextBox Visible="true" runat="server" ID="txtBoxFiltro" CssClass="form-control" placeholder="Digite o filtro:" />
                      </div>
                       <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-success" OnClick="btn_Buscar"/>
                   </asp:Panel><br />
                          <!-- COLUNA DE AÇÕES NO GRIDVIEW -->
                        <asp:GridView ID="gvCoordenadores" runat="server" AutoGenerateColumns="true" 
                            DataKeyNames="ID"
                            CssClass="table table-striped table-hover table-bordered align-middle" 
                            OnRowCommand="gvCoordenadores_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Ações">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" runat="server" Text="✏ Editar" 
                                            CommandName="Editar" 
                                            CommandArgument='<%# Container.DataItemIndex %>' 
                                            CssClass="btn btn-sm btn-warning" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <!-- CARD DE EDIÇÃO (IGUAL AO ESBOÇO) -->
                        <asp:Panel ID="pnlEditarCoordenador" runat="server" Visible="false" CssClass="card shadow mt-4">
                            <div class="card-header bg-warning text-dark">
                                <h3 class="mb-0 fs-4">EDITAR</h3>
                            </div>
                            <div class="card-body">
                                <asp:HiddenField ID="hfCoordenadorID" runat="server" />

                                <div class="mb-3">
                                    <label class="form-label">Nome:</label>
                                    <asp:TextBox ID="txtEditarNome" runat="server" CssClass="form-control" />
                                </div>

                                <div class="mb-3">
                                    <label class="form-label">E-mail:</label>
                                    <asp:TextBox ID="txtEditarEmail" runat="server" CssClass="form-control" />
                                </div>

                                <div class="mb-3">
                                    <label class="form-label">Titulação:</label>
                                    <asp:TextBox ID="txtEditarTitulacao" runat="server" CssClass="form-control" />
                                </div>

                                <div class="mb-3">
                                    <label class="form-label">Área de Atuação:</label>
                                    <asp:TextBox ID="txtEditarAreaAtuacao" runat="server" CssClass="form-control" />
                                </div>

                                <div class="d-flex justify-content-between mt-4">
                                    <asp:Button ID="btnExcluirEditar" runat="server" Text="EXCLUIR" 
                                        CssClass="btn btn-danger" 
                                        OnClick="btnExcluirEditar_Click" 
                                        OnClientClick="return confirm('Deseja realmente excluir este coordenador?');" />

                                    <asp:Button ID="btnSalvarEditar" runat="server" Text="SALVAR" 
                                        CssClass="btn btn-success" 
                                        OnClick="btnSalvarEditar_Click" />
                                </div>
                            </div>
                        </asp:Panel>              
                   </div>
            </div>
   </div>
</asp:Content>
