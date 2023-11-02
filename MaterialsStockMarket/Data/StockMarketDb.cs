using MaterialsStockMarket.Entities.Material;
using MaterialsStockMarket.Entities.Seller;
using Microsoft.EntityFrameworkCore;

namespace MaterialsStockMarket.Data;

//Класс, хранящий таблицы базы данных
public class StockMarketDb : DbContext
{
    //Таблица материалов
    public DbSet<Material> SellingMaterials { get; set; }
    
    //Таблица продавцов
    public DbSet<Seller> Sellers { get; set; }

    public StockMarketDb(DbContextOptions<StockMarketDb> options) : base(options)
    {
        //Создание таблиц SellingMaterials и Sellers в бд, если они не существуют
        Database.EnsureCreated();
    }
}