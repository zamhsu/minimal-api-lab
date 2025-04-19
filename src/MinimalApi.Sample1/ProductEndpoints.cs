using MinimalApi.Service.Dtos;
using MinimalApi.Service.Interfaces;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        app.MapPost("/products", async (IProductService productService, CreateProductDto dto) =>
        {
            var result = await productService.CreateAsync(dto);
            return Results.Created($"/products/{result.Id}", result);
        });

        app.MapGet("/products", async (IProductService productService) =>
        {
            var result = await productService.GetAllAsync();
            return Results.Ok(result);
        });

        app.MapGet("/products/{id:int}", async (IProductService productService, int id) =>
        {
            var result = await productService.GetByIdAsync(id);
            return result is not null ? Results.Ok(result) : Results.NotFound();
        });

        app.MapPut("/products/{id:int}", async (IProductService productService, int id, UpdateProductDto dto) =>
        {
            var success = await productService.UpdateAsync(id, dto);
            return success ? Results.NoContent() : Results.NotFound();
        });

        app.MapDelete("/products/{id:int}", async (IProductService productService, int id) =>
        {
            var success = await productService.DeleteAsync(id);
            return success ? Results.NoContent() : Results.NotFound();
        });
    }
}