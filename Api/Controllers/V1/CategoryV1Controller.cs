using Api.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Categories;

namespace Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryV1Controller : ControllerBase
    {
        private readonly ICategoryService _categoryServicee;

        public CategoryV1Controller(ICategoryService categoryServicee)
        {
            _categoryServicee = categoryServicee;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Category>> Get(int id)
        {
            try
            {
                var category = await _categoryServicee.GetCategoryById(id);
                if (category is null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                Log.Error("Category Get => {@ex}", ex.Message);
                return Problem();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create(CategoryDto categoryDto)
        {
            try
            {
                var category = new Category
                {
                    Id = categoryDto.Id,
                    CategoryName = categoryDto.CategoryName,
                    Description = categoryDto.Description
                };
                await _categoryServicee.InsertCategory(category);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error("Category Create => {@ex}", ex.Message);
                return Problem();
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Category>>> GetAll()
        {
            try
            {
                var categorys = await _categoryServicee.GetAll();
                if (categorys is null)
                {
                    return NotFound();
                }

                var categorysDto = categorys.Select(p => new CategoryDto
                {
                    Id = p.Id,
                    CategoryName = p.CategoryName,
                    Description = p.Description
                }).ToList();

                return Ok(categorysDto);
            }
            catch (Exception ex)
            {
                Log.Error("Category GetAllByCategory => {@ex}", ex.Message);
                return Problem();
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Category>> Delete(int id)
        {
            try
            {
                var category = await _categoryServicee.GetCategoryById(id);
                if (category is null)
                {
                    return NotFound();
                }
                else
                {
                    await _categoryServicee.DeleteCategory(id);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Category Delete => {@ex}", ex.Message);
                return Problem();
            }
        }
    }
}
