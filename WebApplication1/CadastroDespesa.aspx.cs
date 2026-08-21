using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class CadastroDespesa : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarProjetos();
            }
        }

        private void CarregarProjetos()
        {
            var listaProjetos = Repositorio.ObterDadosProjeto();

            ddlProjeto.DataSource = listaProjetos;
            ddlProjeto.DataTextField = "Titulo";
            ddlProjeto.DataValueField = "ID";
            ddlProjeto.DataBind();

            ddlProjeto.Items.Insert(0, new ListItem("-- Selecione um Projeto --", "0"));
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            pnlResultado.Visible = false;

            // 1. Validação dos campos do formulário
            if (string.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                ExibirMensagemErro("Preencha o campo Descrição!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCategoria.Text))
            {
                ExibirMensagemErro("Preencha o campo Categoria!");
                return;
            }

            if (!double.TryParse(txtValor.Text.Trim(), out double valor) || valor <= 0)
            {
                ExibirMensagemErro("Informe um valor numérico válido maior que zero!");
                return;
            }

            if (!DateTime.TryParse(txtData.Text.Trim(), out DateTime data))
            {
                ExibirMensagemErro("Informe uma data válida!");
                return;
            }

            int projetoId = Convert.ToInt32(ddlProjeto.SelectedValue);
            if (projetoId <= 0)
            {
                ExibirMensagemErro("Selecione um projeto válido!");
                return;
            }

            try
            {
                // 2. Criação e salvamento da Despesa
                Despesa despesa = new Despesa
                {
                    Descricao = txtDescricao.Text.Trim(),
                    Categoria = txtCategoria.Text.Trim(),
                    Valor = valor,
                    DataDespesa = data,
                    ProjetoID = projetoId
                };

                Repositorio.SalvarDespesa(despesa);

                // 3. Exibe sucesso e limpa tela
                pnlResultado.CssClass = "alert alert-success mt-3 shadow-sm";
                lblTitulo.Text = "Cadastro Realizado!";
                lblMensagem.Text = "A despesa foi cadastrada com sucesso.";
                pnlResultado.Visible = true;

                LimparCampos();
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("ERRO: " + ex.Message);
            }
        }

        private void ExibirMensagemErro(string detalheErro)
        {
            pnlResultado.CssClass = "alert alert-danger mt-3 shadow-sm";
            lblTitulo.Text = "Cadastro Não Realizado.";
            lblMensagem.Text = detalheErro;
            pnlResultado.Visible = true;
        }

        private void LimparCampos()
        {
            txtDescricao.Text = string.Empty;
            txtCategoria.Text = string.Empty;
            txtValor.Text = string.Empty;
            txtData.Text = string.Empty;
            ddlProjeto.SelectedIndex = 0;
        }
    }
}