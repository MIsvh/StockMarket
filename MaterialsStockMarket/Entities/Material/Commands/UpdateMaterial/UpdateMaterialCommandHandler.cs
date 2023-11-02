using MaterialsStockMarket.Data;
using MediatR;

namespace MaterialsStockMarket.Entities.Material.Commands;

//обработчик команды для изменения материала
public class UpdateMaterialCommandHandler : BaseHandler, IRequestHandler<UpdateMaterialCommand, Material?>
{
    public UpdateMaterialCommandHandler(StockMarketDb dbContext) : base(dbContext)
    { }

    public async Task<Material?> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
    {
        //поиск материала по id
        var result = await _dbContext.SellingMaterials.FindAsync(request.Material.Id);
        
        if (result == null)
            return null;

        //изменение полей материала, если был найден
        result.Name = request.Material.Name;
        result.Price = request.Material.Price;
        result.SellerId = request.Material.SellerId;

        //сохранение измений бд
        await _dbContext.SaveChangesAsync();
        return result;
    }
}