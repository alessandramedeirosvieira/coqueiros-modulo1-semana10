using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExerSemana10.DTO
{
    public class CarroUpDateDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataLocacao { get; set; }
        public string DescricaoCarro { get; set; }
        public int CodigoMarca { get; set; }
        public int MarcaId {get; set;}
    }
}