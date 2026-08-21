using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class Coordenador
    {
        public int ID { get; set; }
        public string Nome {  get; set; }
        public string CPF { get; set; }
        public string Titulacao { get; set; }
        public string AreaAtuacao { get; set; }
        public string Email { get; set; }

        public string Resumo()
        {
            string Resumo = $"Coordenador {Nome} cadastrado!\n Área de Atuação: {AreaAtuacao}\n Email: {Email}\n Titulação: {Titulacao}\n CPF: {CPF}";
            return Resumo;
        }
    }
}