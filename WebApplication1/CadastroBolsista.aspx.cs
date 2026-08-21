using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MontarGrid();
            }
        }

        public void btnEnviar_Click(object sender, EventArgs e)
        {
            pnlCadastro.Visible = false;
            lblCadastro.Visible = false;
            lblResultado.Text = "";

            Bolsista Cadastro = new Bolsista();

            try
            {
                Cadastro.Nome = txtBoxNome.Text.Trim();

                try
                {
                    Cadastro.Matricula = long.Parse(txtBoxMatricula.Text.Trim());
                }
                catch (Exception)
                {
                    Cadastro.Matricula = 0;
                }

                Cadastro.CPF = txtBoxCPF.Text.Trim();
                Cadastro.Sexo = ddlGenero.SelectedItem.Text;
                DateTime DataConvertida;
                bool dataValida = DateTime.TryParse(txtBoxDate.Text, out DataConvertida);

                if (dataValida)
                {
                    Cadastro.DataNascimento = DataConvertida;
                }

                lblCadastro.Visible = true;
                int Idade = Cadastro.IdadeBolsista();

                if (string.IsNullOrWhiteSpace(Cadastro.Nome) ||
                    string.IsNullOrWhiteSpace(Cadastro.CPF) ||
                    Cadastro.Matricula == 0 ||
                    (!dataValida) ||
                    string.IsNullOrWhiteSpace(Cadastro.Sexo))
                {
                    pnlCadastro.Visible = true;
                    pnlCadastro.CssClass = "alert alert-danger mt-3 shadow-sm"; // Altera para vermelho
                    lblCadastro.Text = "Cadastro Não Realizado.";
                    lblResultado.Text = "Preencha todos os campos corretamente.";
                }
                else
                {
                    Repositorio.SalvarBolsista(Cadastro);
                    MontarGrid();

                    pnlCadastro.Visible = true;
                    pnlCadastro.CssClass = "alert alert-success mt-3 shadow-sm"; // Altera para verde
                    lblCadastro.Text = "Cadastro Realizado!";
                    lblResultado.Text = Cadastro.Resumo();
                    LimparForm();

                    // Caso precise utilizar o Redirect sem perder o aviso, passe via QueryString/Session ou mantenha a exibição na tela.
                    // Response.Redirect("CadastroBolsista.aspx");
                }
            }
            catch (Exception ex)
            {
                pnlCadastro.Visible = true;
                pnlCadastro.CssClass = "alert alert-danger mt-3 shadow-sm"; // Altera para vermelho
                lblResultado.Text = "ERRO: " + ex.Message;
            }
        }

        public void LimparForm()
        {
            txtBoxCPF.Text = string.Empty;
            txtBoxDate.Text = string.Empty;
            txtBoxMatricula.Text = string.Empty;
            txtBoxNome.Text = string.Empty;
            ddlGenero.SelectedIndex = 0;
        }

        public void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparForm();
        }

        public void MontarGrid()
        {
            var dados = Repositorio.ObterDadosBolsista();
            gvBolsistas.DataSource = dados;
            gvBolsistas.DataBind();

            if (dados != null && dados.Count >= 1)
            {
                pnlFiltros.Visible = true;
            }
        }

        public void btn_FiltroMulheres(object Sender, EventArgs e)
        {
            gvBolsistas.DataSource = Repositorio.ObterPorSexo("Feminino");
            gvBolsistas.DataBind();
        }

        public void btn_OrdemAlfabetica(object Sender, EventArgs e)
        {
            gvBolsistas.DataSource = Repositorio.ObterOrdenadoPorNome();
            gvBolsistas.DataBind();
        }

        public void btn_FiltroOrdemDeCadastro(object Sender, EventArgs e)
        {
            MontarGrid();
        }
    }
}