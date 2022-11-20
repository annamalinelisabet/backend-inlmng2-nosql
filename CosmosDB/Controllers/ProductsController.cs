using CosmosDB.Contexts;
using CosmosDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CosmosDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var productModel = new ProductModel
            {
                Id = Guid.NewGuid(),
                PartitionKey = "Products",
                ArtNo = product.ArtNo,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };
            _context.Add(productModel);
            await _context.SaveChangesAsync();  
            return new OkObjectResult(productModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _context.Products.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductModel(Guid id, ProductModel productModel)
        {
            if (id != productModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(productModel).State = EntityState.Modified;
             await _context.SaveChangesAsync();

            return new OkObjectResult(productModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductModel(Guid id)
        {
            var productModel = await _context.Products.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
