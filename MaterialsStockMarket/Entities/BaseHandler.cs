using MaterialsStockMarket.Data;

namespace MaterialsStockMarket.Entities;

//базовый класс обработчика команд и запросов
public class BaseHandler
{
    //контекст базы данных, доступный классам-наследникам
    protected readonly StockMarketDb _dbContext;
    
    public BaseHandler(StockMarketDb dbContext)
    {
        _dbContext = dbContext;
    }
}