using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExerSemana10.DTO;
using ExerSemana10.Model;
using Microsoft.AspNetCore.Mvc;
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
//se eu uma injecao tenho uma var de leitura acima e injetado do tipo context, dentro do ctor , faço a injecao de dependencia
        public CarroController(LocacaoContext locacaoContext)
        {
            this.locacaoContext = locacaoContext;
        }
//pensando no CRUD, se não tenho nada no BD, a ordem é POST (insercao), PUT (atual.), DELETE e GET
//se já tiver dados dentro do BD, faço primeiro o GET
    [HttpPost]
        public ActionResult <CarroCreateDTO> Post([FromBody] CarroCreateDTO carroDTO)
        {
//instanciar minha carroModel
//passar meus parametros para a inst criada no metodo post
//id não
CarroModel carroModel = new CarroModel();
carroModel.Nome = carroDTO.Nome;
carroModel.DataLocacao = carroDTO.DataLocacao;
carroModel.MarcaId = carroDTO.MarcaId;
carroModel.Marca = carroDTO.Marca;

//verificar se existe Marca no Bd
var marcaModel = locacaoContext.Marca.Find(carroDTO.MarcaId);

if(marcaModel != null){

//add a model dentro do DBset no meu context
            locacaoContext.Carro.Add(carroModel);
//depois de add na lista do DBSet, preciso salvar no BD
            locacaoContext.SaveChanges();
//depois de salvar em passo o meu id
            carroDTO.Id = carroModel.Id;
             return Ok(carroDTO);
}
else{
//se for null retorno um request de erro
            return BadRequest ("Erro ao atualizar o registro");
    }
        }
    }
}
