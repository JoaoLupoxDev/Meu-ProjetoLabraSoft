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
                if (Repositorio.ContarBolsistas() == 0)
                {
                    lblBolsistasEmpty.Visible = true;
                }
                if (Repositorio.ObterDadosCoordenador().Count == 0)
                {
                    lblCoordenadoresEmpty.Visible = true;
                }
                pnlSalvar.Visible = false;

                // Carrega DropDown list de Coordenadores
                ddlCoordenadores.DataSource = Repositorio.ObterDadosCoordenador();
                ddlCoordenadores.DataTextField = "Nome";
                ddlCoordenadores.DataValueField = "CPF";
                ddlCoordenadores.DataBind();

                // Carrega apenas bolsistas sem nenhum projeto vinculado
                CarregarBolsistasDisponiveis();

                MontarGrid();
            }
        }

        public void btnEnviar_Click(object sender, EventArgs e)
        {
            pnlSalvar.Visible = false;
            string CpfSelecionado = ddlCoordenadores.SelectedValue;
            Coordenador ObjCoordenador = Repositorio.ObterDadosCoordenador().FirstOrDefault(x => x.CPF == CpfSelecionado);

            // Criação do objeto da classe Projeto
            Projeto ObjProjeto = new Projeto();
            ObjProjeto.AreaConhecimento = txtBoxAreaConhecimento.Text.Trim();

            double.TryParse(txtBoxValorBolsa.Text.Trim(), out double ValorBolsa);
            ObjProjeto.ValorBolsa = ValorBolsa;

            ObjProjeto.Titulo = txtBoxTitulo.Text.Trim();

            double.TryParse(txtBoxVerba.Text.Trim(), out double VerbaDouble);
            ObjProjeto.Verba = VerbaDouble;

            ObjProjeto.Coordenador = ObjCoordenador;

            // Validação dos campos antes de salvar no banco
            if (string.IsNullOrWhiteSpace(ObjProjeto.Titulo) ||
                string.IsNullOrWhiteSpace(ObjProjeto.AreaConhecimento) ||
                ObjProjeto.ValorBolsa <= 0 ||
                ObjProjeto.Verba <= 0)
            {
                pnlSalvar.Visible = true;
                pnlSalvar.CssClass = "alert alert-danger mt-3 shadow-sm";
                lblSalvar.Text = "Cadastro não realizado!";
                lblResultado.Text = "Preencha todos os campos corretamente.";
            }
            else
            {
                // Salva o projeto e recupera o ID gerado
                int projetoIdGerado = Repositorio.SalvarProjeto(ObjProjeto);

                // Percorre apenas os bolsistas selecionados com ID válido (> 0)
                foreach (ListItem item in lstBoxBolsistas.Items)
                {
                    if (item.Selected)
                    {
                        if (int.TryParse(item.Value, out int bolsistaId) && bolsistaId > 0)
                        {
                            Repositorio.SalvarProjetoBolsista(projetoIdGerado, bolsistaId);
                        }
                    }
                }

                pnlSalvar.CssClass = "alert alert-success mt-3 shadow-sm";
                pnlSalvar.Visible = true;
                lblSalvar.Text = "Cadastro realizado!";
                lblResultado.Text = string.Empty;

                // Recarrega a lista do formulário para remover os bolsistas recém-vinculados
                CarregarBolsistasDisponiveis();

                MontarGrid();
            }
        }

        public void MontarGrid()
        {
            gvProjetos.DataSource = Repositorio.ObterDadosProjeto();
            gvProjetos.DataBind();
        }

        public void gvProjetos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalhes")
            {
                pnlEditarProjeto.Visible = false;
                int indice = Convert.ToInt32(e.CommandArgument);
                Projeto ProjetoSelecionado = Repositorio.ObterDadosProjeto()[indice];

                if (ProjetoSelecionado != null)
                {
                    lblDetalheTitulo.Text = ProjetoSelecionado.Titulo;
                    lblDetalheArea.Text = ProjetoSelecionado.AreaConhecimento;
                    lblDetalheVerba.Text = ProjetoSelecionado.Verba.ToString("N2");
                    lblDetalheValorBolsa.Text = ProjetoSelecionado.ValorBolsa.ToString("N2");
                    lblDetalheCoordenador.Text = ProjetoSelecionado.Coordenador != null
                        ? ProjetoSelecionado.Coordenador.Nome
                        : "Não Informado";

                    List<Bolsista> bolsistasDoBanco = Repositorio.ObterBolsistasPorProjeto(ProjetoSelecionado.ID);

                    if (bolsistasDoBanco.Count > 0)
                    {
                        rptBolsistas.DataSource = bolsistasDoBanco;
                        rptBolsistas.DataBind();
                        rptBolsistas.Visible = true;
                        lblSemBolsistas.Visible = false;
                    }
                    else
                    {
                        rptBolsistas.Visible = false;
                        lblSemBolsistas.Visible = true;
                    }

                    pnlDetails.Visible = true;
                }
            }
            else if (e.CommandName == "Editar")
            {
                pnlDetails.Visible = false;
                int indice = Convert.ToInt32(e.CommandArgument);
                Projeto proj = Repositorio.ObterDadosProjeto()[indice];

                if (proj != null)
                {
                    hfProjetoID.Value = proj.ID.ToString();
                    lblNomeProjetoEditar.Text = proj.Titulo;

                    // Alimenta o ListBox do painel de edição apenas com bolsistas livres + os cadastrados neste projeto
                    lstEditarBolsistas.DataSource = Repositorio.ObterBolsistasDisponiveis(proj.ID);
                    lstEditarBolsistas.DataTextField = "Nome";
                    lstEditarBolsistas.DataValueField = "ID";
                    lstEditarBolsistas.DataBind();

                    // Marca os bolsistas que já estão vinculados a este projeto
                    List<Bolsista> bolsistasVinculados = Repositorio.ObterBolsistasPorProjeto(proj.ID);
                    foreach (ListItem item in lstEditarBolsistas.Items)
                    {
                        item.Selected = bolsistasVinculados.Any(b => b.ID.ToString() == item.Value);
                    }

                    pnlEditarProjeto.Visible = true;
                }
            }
        }

        // SALVA AS ALTERAÇÕES DE BOLSISTAS (ADICIONAR/REMOVER)
        protected void btnSalvarEditar_Click(object sender, EventArgs e)
        {
            try
            {
                int projetoId = Convert.ToInt32(hfProjetoID.Value);

                List<int> idsBolsistasSelecionados = new List<int>();
                foreach (ListItem item in lstEditarBolsistas.Items)
                {
                    if (item.Selected && int.TryParse(item.Value, out int bId))
                    {
                        idsBolsistasSelecionados.Add(bId);
                    }
                }

                // Atualiza a tabela associativa ProjetoBolsista
                Repositorio.AtualizarBolsistasDoProjeto(projetoId, idsBolsistasSelecionados);

                pnlEditarProjeto.Visible = false;
                pnlSalvar.CssClass = "alert alert-success mt-3 shadow-sm";
                pnlSalvar.Visible = true;
                lblSalvar.Text = "Sucesso!";
                lblResultado.Text = "Bolsistas do projeto atualizados com sucesso.";

                // Atualiza o ListBox do formulário de novo cadastro
                CarregarBolsistasDisponiveis();

                MontarGrid();
            }
            catch (Exception)
            {
                pnlSalvar.CssClass = "alert alert-danger mt-3 shadow-sm";
                pnlSalvar.Visible = true;
                lblSalvar.Text = "Erro ao Atualizar!";
                lblResultado.Text = "Ocorreu uma falha ao tentar atualizar os bolsistas do projeto.";
            }
        }

        // EXCLUI O PROJETO INTEIRO DENTRO DO CARD DE EDIÇÃO
        protected void btnExcluirEditar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(hfProjetoID.Value, out int id))
            {
                try
                {
                    Repositorio.DeletarProjeto(id);

                    pnlEditarProjeto.Visible = false;
                    pnlSalvar.CssClass = "alert alert-success mt-3 shadow-sm";
                    pnlSalvar.Visible = true;
                    lblSalvar.Text = "Sucesso!";
                    lblResultado.Text = "Projeto excluído com sucesso.";

                    // Atualiza a lista de bolsistas disponíveis (pois os bolsistas do projeto excluído foram liberados)
                    CarregarBolsistasDisponiveis();

                    MontarGrid();
                }
                catch (InvalidOperationException ex)
                {
                    pnlSalvar.CssClass = "alert alert-danger mt-3 shadow-sm";
                    pnlSalvar.Visible = true;
                    lblSalvar.Text = "Exclusão Não Permitida!";
                    lblResultado.Text = ex.Message;
                }
                catch (Exception)
                {
                    pnlSalvar.CssClass = "alert alert-danger mt-3 shadow-sm";
                    pnlSalvar.Visible = true;
                    lblSalvar.Text = "Erro ao Excluir!";
                    lblResultado.Text = "Ocorreu uma falha ao tentar excluir o projeto.";
                }
            }
        }

        protected void btnCancelarEditar_Click(object sender, EventArgs e)
        {
            pnlEditarProjeto.Visible = false;
        }

        public void btnFecharDetalhes_Click(object sender, EventArgs e)
        {
            pnlDetails.Visible = false;
        }

        private void CarregarBolsistasDisponiveis()
        {
            lstBoxBolsistas.DataSource = Repositorio.ObterBolsistasDisponiveis();
            lstBoxBolsistas.DataTextField = "Nome";
            lstBoxBolsistas.DataValueField = "ID";
            lstBoxBolsistas.DataBind();
        }
    }
}