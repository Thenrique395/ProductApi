using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Services.Interfaces;

namespace ProductManagement.Api.Controllers;

[Route("v1/[controller]")]
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productDTO)
    {
        try
        {
            await _productService.AddProductAsync(productDTO);
            return Ok(productDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var productDTO = await _productService.GetProductByIdAsync(id);
        if (productDTO == null)
            return NotFound();

        return Ok(productDTO);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO productDTO)
    {
        try
        {
            var product = await _productService.UpdateProductAsync(productDTO);
            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}