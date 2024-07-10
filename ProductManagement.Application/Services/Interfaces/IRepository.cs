using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Services.Interfaces;

public interface IRepository
{
    Task<ProductDTO> GetByIdAsync(int id);
    Task<ProductDTO> AddAsync(ProductDTO entity);
    Task<ProductDTO> UpdateAsync(ProductDTO entity);
}