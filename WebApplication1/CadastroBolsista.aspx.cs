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
                gvBolsistas.DataSource = ListaBolsistas;
                gvBolsistas.DataBind();
            }
        }
        private static List<Bolsista> ListaBolsistas = new List<Bolsista>();

        public void btnEnviar_Click(object sender, EventArgs e)
        {
            pnlCadastro.Visible = false;
            lblCadastro.Visible = false;
            lblResultado.Text = "";

            Bolsista Cadastro = new Bolsista();

            Cadastro.Nome = txtBoxNome.Text.Trim();
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
            DateTime DataNascimento;
            bool dataValida = DateTime.TryParse(txtBoxDate.Text, out DataNascimento);
            lblCadastro.Visible = true;
            int Idade = Cadastro.IdadeBolsista();
            ListaBolsistas.Add(Cadastro);

            if (string.IsNullOrWhiteSpace(Cadastro.Nome) ||
                string.IsNullOrWhiteSpace(Cadastro.CPF) ||
                string.IsNullOrWhiteSpace(Cadastro.Matricula.ToString()) ||
                (!dataValida) || string.IsNullOrWhiteSpace(Cadastro.Sexo))
            {
                lblResultado.Visible = true;
                pnlCadastro.Visible = true;
                lblCadastro.Text = "Cadastro Não Realizado.";
                lblResultado.Text = "Preencha todos os campos corretamente.";
            }
            else
            {
                gvBolsistas.DataSource = ListaBolsistas;
                gvBolsistas.DataBind();
                pnlCadastro.Visible = true;
                lblCadastro.Text = "Cadastro Realizado!";
                lblResultado.Visible = true;
                lblResultado.Text = $"Bolsista {Cadastro.Nome} cadastrado!\n Matrícula: {Cadastro.Matricula}\n Idade: {Idade}";
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
        
    }
}