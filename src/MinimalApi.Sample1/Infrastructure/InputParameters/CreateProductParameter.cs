namespace MinimalApi.Sample1.Infrastructure.InputParameters;

public class CreateProductParameter
{
    public string Title { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
}