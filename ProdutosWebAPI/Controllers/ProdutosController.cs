using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProdutosWebAPI.Models;
using ProdutosWebAPI.Service;

namespace ProdutosWebAPI.Controllers
{
    [ApiController]
    [Route("produtos")]
    public class ProdutosController : Controller
    {
        private readonly IProdutoService service;
        private readonly ILogger<ProdutosController> _logger;

        public ProdutosController(ILogger<ProdutosController> logger)
        {
            _logger = logger;
            this.service = new ProdutoService();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var model = service.GetProdutos();
            return Ok(model);
        }

        [HttpGet("{id}", Name = "GetProduto")]
        public IActionResult Get(int id)
        {
            var model = service.GetProduto(id);

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Produto inputModel)
        {
            if (inputModel == null)
                return BadRequest();

            service.AddProduto(inputModel);
            return CreatedAtRoute("GetProduto", new { id = inputModel.Id }, inputModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Produto inputModel)
        {
            if (inputModel == null || id != inputModel.Id)
                return BadRequest();

            if (!service.ProdutoExists(id))
                return NotFound();

            service.UpdateProduto(inputModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!service.ProdutoExists(id))
                return NotFound();

            service.DeleteProduto(id);
            return NoContent();
        }
    }
}
