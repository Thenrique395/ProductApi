using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Services.Interfaces;
public interface IProductService
{
    Task<ProductDTO> GetProductByIdAsync(int id);
    Task<ProductDTO> AddProductAsync(ProductDTO productDTO);
    Task<ProductDTO> UpdateProductAsync(ProductDTO productDTO);
}