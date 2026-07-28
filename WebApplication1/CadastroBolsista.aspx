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
                <form>
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
                        <asp:TextBox runat="server" ID="txtBoxDate" TextMode="Date" type="date" CssClass="form-control" placeholder="Digite o a sua data de nascimento"/>
                    </div>
                    <label>Selecione o seu genero:</label>
                    <asp:DropDownList runat="server" ID="ddlGenero">
                        <asp:ListItem value="Masculino">Masculino</asp:ListItem>
                        <asp:ListItem value="Feminino">Feminino</asp:ListItem>
                        <asp:ListItem value="Não-Binário">Não-binário</asp:ListItem>
                        <asp:ListItem value="Prefiro não dizer">Prefiro não dizer</asp:ListItem>
                    </asp:DropDownList>
                    <hr />
                    <asp:Button runat="server" ID="btnEnviar" CssClass="btn btn-success" OnClick="btnEnviar_Click" Text="Salvar"/>
                    <asp:Button runat="server" ID="btnLimpar" CssClass="btn btn-success" OnClick="btnLimpar_Click" Text="Limpar"/>
                        <asp:Panel
                            ID="pnlCadastro"
                            runat="server"
                            Visible="false"
                            CssClass="alert alert-success mt-3 shadow-sm">

                            <asp:Label ID="lblCadastro" runat="server" Text="Cadastro Realizado!" CssClass="fw-bold"></asp:Label>
                            <br />

                            <asp:Label ID="lblResultado" runat="server" />
                        </asp:Panel>
                        
                </form>
            </div>
        </div>
    </div>
    
    <div class="container mt-5">
        <div class="card shadow">
            <div class="card-header bg-primary text-white">
                <h2 class="mb-0">Bolsistas Cadastrados</h2>
            </div>
            <div class="card-body">
                    <div class="card-body table-responsive">
                        <asp:GridView ID="gvBolsistas" runat="server" AutoGenerateColumns="True" 
                              CssClass="table table-striped table-hover table-bordered align-middle">
                        <Columns>
                            <asp:BoundField DataField="Nome" HeaderText="Nome Completo" />
                            <asp:BoundField DataField="CPF" HeaderText="CPF" />
                            <asp:BoundField DataField="Matricula" HeaderText="Matrícula" />
                            <asp:BoundField DataField="DataNascimento" HeaderText="Data de Nascimento" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="text-center text-muted p-3">
                                Nenhum bolsista cadastrado no momento.
                            </div>
                        </EmptyDataTemplate>
                      </asp:GridView>              
                   </div>
            </div>
   </div>


</asp:Content>
