using ApiDesafio.Business.Commands.Produtos;
using ApiDesafio.Business.Core.Notificacoes;
using ApiDesafio.Business.Models.Produtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafio.Controllers
{
    [ApiController]
    [Route("api/produtos")]
    public class ProdutosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProdutoRepository _produtoRepository;
        private readonly INotificador _notificador;


        public ProdutosController(IProdutoRepository produtoRepository, INotificador notificador, IMediator mediator)
        {
            _produtoRepository = produtoRepository;
            _notificador = notificador;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            return Ok(await _produtoRepository.ObterTodos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            if(produto is null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(ProdutoAdicionarCommand produto)
        {
            var response = await _mediator.Send(produto);

            if (_notificador.TemNotificacao())
            {
                return BadRequest(_notificador.ObterNotificacoes());
            }

            return CreatedAtAction(nameof(ObterPorId), new { response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] ProdutoAtualizarCommand produto)
        {
            var produtoEditavel = await _produtoRepository.ObterPorId(id);
            if (produtoEditavel is null)
            {
                return NotFound();
            }

            var mensagem = await _mediator.Send(produto);

            if (_notificador.TemNotificacao())
            {
                return BadRequest(_notificador.ObterNotificacoes());
            }

            return Ok(mensagem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            if (produto is null)
            {
                return NotFound();
            }

            var request = new ProdutoRemoverCommand();
            request.Id = id;

            await _mediator.Send(request);

            if (_notificador.TemNotificacao())
            {
                return BadRequest(_notificador.ObterNotificacoes());
            }

            return Ok(produto);
        }
    }
}
