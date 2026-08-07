using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Repositorio
    {
        public static List<Bolsista> Bolsistas { get; set; } = new List<Bolsista>();

        public static List<Coordenador> Coordenadores = new List<Coordenador>();

        public static List<Projeto> Projetos = new List<Projeto>();

    }
}