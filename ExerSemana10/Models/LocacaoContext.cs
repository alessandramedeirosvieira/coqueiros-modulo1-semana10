using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ExerSemana10.Model
{
    public class LocacaoContext  : DbContext
    {
      public LocacaoContext(DbContextOptions<LocacaoContext> options) : base(options)
      {
      }
      public DbSet<CarroModel> carro {get; set;}
      public DbSet<MarcaModel> marca {get; set;}
      }
}