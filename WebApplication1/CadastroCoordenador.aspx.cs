using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class CadastroCoordenador : System.Web.UI.Page
    {
        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MontarGridCoordenadores();
            }
        }

        public void btnEnviar_Click(object sender, EventArgs e)
        {
            pnlCadastro.Visible = false;
            lblResultado.Text = "";

            Coordenador Coordenador = new Coordenador();

            Coordenador.Nome = txtBoxNome.Text.Trim();
            Coordenador.CPF = txtBoxCPF.Text.Trim();
            Coordenador.Titulacao = txtBoxTitulacao.Text.Trim();
            Coordenador.AreaAtuacao = txtBoxAreaAtuacao.Text.Trim();
            Coordenador.Email = txtBoxEmail.Text.Trim();

            // Validação de campos vazios
            if (string.IsNullOrWhiteSpace(Coordenador.Nome) ||
                string.IsNullOrWhiteSpace(Coordenador.CPF) ||
                string.IsNullOrWhiteSpace(Coordenador.Titulacao) ||
                string.IsNullOrWhiteSpace(Coordenador.AreaAtuacao) ||
                string.IsNullOrWhiteSpace(Coordenador.Email))
            {
                pnlCadastro.CssClass = "alert alert-danger mt-3 shadow-sm";
                lblResultado.Visible = true;
                lblCadastro.Visible = true;
                pnlCadastro.Visible = true;
                lblCadastro.Text = "Cadastro Não Realizado.";
                lblResultado.Text = "Preencha todos os campos corretamente.";
            }
            else
            {
                // Busca no banco via SQL se o CPF já existe
                if (Repositorio.ObterDadosCoordenador().Any(c => c.CPF == Coordenador.CPF))
                {
                    pnlCadastro.CssClass = "alert alert-danger mt-3 shadow-sm";
                    lblCadastro.Visible = true;
                    lblResultado.Visible = true;
                    pnlCadastro.Visible = true;
                    lblCadastro.Text = "Cadastro Não Realizado.";
                    lblResultado.Text = "Este CPF já foi cadastrado.";
                }
                else
                {
                    // Grava o coordenador diretamente no Banco de Dados
                    Repositorio.SalvarCoordenador(Coordenador);

                    pnlCadastro.CssClass = "alert alert-success mt-3 shadow-sm";
                    pnlFiltros.Visible = true;
                    pnlCadastro.Visible = true;
                    lblCadastro.Text = "Cadastro Realizado!";
                    lblResultado.Text = Coordenador.Resumo();

                    // Atualiza a Grid com os dados do banco
                    MontarGridCoordenadores();
                }
            }
        }

        // Eventos acionados pelas ações da GridView
        protected void gvCoordenadores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var lista = Repositorio.ObterDadosCoordenador();

                if (index >= 0 && index < lista.Count)
                {
                    Coordenador coord = lista[index];

                    // Carrega os dados no card de edição
                    hfCoordenadorID.Value = coord.ID.ToString();
                    txtEditarNome.Text = coord.Nome;
                    txtEditarEmail.Text = coord.Email;
                    txtEditarTitulacao.Text = coord.Titulacao;
                    txtEditarAreaAtuacao.Text = coord.AreaAtuacao;

                    pnlEditarCoordenador.Visible = true;
                    pnlCadastro.Visible = false;
                }
            }
            else if (e.CommandName == "Deletar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                ExecutarExclusao(id);
            }
        }

        // Botão SALVAR dentro do Card de Edição
        protected void btnSalvarEditar_Click(object sender, EventArgs e)
        {
            try
            {
                Coordenador coord = new Coordenador
                {
                    ID = Convert.ToInt32(hfCoordenadorID.Value),
                    Nome = txtEditarNome.Text.Trim(),
                    Email = txtEditarEmail.Text.Trim(),
                    Titulacao = txtEditarTitulacao.Text.Trim(),
                    AreaAtuacao = txtEditarAreaAtuacao.Text.Trim()
                };

                Repositorio.AtualizarCoordenador(coord);

                pnlEditarCoordenador.Visible = false;
                pnlCadastro.CssClass = "alert alert-success mt-3 shadow-sm";
                pnlCadastro.Visible = true;
                lblCadastro.Text = "Sucesso!";
                lblResultado.Text = "Coordenador atualizado com sucesso.";

                MontarGridCoordenadores();
            }
            catch (Exception)
            {
                pnlCadastro.CssClass = "alert alert-danger mt-3 shadow-sm";
                pnlCadastro.Visible = true;
                lblCadastro.Text = "Erro ao Atualizar!";
                lblResultado.Text = "Ocorreu uma falha ao tentar atualizar o coordenador.";
            }
        }

        // Botão EXCLUIR dentro do Card de Edição
        protected void btnExcluirEditar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(hfCoordenadorID.Value, out int id))
            {
                ExecutarExclusao(id);
            }
        }

        // Método auxiliar para centralizar a exclusão
        private void ExecutarExclusao(int id)
        {
            try
            {
                Repositorio.DeletarCoordenador(id);

                pnlEditarCoordenador.Visible = false;
                pnlCadastro.CssClass = "alert alert-success mt-3 shadow-sm";
                pnlCadastro.Visible = true;
                lblCadastro.Text = "Sucesso!";
                lblResultado.Text = "Coordenador excluído com sucesso.";

                MontarGridCoordenadores();
            }
            catch (InvalidOperationException ex)
            {
                pnlCadastro.CssClass = "alert alert-danger mt-3 shadow-sm";
                pnlCadastro.Visible = true;
                lblCadastro.Text = "Exclusão Não Permitida!";
                lblResultado.Text = ex.Message;
            }
            catch (Exception)
            {
                pnlCadastro.CssClass = "alert alert-danger mt-3 shadow-sm";
                pnlCadastro.Visible = true;
                lblCadastro.Text = "Erro ao Excluir!";
                lblResultado.Text = "Ocorreu uma falha ao tentar excluir o registro.";
            }
        }

        public void MontarGridCoordenadores()
        {
            var lista = Repositorio.ObterDadosCoordenador();

            if (lista.Count > 0)
            {
                pnlFiltros.Visible = true;
            }

            gvCoordenadores.DataSource = lista;
            gvCoordenadores.DataBind();
        }

        public void btn_Buscar(object sender, EventArgs e)
        {
            string termo = txtBoxFiltro.Text.Trim();

            var listaFiltrada = Repositorio.ObterDadosCoordenador()
                .Where(c => (c.Nome != null && c.Nome.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (c.Titulacao != null && c.Titulacao.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0))
                .ToList();

            gvCoordenadores.DataSource = listaFiltrada;
            gvCoordenadores.DataBind();
        }
    }
}