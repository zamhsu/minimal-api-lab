using Microsoft.EntityFrameworkCore;
using MinimalApi.Repository.DbContexts;
using MinimalApi.Repository.Entities;
using MinimalApi.Repository.Interfaces;

namespace MinimalApi.Repository.Implements;

public class ProductRepository : IProductRepository
{
    private readonly EcShopContext _context;

    public ProductRepository(EcShopContext context)
    {
        _context = context;
    }

    // Create
    public async Task<Product> CreateAsync(Product product)
    {
        _context.Product.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    // Read - All
    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Product.ToListAsync();
    }

    // Read - By Id
    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Product.FindAsync(id);
    }

    // Update
    public async Task<bool> UpdateAsync(Product product)
    {
        var existing = await _context.Product.FindAsync(product.Id);
        if (existing == null) return false;

        _context.Entry(existing).CurrentValues.SetValues(product);
        await _context.SaveChangesAsync();
        return true;
    }

    // Delete
    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Product.FindAsync(id);
        if (product == null) return false;

        _context.Product.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}