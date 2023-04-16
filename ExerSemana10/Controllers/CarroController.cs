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
        [HttpGet]
        public ActionResult<List<CarroDTO>> Get()
        {
        var listaCarroModel = locacaoContext.carro.Include(c => c.MarcaModel);

        List<CarroDTO> listaCarrosDto = new();

        foreach (var carro in listaCarroModel)
        {
            var carroDto = new CarroDTO();

            carroDto.Codigo = carro.Id;
            carroDto.Nome = carro.Nome;
            carroDto.CodigoMarca = carro.MarcaId;
            listaCarrosDto.Add(carroDto);
        }

        return Ok(listaCarrosDto);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetPorId([FromRoute]int id)
        {
        var carroModel = locacaoContext.carro.Include(c => c.MarcaModel).FirstOrDefault(x => x.Id == id);

        CarroDTO carroDto = new();
        if (carroModel.Id == null)
        {
            BadRequest("Carro não encontrado");
        }

        carroDto.Codigo = carroModel.Id;
        carroDto.Nome = carroModel.Nome;
        carroDto.CodigoMarca = carroModel.MarcaId;

        return Ok(carroDto);
        }
        [HttpPost]
        public ActionResult Post([FromBody] CarroDTO carroDto)
        {
        CarroModel carroModel = new();
        MarcaModel marcaModel = locacaoContext.marca.Find(carroDto.CodigoMarca);

        if (marcaModel == null)
        {
            return NotFound("Marca não encontrada");
        }

        carroModel.Id = carroDto.Codigo;
        carroModel.Nome = carroDto.Nome;
        carroModel.MarcaId = marcaModel.Id;

        locacaoContext.Add(carroModel);
        locacaoContext.SaveChanges();

        return Ok("Carro salvo");
        }
        [HttpPut]
        public ActionResult Put([FromBody] CarroDTO carroDto)
        {
        CarroModel carroModel = locacaoContext.carro.Find(carroDto.Codigo);
        MarcaModel marcaModel = locacaoContext.marca.Find(carroDto.CodigoMarca);

        if (marcaModel == null)
        {
            return NotFound("Marca não encontrada");
        }

        if (carroModel == null)
        {
            return NotFound("Carro não encontrado");
        }

        carroModel.Id = carroDto.Codigo;
        carroModel.Nome = carroDto.Nome;
        carroModel.MarcaId = marcaModel.Id;

        locacaoContext.Attach(carroModel);
        locacaoContext.SaveChanges();

        return Ok("Carro atualizado");
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
        CarroModel carroModel = locacaoContext.carro.Find(id);

        if (carroModel != null)
        {
            locacaoContext.Remove(carroModel);
            locacaoContext.SaveChanges();

            return Ok("Carro deletado");
        }

        return BadRequest("Carro não localizado");
        }
    }
}
