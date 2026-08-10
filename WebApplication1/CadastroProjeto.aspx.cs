using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class CadastroProjeto : System.Web.UI.Page
    {
        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Bolsista.ListaBolsistas.Count == 0) //para fins de teste
                {
                    Bolsista.ListaBolsistas.Add(new Bolsista { Nome = "João Silva", Matricula = 12345, CPF = "111.111.111-11", Sexo = "Masculino" });
                    Bolsista.ListaBolsistas.Add(new Bolsista { Nome = "Maria Souza", Matricula = 67890, CPF = "222.222.222-22", Sexo = "Feminino"});
                }

                if (CadastroCoordenador.ListaCoordenadores.Count == 0) //para fins de teste
                {
                    CadastroCoordenador.ListaCoordenadores.Add(new Coordenador { Nome = "Prof. Carlos", CPF = "333.333.333-33" });
                    CadastroCoordenador.ListaCoordenadores.Add(new Coordenador { Nome = "Profa. Ana", CPF = "444.444.444-44" });
                }
                pnlSalvar.Visible = false;
                ddlCoordenadores.DataSource = CadastroCoordenador.ListaCoordenadores;
                ddlCoordenadores.DataTextField = "Nome";
                ddlCoordenadores.DataValueField= "CPF";
                ddlCoordenadores.DataBind();

                lstBoxBolsistas.DataSource = Bolsista.ListaBolsistas;
                lstBoxBolsistas.DataTextField = "Nome";
                lstBoxBolsistas.DataValueField = "Matricula";
                lstBoxBolsistas.DataBind();

                MontarGrid();
            }
        } 

        public void btnEnviar_Click(object sender, EventArgs e)
        {
         
            string CpfSelecionado = ddlCoordenadores.SelectedValue;
            Coordenador ObjCoordenador = CadastroCoordenador.ListaCoordenadores.FirstOrDefault(x => x.CPF == CpfSelecionado);
            //criação do objeto da classe Projeto
            Projeto ObjProjeto = new Projeto();
            ObjProjeto.AreaConhecimento = txtBoxAreaConhecimento.Text.Trim();     
            try
            {
                double.TryParse(txtBoxValorBolsa.Text.Trim(), out double ValorBolsa);
                ObjProjeto.ValorBolsa = ValorBolsa;
            }
            catch
            {
                ObjProjeto.ValorBolsa = 0;
            }
            ObjProjeto.Titulo = txtBoxTitulo.Text.Trim();
            try
            {
                double.TryParse(txtBoxVerba.Text.Trim(), out double VerbaDouble);
                ObjProjeto.Verba = VerbaDouble;
            }
            catch
            {
                ObjProjeto.Verba = 0;
            }
            ObjProjeto.Coordenador = ObjCoordenador;        
            foreach (ListItem item in lstBoxBolsistas.Items)
            {
                if (item.Selected)
                {
                    if (long.TryParse(item.Value, out long matricula))
                    {
                        Bolsista BolsistaEncontrado = Bolsista.ListaBolsistas.FirstOrDefault(b => b.Matricula == matricula);
                        if (BolsistaEncontrado != null)
                        {
                            ObjProjeto.Bolsistas.Add(BolsistaEncontrado);
                        }
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(ObjProjeto.Titulo) ||
               string.IsNullOrWhiteSpace(ObjProjeto.AreaConhecimento) ||
               string.IsNullOrWhiteSpace(ObjProjeto.ValorBolsa.ToString()) ||
               string.IsNullOrWhiteSpace(ObjProjeto.Verba.ToString()) || ObjProjeto.Bolsistas == null ) //nao colocquei para caso o coordenador esteja vazio pois nao tem como nao selecionar um coordenador
            {
                pnlSalvar.Visible = true;
                pnlSalvar.CssClass = 
                lblSalvar.Text = "Cadastro não realizado!";
                lblResultado.Text = "Preencha todos os campos corretamente.";
                pnlSalvar.CssClass = "alert alert-danger mt-3 shadow-sm";
            }
            else
            {
                pnlSalvar.CssClass = "alert alert-success mt-3 shadow-sm";
                pnlSalvar.Visible = true;
                lblSalvar.Text = "Cadastro realizado!";
                Repositorio.Projetos.Add(ObjProjeto);
                MontarGrid();
            }
        }
         public void lstBoxBolsistas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void MontarGrid()
        {
            gvProjetos.DataSource = Repositorio.Projetos;
            gvProjetos.DataBind();
        }
        public void btn_Detalhes(object sender, EventArgs e)
        {

        }
        public void gvProjetos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalhes")
            {
                int indice = Convert.ToInt32(e.CommandArgument);
                Projeto ProjetoSelecionado = Repositorio.Projetos[indice];

                if (ProjetoSelecionado != null)
                {
                    // Preenche as informações gerais do projeto nos Labels
                    lblDetalheTitulo.Text = ProjetoSelecionado.Titulo;
                    lblDetalheArea.Text = ProjetoSelecionado.AreaConhecimento;
                    lblDetalheVerba.Text = ProjetoSelecionado.Verba.ToString("N2");
                    lblDetalheValorBolsa.Text = ProjetoSelecionado.ValorBolsa.ToString("N2");
                    lblDetalheCoordenador.Text = ProjetoSelecionado.Coordenador != null
                        ? ProjetoSelecionado.Coordenador.Nome
                        : "Não Informado";

                    // Alimenta o Repeater de Bolsistas
                    if (ProjetoSelecionado.Bolsistas != null && ProjetoSelecionado.Bolsistas.Count > 0)
                    {
                        rptBolsistas.DataSource = ProjetoSelecionado.Bolsistas;
                        rptBolsistas.DataBind();
                        rptBolsistas.Visible = true;
                        lblSemBolsistas.Visible = false;
                    }
                    else
                    {
                        rptBolsistas.Visible = false;
                        lblSemBolsistas.Visible = true;
                    }

                    // Exibe o painel de detalhes
                    pnlDetails.Visible = true;
                }
            }
        }
        public void btnFecharDetalhes_Click(object sender, EventArgs e)
        {
            pnlDetails.Visible = false;
        }
    }
}