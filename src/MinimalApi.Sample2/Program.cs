using Mapster;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Repository.DbContexts;
using MinimalApi.Repository.Implements;
using MinimalApi.Repository.Interfaces;
using MinimalApi.Sample2.Infrastructure.InputParameters;
using MinimalApi.Sample2.Infrastructure.OutputModels;
using MinimalApi.Sample2.Infrastructure.ServiceCollectionExtensions;
using MinimalApi.Service.Dtos;
using MinimalApi.Service.Implements;
using MinimalApi.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDbContext<EcShopContext>(options =>
    options.UseInMemoryDatabase("EcShop"));

// Add demo data.
builder.Services.AddDemoData();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

var products = app.MapGroup("/products");

products.MapPost("/", CreateProductAsync);
products.MapGet("/", GetAllProductsAsync);
products.MapGet("/{id:int}", GetProductByIdAsync);
products.MapPut("/{id:int}", UpdateProductAsync);
products.MapDelete("/{id:int}", DeleteProductAsync);

app.Run();

static async Task<IResult> CreateProductAsync(IProductService service, CreateProductParameter parameter)
{
    var dto = parameter.Adapt<CreateProductDto>();
    var result = await service.CreateAsync(dto);
    return TypedResults.Created($"/products/{result.Id}", result);
}

static async Task<IResult> GetAllProductsAsync(IProductService service)
{
    var dto = await service.GetAllAsync();
    var output = dto.Adapt<List<ProductOutputModel>>();
    return TypedResults.Ok(output);
}

static async Task<IResult> GetProductByIdAsync(int id, IProductService service)
{
    var dto = await service.GetByIdAsync(id);
    if (dto == null) return TypedResults.NotFound();

    var output = dto.Adapt<ProductOutputModel>();
    return TypedResults.Ok(output);
}

static async Task<IResult> UpdateProductAsync(int id, UpdateProductParameter parameter, IProductService service)
{
    var dto = parameter.Adapt<UpdateProductDto>();
    var success = await service.UpdateAsync(id, dto);
    return success ? TypedResults.NoContent() : TypedResults.NotFound();
}

static async Task<IResult> DeleteProductAsync(int id, IProductService service)
{
    var success = await service.DeleteAsync(id);
    return success ? TypedResults.NoContent() : TypedResults.NotFound();
}