
using Microsoft.AspNetCore.Mvc;
using TaroTime.Application.DTOs.Products;
using TaroTime.Application.Interfaces.Services;

namespace TaroTime.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync(int page = 0, int take = 0)
        {

            return Ok(await _service.GetAllAsync(page, take));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            if (id < 1) return BadRequest();
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]

        public async Task<IActionResult> PostAsync([FromForm] PostProductDto postProductDto)
        {
            await _service.CreateProductAsync(postProductDto);
            return Created();
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutAsync(long id,[FromForm] PutProductDto putProductDto)
        {
            await _service.UpdateProductAsync(id,putProductDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(long id)
        {

            if (id == null || id < 1) return BadRequest();
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}/soft")]
        public async Task<IActionResult> SoftDelete(long id)
        {
            await _service.SoftDeleteAsync(id);
            return NoContent();
        }


    }
}
