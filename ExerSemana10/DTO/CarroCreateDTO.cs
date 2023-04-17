using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExerSemana10.DTO
{
    public class CarroCreateDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataLocacao { get; set; }
        public int MarcaId {get; set;}
    }
}