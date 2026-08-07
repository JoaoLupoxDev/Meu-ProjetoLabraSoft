using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class Projeto
    {
        public string Titulo { get; set; }
        public double Verba { get; set; }
        public double ValorBolsa { get; set; }
        public string AreaConhecimento { get; set; }
        public List<Bolsista> Bolsistas { get; set; } = new List<Bolsista>();
        public Coordenador Coordenador { get; set; }

        public Projeto()
        {

        }
    }
}