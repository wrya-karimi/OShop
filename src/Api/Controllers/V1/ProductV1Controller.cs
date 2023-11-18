using Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using OShop.Domain.Abstracts.Application;
using OShop.Domain.Entities;
using Serilog;


namespace Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductV1Controller : ControllerBase
    {
        private readonly IProductService _productServicee;

        public ProductV1Controller(IProductService productServicee)
        {
            _productServicee = productServicee;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Product>> Get(int id)
        {
            try
            {
                var product = await _productServicee.GetProductById(id);
                if (product is null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                Log.Error("Product Get => {@ex}", ex.Message);
                return Problem();
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(ProductDto productDto)
        {
            try
            {
                var product = new Product
                {
                    CategoryId = productDto.CategoryId,
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    Discount = productDto.Discount,
                    ImagePath = productDto.ImagePath
                };
                await _productServicee.InsertProduct(product);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error("Product Create => {@ex}", ex.Message);
                return Problem();
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }



        [HttpGet("{cId}/GetAllByCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Product>>> GetAllByCategory(int cId)
        {
            try
            {
                var products = await _productServicee.GetAllProductsByCategory(cId);
                if (products is null)
                {
                    return NotFound();
                }

                var productsDto = products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Discount = p.Discount,
                    ImagePath = p.ImagePath,
                    CategoryId = p.CategoryId

                }).ToList();

                return Ok(productsDto);
            }
            catch (Exception ex)
            {
                Log.Error("Product GetAllByCategory => {@ex}", ex.Message);
                return Problem();
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            try
            {
                var product = await _productServicee.GetProductById(id);
                if (product is null)
                {
                    return NotFound();
                }
                else
                {
                    await _productServicee.DeleteProduct(id);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Product Delete => {@ex}", ex.Message);
                return Problem();
            }
        }
    }
}
