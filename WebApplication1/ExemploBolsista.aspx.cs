
using System;
using System.Web.UI;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class ExemploBolsista: System.Web.UI.Page
    {
        public void Page_Load(object sender, EventArgs e)
        {

            Bolsista AlunoTeste = new Bolsista();

            AlunoTeste.Nome = "João Vitor";
            AlunoTeste.Matricula = 2026013331;
            AlunoTeste.CPF = "293.414.314-32";
            AlunoTeste.Sexo = "Masculino";
            AlunoTeste.DataNascimento = new DateTime(2007, 05, 09);

            string resultado = $"Nome: {AlunoTeste.Nome}\n Matricula: {AlunoTeste.Matricula}\n CPF: {AlunoTeste.CPF}\n Sexo: {AlunoTeste.Sexo}\n Idade: {AlunoTeste.IdadeBolsista()}";

            lblResultado.Text = resultado;

        }
    }
}