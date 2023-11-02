using MaterialsStockMarket.Data;
using MediatR;

namespace MaterialsStockMarket.Entities.Material.Commands;

//обработчик команды для удаления материала
public class DeleteMaterialCommandHandler : BaseHandler, IRequestHandler<DeleteMaterialCommand, Boolean>
{
    public DeleteMaterialCommandHandler(StockMarketDb dbContext) : base(dbContext)
    { }

    public async Task<Boolean> Handle(DeleteMaterialCommand request, CancellationToken cancellationToken)
    {
        //поиск материала с нужным id
        var material = await _dbContext.SellingMaterials.FindAsync(request.Id);
        
        if (material == null)
            return false;
        
        //если найден - его удаление и сохранения изменений бд
        _dbContext.SellingMaterials.Remove(material);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}