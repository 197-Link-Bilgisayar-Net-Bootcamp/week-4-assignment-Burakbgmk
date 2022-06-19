using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLayerCore.Dtos;
using NLayerCore.Services;

namespace NLayerApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAll();
            return new ObjectResult(response) { StatusCode = response.Status };
        }

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _categoryService.GetById(id);
            return new ObjectResult(response) { StatusCode = response.Status };
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            var response = await _categoryService.Create(categoryDto);
            return new ObjectResult(response) { StatusCode = response.Status };
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, CategoryDto categoryDto)
        {
            var response = await _categoryService.Update(id, categoryDto);
            return new ObjectResult(response) { StatusCode = response.Status };
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var response = _categoryService.Delete(id);
            return new ObjectResult(response) { StatusCode = response.Status };
        }
    }
}
