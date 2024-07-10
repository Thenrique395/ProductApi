using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Services;
using ProductManagement.Application.Services.Interfaces;
using ProductManagement.Infrastructure.DTOs;

namespace ProductManagement.Tests.Application;
public class ProductServiceTests
{
    private readonly ProductService _productService;
    private readonly Mock<IKafkaProducer> _mockKafkaProducer;
    private readonly Mock<IRepository> _mockProductRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IMemoryCache> _mockCache;

    public ProductServiceTests()
    {
        _mockKafkaProducer = new Mock<IKafkaProducer>();
        _mockProductRepository = new Mock<IRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockCache = new Mock<IMemoryCache>();

        _productService = new ProductService(
            _mockKafkaProducer.Object,
            _mockProductRepository.Object,
            _mockMapper.Object,
            _mockCache.Object
        );
    }

    [Fact]
    public async Task AddProductAsync_ShouldAddProduct_AndProduceMessage()
    {
        // Arrange
        var productDTO = new ProductDTO { Id = 1, Name = "New Product" };
        var productEntity = new ProductEntityDTO { Id = 1, Name = "New Product" };


        _mockProductRepository.Setup(x => x.AddAsync(productDTO)).ReturnsAsync(productDTO);

        _mockKafkaProducer.Setup(x => x.ProduceAsync("Produtos", productEntity)).Returns(Task.CompletedTask);

        _mockMapper.Setup(x => x.Map<ProductDTO>(productEntity)).Returns(productDTO);

        // Act
        var result = await _productService.AddProductAsync(productDTO);

        // Assert
        Assert.Equal(productDTO, result);
        _mockProductRepository.Verify(x => x.AddAsync(productDTO), Times.Once);
        _mockKafkaProducer.Verify(x => x.ProduceAsync("Produtos", productDTO), Times.Once);
    }

    [Fact]
    public async Task UpdateProductAsync_ShouldUpdateProduct()
    {
        // Arrange
        var productDTO = new ProductDTO { Id = 1, Name = "Updated Product" };
        var productEntity = new ProductEntityDTO { Id = 1, Name = "Updated Product" };

        _mockProductRepository.Setup(x => x.UpdateAsync(productDTO)).ReturnsAsync(productDTO);
        _mockMapper.Setup(x => x.Map<ProductDTO>(productEntity)).Returns(productDTO);

        // Act
        var result = await _productService.UpdateProductAsync(productDTO);

        // Assert
        Assert.Equal(productDTO, result);
        _mockProductRepository.Verify(x => x.UpdateAsync(productDTO), Times.Once);
    }
}