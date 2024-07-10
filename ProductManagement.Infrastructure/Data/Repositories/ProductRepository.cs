using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Services.Interfaces;
using ProductManagement.Infrastructure.DTOs;
using System.Diagnostics;

namespace ProductManagement.Infrastructure.Data.Repositories;
public class ProductRepository : IRepository
{
    private readonly ProductDbContext _dbContext;

    public ProductRepository(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductDTO> GetByIdAsync(int id)
    {
        var productEntity = await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        return new ProductDTO
        {
            Id = productEntity.Id,
            Name = productEntity.Name,
            Price = productEntity.Price,
        };
    }

    public async Task<ProductDTO> AddAsync(ProductDTO productDTO)
    {
        var productEntity = await _dbContext.Products.AddAsync(ConvertEntity(productDTO));

        await _dbContext.SaveChangesAsync();

        var ProductDTO = new ProductDTO
        {
            Name = productEntity.Entity.Name,
            Price = productEntity.Entity.Price,
        };

        return ProductDTO;
    }

    public async Task<ProductDTO> UpdateAsync(ProductDTO productDTO)
    {
        var result = await _dbContext.Products
        .Where(p => p.Id == productDTO.Id)
        .ExecuteUpdateAsync(setters => setters
            .SetProperty(p => p.Name, productDTO.Name)
            .SetProperty(p => p.Price, productDTO.Price));

        if (result == 0)
        {
            throw new InvalidOperationException("Product not found.");
        }

        await _dbContext.SaveChangesAsync();
        return productDTO;

    }
    private ProductEntityDTO ConvertEntity(ProductDTO productDTO)
    {
        return new ProductEntityDTO
        {
            Id = productDTO.Id,
            Name = productDTO.Name,
            Price = productDTO.Price,
        };

    }

}