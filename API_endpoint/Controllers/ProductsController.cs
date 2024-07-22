using API_endpoint.Models;
using API_endpoint.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        // Llamar a la MAC:
        private readonly MacService _macAddressService;
        public ProductsController(MacService macAddressService)
        {
            _macAddressService = macAddressService;
        }


        private static readonly List<Producto> Products = new List<Producto>
        {
            new Producto { Id = 1, Name = "Product1", Price = 10.0M },
            new Producto { Id = 2, Name = "Product2", Price = 20.0M }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Producto>> GetProducts()
        {
            return Products;
        }

        [HttpGet("{id}")]
        public ActionResult<Producto> GetProduct(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public ActionResult<Producto> CreateProduct(Producto product)
        {
            product.Id = Products.Max(p => p.Id) + 1;
            Products.Add(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Producto product)
        {
            var existingProduct = Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            Products.Remove(product);
            return NoContent();
        }

        // Endpoint para obtener la dirección MAC
        [HttpGet("mac-address")]
        public ActionResult<string> GetMacAddress()
        {
            var macAddress = _macAddressService.GetMacAddress();
            return Ok(macAddress);
        }
    }
}
