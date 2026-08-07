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
                    Bolsista.ListaBolsistas.Add(new Bolsista { Nome = "João Silva", Matricula = 12345, CPF = "111.111.111-11" });
                    Bolsista.ListaBolsistas.Add(new Bolsista { Nome = "Maria Souza", Matricula = 67890, CPF = "222.222.222-22" });
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
            //if (string.IsNullOrWhiteSpace() ||
            //    string.IsNullOrWhiteSpace(Cadastro.CPF) ||
            //    string.IsNullOrWhiteSpace(Cadastro.Matricula.ToString()) ||
            //    (!dataValida) || string.IsNullOrWhiteSpace(Cadastro.Sexo))
            //{

            //}
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
            Repositorio.Projetos.Add(ObjProjeto);
            MontarGrid();
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

                }
            }
        }
    }
}