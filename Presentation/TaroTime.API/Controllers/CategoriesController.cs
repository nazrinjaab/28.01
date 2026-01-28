using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaroTime.Application.DTOs;
using TaroTime.Application.Interfaces.Services;

namespace TaroTime.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {


        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {

            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page, int take)
        {



            return Ok(await _service.GetAllAsync(page, take));
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> Get(long id)
        {
            if (id == null || id < 1) return BadRequest();

            return Ok(await _service.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PostCategoryDto categoryDto)
        {

            await _service.CreateAsync(categoryDto);
            return Created();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromForm] PutCategoryDto categoryDto)
        {
            if (id == null || id < 1) return BadRequest();
            await _service.UpdateAsync(id, categoryDto);
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
        public async Task<IActionResult> SoftDelete (long id)
        {
            await _service.SoftDeleteAsync(id);
            return NoContent();
        }


    }
}
