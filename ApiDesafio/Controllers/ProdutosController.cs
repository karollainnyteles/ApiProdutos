using ApiDesafio.Business.Core.Notificacoes;
using ApiDesafio.Business.Models.Produtos;
using ApiDesafio.Business.Models.Produtos.Services;
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
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly INotificador _notificador;


        public ProdutosController(IProdutoRepository produtoRepository, INotificador notificador, IProdutoService produtoService)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _notificador = notificador;
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
        public async Task<IActionResult> Adicionar(Produto produto)
        {
            await _produtoService.Adicionar(produto);

            if (_notificador.TemNotificacao())
            {
                return BadRequest(_notificador.ObterNotificacoes());
            }

            return CreatedAtAction(nameof(ObterPorId), new { produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Produto produto)
        {
            var produtoEditavel = await _produtoRepository.ObterPorId(id);
            if (produtoEditavel is null)
            {
                return NotFound();
            }

            //if(id != produto.Id)
            //{
            //    _notificador.
            //}



            await _produtoService.Atualizar(produto);

            if (_notificador.TemNotificacao())
            {
                return BadRequest(_notificador.ObterNotificacoes());
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            if (produto is null)
            {
                return NotFound();
            }

            await _produtoService.Remover(id);

            if (_notificador.TemNotificacao())
            {
                return BadRequest(_notificador.ObterNotificacoes());
            }

            return Ok(produto);
        }
    }
}
