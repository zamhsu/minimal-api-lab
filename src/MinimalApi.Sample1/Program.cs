using Mapster;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Repository.DbContexts;
using MinimalApi.Repository.Interfaces;
using MinimalApi.Repository.Implements;
using MinimalApi.Sample1.Infrastructure.InputParameters;
using MinimalApi.Sample1.Infrastructure.OutputModels;
using MinimalApi.Sample1.Infrastructure.ServiceCollectionExtensions;
using MinimalApi.Service.Dtos;
using MinimalApi.Service.Interfaces;
using MinimalApi.Service.Implements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDbContext<EcShopContext>(options =>
    options.UseInMemoryDatabase("EcShop"));

// Add demo data.
builder.Services.AddDemoData();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/products", async (IProductService productService, CreateProductParameter parameter) =>
{
    var dto = parameter.Adapt<CreateProductDto>();
    var result = await productService.CreateAsync(dto);
    return Results.Created($"/products/{result.Id}", result);
});

app.MapGet("/api/products", async (IProductService productService) =>
{
    var dto = await productService.GetAllAsync();
    var outputModel = dto.Adapt<List<ProductOutputModel>>();
    return Results.Ok(outputModel);
});

app.MapGet("/api/products/{id:int}", async (IProductService productService, int id) =>
{
    var dto = await productService.GetByIdAsync(id);
    var outputModel = dto.Adapt<ProductOutputModel>();
    return outputModel is not null ? Results.Ok(outputModel) : Results.NotFound();
});

app.MapPut("/api/products/{id:int}", async (IProductService productService, int id, UpdateProductParameter parameter) =>
{
    var dto = parameter.Adapt<UpdateProductDto>();
    var success = await productService.UpdateAsync(id, dto);
    return success ? Results.NoContent() : Results.NotFound();
});

app.MapDelete("/api/products/{id:int}", async (IProductService productService, int id) =>
{
    var success = await productService.DeleteAsync(id);
    return success ? Results.NoContent() : Results.NotFound();
});

app.Run();