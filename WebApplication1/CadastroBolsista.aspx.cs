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
        private static List<Bolsista> ListaBolsistas = new List<Bolsista>{
                new Bolsista
                {
                    Nome = "Ana Clara Silva",
                    CPF = "123.456.789-01",
                    Matricula = 202301001,
                    DataNascimento = new DateTime(2001, 3, 15),
                    Sexo = "Feminino"
                },
                new Bolsista
                {
                    Nome = "Mateus Oliveira",
                    CPF = "234.567.890-12",
                    Matricula = 202202015,
                    DataNascimento = new DateTime(1999, 11, 22),
                    Sexo = "Masculino"
                },
                new Bolsista
                {
                    Nome = "Marielle Santos",
                    CPF = "345.678.901-23",
                    Matricula = 202401042,
                    DataNascimento = new DateTime(2003, 7, 8),
                    Sexo = "Feminino"
                },
                new Bolsista
                {
                    Nome = "Lucas Tanaka",
                    CPF = "456.789.012-34",
                    Matricula = 202103008,
                    DataNascimento = new DateTime(2000, 1, 30),
                    Sexo = "Masculino"
                },
                new Bolsista
                {
                    Nome = "Beatriz Souza",
                    CPF = "567.890.123-45",
                    Matricula = 202302089,
                    DataNascimento = new DateTime(2002, 9, 12),
                    Sexo = "Feminino"
                },
                new Bolsista
                {
                    Nome = "Gabriel Costa",
                    CPF = "678.901.234-56",
                    Matricula = 202001005,
                    DataNascimento = new DateTime(1998, 5, 4),
                    Sexo = "Masculino"
                },
                new Bolsista
                {
                    Nome = "Aline Ferreira",
                    CPF = "789.012.345-67",
                    Matricula = 202402011,
                    DataNascimento = new DateTime(2004, 12, 19),
                    Sexo = "Feminino"
                },
                new Bolsista
                {
                    Nome = "Enzo Gabriel Lima",
                    CPF = "890.123.456-78",
                    Matricula = 202301077,
                    DataNascimento = new DateTime(2002, 4, 25),
                    Sexo = "Masculino"
                },
                new Bolsista
                {
                    Nome = "Camila Xavier",
                    CPF = "901.234.567-89",
                    Matricula = 202102033,
                    DataNascimento = new DateTime(1997, 8, 30),
                    Sexo = "Feminino"
                },
                new Bolsista
                {
                    Nome = "Thiago Mendes",
                    CPF = "012.345.678-90",
                    Matricula = 202201050,
                    DataNascimento = new DateTime(2001, 10, 5),
                    Sexo = "Masculino"
                }
            };

public void btnEnviar_Click(object sender, EventArgs e)
        {
            pnlCadastro.Visible = false;
            lblCadastro.Visible = false;
            lblResultado.Text = "";

            Bolsista Cadastro = new Bolsista();

            Cadastro.Nome = txtBoxNome.Text.Trim();
            //gambiarra para quando matricula nao receber valor nenhum,=.
            try
            {
                Cadastro.Matricula = long.Parse(txtBoxMatricula.Text.Trim());
            }
            catch (Exception ex)
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
                string.IsNullOrWhiteSpace(Cadastro.Matricula.ToString()) ||
                (!dataValida) || string.IsNullOrWhiteSpace(Cadastro.Sexo))
            {
                pnlCadastro.Visible = true;
                lblCadastro.Text = "Cadastro Não Realizado.";
                lblResultado.Text = "Preencha todos os campos corretamente.";
            }
            else
            {
                ListaBolsistas.Add(Cadastro);
                MontarGrid();
                //substitui a ultima ação de cadastro para redirecionamento da pagina afim de evitar cadastros duplicados ao recarregar a pagina;
                Response.Redirect("CadastroBolsista.aspx");
                pnlCadastro.Visible = true;
                lblCadastro.Text = "Cadastro Realizado!";
                lblResultado.Text = Cadastro.Resumo();
                LimparForm();

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
            gvBolsistas.DataSource = ListaBolsistas;
            gvBolsistas.DataBind();
            if (ListaBolsistas.Count() >= 1)
            {
                pnlFiltros.Visible = true;
            }

        }
        public void btn_FiltroMulheres(object Sender, EventArgs e)
        {
            gvBolsistas.DataSource = ListaBolsistas.Where(x => x.Sexo == "Feminino");
            gvBolsistas.DataBind();
        }
        
        public void btn_OrdemAlfabetica(object Sender, EventArgs e)
        {
            gvBolsistas.DataSource = ListaBolsistas.OrderBy(x => x.Nome);
            gvBolsistas.DataBind();
        }

        public void btn_FiltroOrdemDeCadastro(object Sender, EventArgs e)
        {
            MontarGrid();
        }
    }
}