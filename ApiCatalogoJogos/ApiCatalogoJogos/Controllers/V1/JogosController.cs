using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.Services;
using ApiCatalogoJogos.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService _jogoService;

        public JogosController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }

        /// <summary>
        /// Buscar todos os jogos de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retonar os jogos sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de registros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos</response>
        [HttpGet]
        public async Task<ActionResult<List<JogoViewModel>>> ObtemJogos([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var jogos = await _jogoService.ObtemJogos(pagina, quantidade);

            if (jogos.Count() == 0)
                return NoContent();

            return Ok(jogos);
        }

        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> ObtemJogoPorId([FromRoute] Guid idJogo)
        {
            var jogo = await _jogoService.ObtemJogoPorId(idJogo);

            if (jogo == null)
                return NoContent();

            return Ok(jogo);
        }

        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InsereJogo([FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                var jogo = await _jogoService.InsereJogo(jogoInputModel);

                return Ok(jogo);
            }
            catch(JogoJaCadastradoException e)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
            
        }

        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizaJogo([FromRoute] Guid idJogo, [FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                await _jogoService.AtualizaJogo(idJogo, jogoInputModel);

                return Ok();
            }
            catch(JogoNaoCadastradoException e)
            {
                return NotFound("O jogo digitado não existe.");
            }
        }

        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizaPrecoJogo([FromRoute] Guid idJogo, [FromRoute] double preco)
        {
            try
            {
                await _jogoService.AtualizaPrecoJogo(idJogo, preco);

                return Ok();
            }
            catch (JogoNaoCadastradoException e)
            {
                return NotFound("O jogo digitado não existe.");
            }
        }

        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> ExcluiJogo([FromRoute] Guid idJogo)
        {
            try
            {
                await _jogoService.ExcluiJogo(idJogo);

                return Ok();
            }
            catch (JogoNaoCadastradoException e)
            {
                return NotFound("O jogo digitado não existe.");
            }
        }
    }
}
