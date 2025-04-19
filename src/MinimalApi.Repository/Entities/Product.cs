namespace MinimalApi.Repository.Entities;

public class Product
{
    public Product()
    {
        CreateDate = DateTime.Now;
        UpdateDate = DateTime.Now;
    }
    
    public int Id { get; set; }
    public string Title { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}