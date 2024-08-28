using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApiTools.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace ApiTools.Controllers
{
    [Route("[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Produtos>> Get()
        {
            var produtos = _produtoService.GetAll();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Produtos> Get(int id)
        {
            var produto = _produtoService.GetById(id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Add([FromBody] Produtos produto)
        {
            _produtoService.Add(produto);
            return CreatedAtAction(nameof(Get), new { id = produto.cd_prouto }, produto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Update(int id, [FromBody] Produtos produto)
        {
            if (id != produto.cd_prouto)
            {
                return BadRequest();
            }

            var existingProduto = _produtoService.GetById(id);
            if (existingProduto == null)
            {
                return NotFound();
            }

            _produtoService.Update(produto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Delete(int id)
        {
            var produto = _produtoService.GetById(id);
            if (produto == null)
            {
                return NotFound();
            }

            _produtoService.Delete(id);
            return NoContent();
        }
    }
}