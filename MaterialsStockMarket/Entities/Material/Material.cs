namespace MaterialsStockMarket.Entities.Material;

//Модель данных для материала
public class Material
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public decimal Price { get; set; }
    
    public int SellerId { get; set; }
}