using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Services.Interfaces;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Services;

public class ProductService : IProductService
{
    private readonly IKafkaProducer _kafkaProducer;
    private readonly IRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    public ProductService(IKafkaProducer kafkaProducer, IRepository productRepository, IMapper mapper, IMemoryCache cache)
    {
        _kafkaProducer = kafkaProducer;
        _productRepository = productRepository;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<ProductDTO> GetProductByIdAsync(int id)
    {
        var cacheKey = $"Product_{id}";
        if (!_cache.TryGetValue(cacheKey, out ProductDTO productDTO))
        {
            var product = await _productRepository.GetByIdAsync(id);
            productDTO = _mapper.Map<ProductDTO>(product);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            _cache.Set(cacheKey, productDTO, cacheEntryOptions);
        }

        return productDTO;
    }

    public async Task<ProductDTO> AddProductAsync(ProductDTO productDTO)
    {

        var productRegistered = await _productRepository.AddAsync(productDTO);
        await _kafkaProducer.ProduceAsync("Produtos", productRegistered);

        return productRegistered;
    }

    public async Task<ProductDTO> UpdateProductAsync(ProductDTO productDTO)
    {
        var product = await _productRepository.UpdateAsync(productDTO);

        return product;
    }
}