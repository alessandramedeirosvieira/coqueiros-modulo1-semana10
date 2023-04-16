using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExerSemana10.Model;

namespace ExerSemana10.DTO
{
    public class CarroDTO
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string DescricaoCarro { get; set; }
        public int CodigoMarca { get; set; }
        public DateTime DataLocacao { get; set; }
    }
}