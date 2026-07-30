using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace WebApplication1.Models
{
    public partial class Bolsista
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public long Matricula { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }

        public static List<Bolsista> ListaBolsistas { get; set; }
        public string Resumo()
        {
            Console.WriteLine("Resumo do Bolsista: ");
            string Resumo = $"Bolsista {Nome} cadastrado!\n Matrícula: {Matricula}\n Idade: {IdadeBolsista()}";
            return Resumo;
        }     
        public int IdadeBolsista()
        {
            int Idade = DateTime.Today.Year - DataNascimento.Year;
            return Idade;
        }
    }

}
