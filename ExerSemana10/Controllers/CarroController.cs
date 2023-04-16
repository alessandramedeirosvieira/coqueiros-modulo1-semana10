using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExerSemana10.DTO;
using ExerSemana10.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ExerSemana10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarroController : ControllerBase
    {
//variavel do tipo leitura
        private readonly LocacaoContext locacaoContext;

//CTOR com parametros (injetado dentro dos parenteses o context)
//se eu tenho uma injecao tenho uma var de leitura acima e injetado do tipo context, dentro do ctor , faço a injecao de dependencia
        public CarroController(LocacaoContext locacaoContext)
        {
            this.locacaoContext = locacaoContext;
        }
//pensando no CRUD, se não tenho nada no BD, a ordem de execução é POST (insercao), PUT (atual.), DELETE e GET
//se já tiver dados dentro do BD, faço primeiro o GET
        
        [HttpPost]
        public ActionResult Inserir([FromBody] CarroDTO carroDTO)
        {
            if (carroDTO == null)
            {
                return BadRequest("Precisa inserir dados.");
            }
            else
            {
                if (carroDTO.Codigo != 0)
                {
                    return BadRequest("Código deve ser igual a zero(0).");
                }
                else
                {
                    foreach(var item in locacaoContext.marca)
                    {
                        if (item.Id == carroDTO.CodigoMarca)
                        {
                            CarroModel carroModel = new CarroModel()
                            {
                                Id = carroDTO.Codigo,
                                Nome = carroDTO.DescricaoCarro,
                                DataLocacao = carroDTO.DataLocacao,
                                Marca = item
                            };
                            locacaoContext.carro.Add(carroModel);
                            locacaoContext.SaveChanges();
                            return Ok("Salvo com sucesso.");
                        }
                        else { return BadRequest("Código de marca não encontrado."); }
                    }
                }
            }
            return Ok();
        }
    }
}
