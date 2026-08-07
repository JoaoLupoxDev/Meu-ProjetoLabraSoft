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
                            Visible="True"
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
                        <asp:GridView ID="gvCoordenadores" runat="server" AutoGenerateColumns="true" 
                              CssClass="table table-striped table-hover table-bordered align-middle">
                        <%--<Columns>
                            <asp:BoundField DataField="Nome" HeaderText="Nome Completo" />
                            <asp:BoundField DataField="CPF" HeaderText="CPF" />
                            <asp:BoundField DataField="Matricula" HeaderText="Matrícula" />
                            <asp:BoundField DataField="DataNascimento" HeaderText="Data de Nascimento" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                        </Columns>--%>
                        <EmptyDataTemplate>
                            <div class="text-center text-muted p-3">
                                <h3>Nenhum coordenador cadastrado no momento.</h3>
                            </div>
                        </EmptyDataTemplate>
                      </asp:GridView>              
                   </div>
            </div>
   </div>
</asp:Content>
