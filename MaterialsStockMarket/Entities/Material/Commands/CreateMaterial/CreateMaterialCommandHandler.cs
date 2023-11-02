using MaterialsStockMarket.Data;
using MediatR;

namespace MaterialsStockMarket.Entities.Material.Commands;

//обработчик команды для создания материала
public class CreateMaterialCommandHandler : BaseHandler, IRequestHandler<CreateMaterialCommand, Material?>
{
    public CreateMaterialCommandHandler(StockMarketDb dbContext) : base(dbContext) { }
    
    public async Task<Material?> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
    {
        //при id не равном нулю поиск уже существующего материала
        if (request.Material.Id != 0 )
        {
            var existingMaterial = await _dbContext.SellingMaterials.FindAsync(request.Material.Id);
            //если материал с таким id существует, то добавление метариала не происходит
            if (existingMaterial != null)
                return null;
        }
        
        //добавление материала с указаным id или генерация id если он равен 0
        var result = await _dbContext.SellingMaterials.AddAsync(request.Material);
        //сохранение изменений в бд
        await _dbContext.SaveChangesAsync();
        //возвращение добавленного материала
        return result.Entity;
    }
}