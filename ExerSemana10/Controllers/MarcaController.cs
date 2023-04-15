using System.Net;
using System.ComponentModel.DataAnnotations.Schema;
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
        //para especificar o tipo de objeto a ser devolvido sinalizo com <> depois do Post
        //aqui estamos recebendo e devolvendo um objeto (nome)
        [HttpPost]
        public ActionResult Post ([FromBody] MarcaCreateDTO marcaCreateDTO)
        {
            //extrair da MarcaDTO para MarcaModel
            //num primeiro model não preencher o Id da Model
            MarcaModel model = new MarcaModel();
            model.Nome = marcaCreateDTO.Nome;
            //DBset é uma lista, informa a propriedade do DBset e adiciona a model
            locacaoContext.marca.Add(model);

            //salvar no BD
            locacaoContext.SaveChanges();
            //marcaDTO que seu ID é chamado de codigo = modelId
            // marcaDTO.Codigo = model.Id;
            //quando vou adicionando o ID, por conta do migration já adiciona +1 aut
            //devolver informações que o cliente precisa saber (DTO)
            return Ok(marcaCreateDTO);
        }
        [HttpPut]
        public ActionResult Put ([FromBody] MarcaUpDateDTO marcaUpDateDTO)
        {
            //primeira coisa: buscar por id no BD
            //é uma lista, faz where
            //não é performatico, porque carrega todos os dados em memoria (dai tem que configura, outra alternativa)
            //o EF com uma massa de dados muito grande não é legal (procurar alternativas)
            var marcaModel = locacaoContext.marca.Where(x => x.Id == marcaUpDateDTO.Codigo).FirstOrDefault();

            //segunda ação: verificar se não é null
            if (marcaModel !=null)
            {
                marcaModel.Id = marcaUpDateDTO.Codigo;
                marcaModel.Nome = marcaUpDateDTO.Nome;
                //se for diferente de null, atualizar variavel (atribuir valor)
                //modifica no BD (dixa e depois salva)
                locacaoContext.marca.Attach(marcaModel);

                locacaoContext.SaveChanges();
                //o retorno fica aqui, para validar o sucesso da condição
                return Ok(marcaUpDateDTO);
            }
            else
            {
                //se for null retorna uma request de erro
                //não precisa especificar o erro
                //devolve o status 400 
                return BadRequest("erro ao atualizar o registro");
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete ([FromRoute] int id)
        {
            //verificar se existe registro no BD
            var marcaModel = locacaoContext.marca.Find(id);

            //verificar se o registro está diferente de null
            if (marcaModel != null)
            {
                //deletar o registro do BD
                //locacaoContext.Remove(marcaModel);
                locacaoContext.marca.Remove(marcaModel);

                locacaoContext.SaveChanges();
            return Ok();
            }
            else
            {
                //se for null retorna uma request de erro
                //não precisa especificar o erro
                //devolve o status 400 
                return BadRequest("erro ao atualizar o registro");
            }
        }
        [HttpGet]
        public ActionResult<List<MarcaGetDTO>> Get()
        {
            //ActionResult sempre devolve alguma coisa
            //preciso pegar todos os meus dados do BD (=significa recebo)
            //alocando memoria dos dados no context GET
            var ListMarcaModel = locacaoContext.marca;
            //inst
            //tipo de obj devolvido (lista), preciso percorrer a model
            List<MarcaGetDTO> listaGetDTO = new List<MarcaGetDTO>();

            foreach (var item in ListMarcaModel)
            {
                var marcaGetDTO = new MarcaGetDTO();
                marcaGetDTO.Codigo = item.Id;
                marcaGetDTO.Nome = item.Nome;

                listaGetDTO.Add(marcaGetDTO);
            }
            return Ok(listaGetDTO);
        }
    }
}
