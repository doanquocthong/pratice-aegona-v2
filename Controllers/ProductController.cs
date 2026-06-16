using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pratice_aegona_v2.Models.ViewModels;
using pratice_aegona_v2.Services.Interfaces;

namespace pratice_aegona_v2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound(new { message = "Không tìm thấy sản phẩm" });
            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateProductViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newProduct = await _productService.CreateProductAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedProduct = await _productService.UpdateProductAsync(id, model);
            if (updatedProduct == null) return NotFound(new { message = "Không tìm thấy sản phẩm để cập nhật" });

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result) return NotFound(new { message = "Không tìm thấy sản phẩm để xóa" });

            return Ok(new { message = "Xóa sản phẩm thành công" });
        }
    }
}