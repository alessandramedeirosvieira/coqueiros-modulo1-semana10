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
    public class CarroController : ControllerBase
    {
        private readonly LocacaoContext locacaoContext1;
        private LocacaoContext locacaoContext;

        public CarroController (LocacaoContext locacaoContext)
        {
           this.locacaoContext = locacaoContext;
        }

    [HttpPost]
    public ActionResult<CarroCreateDTO> Post ([FromBody] CarroCreateDTO carroDTO)
    {
        //instanciando a model
        //passar meus parametros
        CarroModel carroModel = new CarroModel ();
        carroModel.DataLocacao = carroDTO.DataLocacao;
        carroModel.Nome = carroDTO.Nome;
        carroModel.MarcaId = carroDTO.MarcaId;

        //fazer validação (verificar se existe a marca no BD)
        var marcaModel = locacaoContext.marca.Find(carroDTO.MarcaId);
        if(marcaModel != null)
        {
            //add no DBset
            locacaoContext.carro.Add(carroModel);

             //salvar
            locacaoContext.SaveChanges();

            carroDTO.Id = carroModel.Id;
            return Ok (carroModel);
        }
        else
        {
            return BadRequest ("erro ao salvar o carro no Banco de Dados");
        }
    }

    [HttpPut]
    public ActionResult Put([FromBody] CarroUpDateDTO carroUpDateDTO)
    {
       //carro id tem que ter cadastro
       //codigo marca não pode ser nulo
       //marcaid não tem que estar cadastrado
        CarroModel carroModel = locacaoContext.carro.Find(carroUpDateDTO.Id);
        MarcaModel marcaModel = locacaoContext.marca.Find(carroUpDateDTO.CodigoMarca);

        if (marcaModel == null)
        {
            return NotFound("Marca não encontrada");
        }

        if (carroModel == null)
        {
            return NotFound("Carro não encontrado");
        }

        carroModel.Id = carroUpDateDTO.Id;
        carroModel.Nome = carroUpDateDTO.Nome;
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

            return Ok("Carro removido");
        }

        return BadRequest("Carro não cadastrado");
    }

    [HttpGet]
    public ActionResult<List<CarroGetDTO>> Get()
    {
       var ListCarroModel = locacaoContext.carro;
            //inst
            //tipo de obj devolvido (lista), preciso percorrer a model
            List<CarroGetDTO> listaGetDTO = new List<CarroGetDTO>();

            foreach (var item in ListCarroModel)
            {
                var carroGetDTO = new CarroGetDTO();
                carroGetDTO.Id = item.Id;
                carroGetDTO.Nome = item.Nome;

                listaGetDTO.Add(carroGetDTO);
            }
            return Ok(listaGetDTO);
    }

     [HttpGet("{id}")]
        public ActionResult<CarroGetDTO> Get([FromRoute] int id)
        {
            //Buscar o registro no BD por Id
            //var carroModel = locacaoContext.carro.Find(id);
            var carroModel = locacaoContext.carro.Where(w => w.Id == id).FirstOrDefault();

            if (carroModel == null)
            {
                return BadRequest("Dados não encontrados no BD");
            }

            //Modificar de carroModel para carroGetDTO
            CarroGetDTO carroGetDTO = new CarroGetDTO();
            carroGetDTO.Id = carroModel.Id;
            carroGetDTO.Nome = carroModel.Nome;
            return Ok(carroGetDTO);
        }
    }
}