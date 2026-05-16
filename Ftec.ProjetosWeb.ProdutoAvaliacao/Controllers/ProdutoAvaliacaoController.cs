using Ftec.ProjetosWeb.ProdutoAvaliacao.Aplicacao;
using Ftec.ProjetosWeb.ProdutoAvaliacao.Aplicacao.DTO;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ftec.ProjetosWeb.ProdutoAvaliacao.Controllers
{
    [Route("api/avaliacao")]
    [ApiController]
    [SwaggerTag("Operações relacionadas às avaliações de produtos")]
    public class ProdutoAvaliacaoController : ControllerBase
    {
        ProdutoAvaliacaoAplicacao produtoAvaliacaoAplicacao;

        public ProdutoAvaliacaoController(IConfiguration config)
        {
            produtoAvaliacaoAplicacao = new ProdutoAvaliacaoAplicacao(config["strConexao"]);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Busca avaliação por ID",
            Description = "Retorna uma avaliação específica pelo ID informado."
        )]
        [ProducesResponseType(typeof(ProdutoAvaliacaoDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Get(Guid id)
        {
            try
            {
                var aval = produtoAvaliacaoAplicacao.ObterProdutoAvaliacaoById(id);
                return Ok(aval);
            }
            catch (Exception ex) when (ex.Message.Contains("não encontrado"))
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("produto/{idProduto}")]
        [SwaggerOperation(
            Summary = "Lista avaliações de um produto",
            Description = "Retorna todas as avaliações relacionadas a um produto específico."
        )]
        [ProducesResponseType(typeof(List<ProdutoAvaliacaoDTO>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetAvaliacoesProduto(Guid idProduto)
        {
            try
            {
                var aval = produtoAvaliacaoAplicacao.ObterAvaliacoesByIdProduto(idProduto);
                return Ok(aval);
            }
            catch (Exception ex) when (ex.Message.Contains("não encontrado"))
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria uma nova avaliação",
            Description = "Adiciona uma nova avaliação de produto ao sistema."
        )]
        [ProducesResponseType(typeof(ProdutoAvaliacaoDTO), 201)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] ProdutoAvaliacaoDTO produtoAval)
        {
            try
            {
                produtoAvaliacaoAplicacao.AdicionarProdutoAvaliacao(produtoAval);
                return Created(string.Empty, produtoAval);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualiza uma avaliação",
            Description = "Altera os dados de uma avaliação já cadastrada."
        )]
        [ProducesResponseType(typeof(ProdutoAvaliacaoDTO), 200)]
        [ProducesResponseType(400)]
        public IActionResult Put([FromBody] ProdutoAvaliacaoDTO produtoAval)
        {
            try
            {
                produtoAvaliacaoAplicacao.AlterarProdutoAvaliacao(produtoAval);
                return Ok(produtoAval);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remove uma avaliação",
            Description = "Exclui uma avaliação de produto pelo ID informado."
        )]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Delete(Guid id)
        {
            try
            {
                produtoAvaliacaoAplicacao.ExcluirProdutoAvaliacao(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}