using ApiDesafio.Business.Commands.Compras;
using ApiDesafio.Business.Core.Notificacoes;
using ApiDesafio.Business.Models.Compras;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafio.Controllers
{
    [ApiController]
    [Route("api/compras")]
    public class ComprasController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICompraRepository _comprasRepository;
        private readonly INotificador _notificador;


        public ComprasController(ICompraRepository compraRepository, INotificador notificador, IMediator mediator)
        {
            _comprasRepository = compraRepository;
            _notificador = notificador;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            return Ok(await _comprasRepository.ObterTodosComProdutos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var compra = await _comprasRepository.ObterComProdutosPorId(id);
            if (compra is null)
            {
                return NotFound();
            }

            return Ok(compra);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(CompraAdicionarCommand compra)
        {
            var response = await _mediator.Send(compra);

            if (_notificador.TemNotificacao())
            {
                return BadRequest(_notificador.ObterNotificacoes());
            }

            return CreatedAtAction(nameof(ObterPorId), new { response.Id }, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var compra = await _comprasRepository.ObterPorId(id);
            if (compra is null)
            {
                return NotFound();
            }

            var request = new CompraRemoverCommand();
            request.Id = id;

            var response = await _mediator.Send(request);

            if (_notificador.TemNotificacao())
            {
                return BadRequest(_notificador.ObterNotificacoes());
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(CompraAtualizarCommand compra)
        {

            var compraAtualizar = await _comprasRepository.ObterPorId(compra.CompraId);
            if (compraAtualizar is null)
            {
                return NotFound();
            }

            var response = await _mediator.Send(compra);

            if (_notificador.TemNotificacao())
            {
                return BadRequest(_notificador.ObterNotificacoes());
            }

            return Ok(response);
        }
    }
}
