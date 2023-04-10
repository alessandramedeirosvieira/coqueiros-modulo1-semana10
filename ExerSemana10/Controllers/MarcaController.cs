using System.Runtime.CompilerServices;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExerSemana10.DTO;
using ExerSemana10.Model;
using Microsoft.AspNetCore.Mvc;

namespace ExerSemana10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcaController : ControllerBase
    {
        private readonly LocacaoContext locacaoContext;

        public MarcaController(LocacaoContext locacaoContext)
        {
            this.locacaoContext = locacaoContext;
        }
    }
}
