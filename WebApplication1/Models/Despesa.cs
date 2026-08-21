using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class Despesa
    {
        public int ID { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public double Valor { get; set; }
        public DateTime DataDespesa {  get; set; }
        public int ProjetoID { get; set; }

    }
}