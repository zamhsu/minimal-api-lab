using MinimalApi.Service.Dtos;

namespace MinimalApi.Service.Interfaces;

public interface IProductService
{
    Task<ProductDto> CreateAsync(CreateProductDto createProductDto);
    Task<List<ProductDto>> GetAllAsync();
    Task<ProductDto> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, UpdateProductDto updateProductDto);
    Task<bool> DeleteAsync(int id);
}