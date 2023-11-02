using MaterialsStockMarket.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaterialsStockMarket.Entities.Material.Commands;

//обработчик команды для случайного изменения цен всех материалов
public class UpdateMaterialsPricesCommandHandler : BaseHandler, IRequestHandler<UpdateMaterialsPricesCommand, Boolean>
{
    public UpdateMaterialsPricesCommandHandler(StockMarketDb dbContext) : base(dbContext)
    { }

    public async Task<Boolean> Handle(UpdateMaterialsPricesCommand request, CancellationToken cancellationToken)
    {
        //создание генератора случайных чисел
        Random r = new ();
        
        //обход всех елементов таблицы материалов и изменение цены в пределах MinPrice - MaxPrice
        await _dbContext.SellingMaterials.ForEachAsync(
            material => material.Price = 
                (decimal) Math.Round(r.NextDouble(), 4) * (request.MaxPrice - request.MinPrice) + request.MinPrice);
        
        //сохранение изменений бд
        await _dbContext.SaveChangesAsync();
        return true;
    }
}