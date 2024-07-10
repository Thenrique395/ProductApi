using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManagement.Api.Controllers;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Services.Interfaces;

namespace ProductManagement.Tests.Api;

public class ProductsControllerTests
{
    private readonly Mock<IProductService> _mockProductService;
    private readonly ProductsController _controller;

    public ProductsControllerTests()
    {
        _mockProductService = new Mock<IProductService>();
        _controller = new ProductsController(_mockProductService.Object);
    }

    [Fact]
    public async Task CreateProduct_ReturnsOkObjectResult_WhenProductIsAddedSuccessfully()
    {
        // Arrange
        var productDTO = new ProductDTO { Id = 1, Name = "Test Product", Price = 99.99M };

        _mockProductService.Setup(x => x.AddProductAsync(It.IsAny<ProductDTO>()))
                           .ReturnsAsync(productDTO);

        // Act
        var result = await _controller.CreateProduct(productDTO) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(productDTO, result.Value);
    }

    [Fact]
    public async Task CreateProduct_ReturnsStatusCode500_WhenExceptionOccurs()
    {
        // Arrange
        var productDTO = new ProductDTO { Id = 1, Name = "Test Product", Price = 99.99M };
        var errorMessage = "Simulated error message";

        _mockProductService.Setup(x => x.AddProductAsync(It.IsAny<ProductDTO>()))
                           .ThrowsAsync(new Exception(errorMessage));

        // Act
        var result = await _controller.CreateProduct(productDTO) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
        Assert.Equal($"Internal server error: {errorMessage}", result.Value);
    }

    [Fact]
    public async Task GetProduct_ReturnsOkObjectResult_WhenProductExists()
    {
        // Arrange
        var productId = 1;
        var productDTO = new ProductDTO { Id = productId, Name = "Test Product", Price = 99.99M };

        _mockProductService.Setup(x => x.GetProductByIdAsync(productId))
                           .ReturnsAsync(productDTO);

        // Act
        var result = await _controller.GetProduct(productId) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(productDTO, result.Value);
    }

    [Fact]
    public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
    {
        // Arrange
        var productId = 1;

        _mockProductService.Setup(x => x.GetProductByIdAsync(productId))
                           .ReturnsAsync((ProductDTO)null);

        // Act
        var result = await _controller.GetProduct(productId) as NotFoundResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task UpdateProduct_ReturnsOkObjectResult_WhenProductIsUpdatedSuccessfully()
    {
        // Arrange
        var productDTO = new ProductDTO { Id = 1, Name = "Updated Product", Price = 129.99M };

        _mockProductService.Setup(x => x.UpdateProductAsync(It.IsAny<ProductDTO>()))
                           .ReturnsAsync(productDTO);

        // Act
        var result = await _controller.UpdateProduct(productDTO) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(productDTO, result.Value);
    }

    [Fact]
    public async Task UpdateProduct_ReturnsStatusCode500_WhenExceptionOccurs()
    {
        // Arrange
        var productDTO = new ProductDTO { Id = 1, Name = "Test Product", Price = 99.99M };
        var errorMessage = "Simulated error message";

        _mockProductService.Setup(x => x.UpdateProductAsync(It.IsAny<ProductDTO>()))
                           .ThrowsAsync(new Exception(errorMessage));

        // Act
        var result = await _controller.UpdateProduct(productDTO) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
        Assert.Equal($"Internal server error: {errorMessage}", result.Value);
    }
}
