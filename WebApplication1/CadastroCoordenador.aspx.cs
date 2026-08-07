using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class CadastroCoordenador : System.Web.UI.Page
    {
        public static List<Coordenador> ListaCoordenadores = new List<Coordenador>();
        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlCadastro.Visible = false;
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

            if (string.IsNullOrWhiteSpace(Coordenador.Nome) ||
                string.IsNullOrWhiteSpace(Coordenador.CPF) ||
                string.IsNullOrWhiteSpace(Coordenador.Titulacao) ||
                string.IsNullOrWhiteSpace(Coordenador.AreaAtuacao) || string.IsNullOrWhiteSpace(Coordenador.Email))
            {
                lblResultado.Visible = true;
                lblCadastro.Visible = true;
                pnlCadastro.Visible = true;
                lblCadastro.Text = "Cadastro Não Realizado.";
                lblResultado.Text = "Preencha todos os campos corretamente.";
            }

            else
            {
                if (ListaCoordenadores.Any(c => c.CPF == txtBoxCPF.Text))
                {
                    lblCadastro.Visible = true;
                    lblResultado.Visible= true;
                    pnlCadastro.Visible = true;
                    lblCadastro.Text = "Cadastro Não Realizado.";
                    lblResultado.Text = "Este CPF já foi cadastrado.";
                }
                else
                {
                    ListaCoordenadores.Add(Coordenador);

                    pnlFiltros.Visible = true;
                    lblCadastro.Text = "Cadastro Realizado! ";
                    lblResultado.Text = Coordenador.Resumo();
                    MontarGridCoordenadores();
                    pnlFiltros.Visible = true;

                }

            }

        }

        public void MontarGridCoordenadores()
        {
            if (ListaCoordenadores.Count > 0)
            {
                pnlCadastro.Visible = true;
                pnlFiltros.Visible = true;
            }
            gvCoordenadores.DataSource = ListaCoordenadores;
            gvCoordenadores.DataBind();
        }

        public void btn_Buscar(object sender, EventArgs e)
        {
            gvCoordenadores.DataSource = ListaCoordenadores.Where(c => c.Nome.Contains(txtBoxFiltro.Text) || c.Titulacao.Contains(txtBoxFiltro.Text));
            gvCoordenadores.DataBind();
        }
    }
}