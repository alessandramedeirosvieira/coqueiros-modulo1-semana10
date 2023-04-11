using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ExerSemana10.Model
{
    [Table ("Marca")]
    public class MarcaModel
    {
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    [NotNull]
    public string Nome { get; set; }
    }
}