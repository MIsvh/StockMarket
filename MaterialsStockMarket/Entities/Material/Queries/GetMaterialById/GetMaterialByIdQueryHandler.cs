using MaterialsStockMarket.Data;
using MediatR;
using NuGet.Protocol.Plugins;

namespace MaterialsStockMarket.Entities.Material.Queries;

//обработчик запроса на получение материала по идентификатору
public class GetMaterialByIdQueryHandler : BaseHandler, IRequestHandler<GetMaterialByIdQuery, Material?>
{
    public GetMaterialByIdQueryHandler(StockMarketDb dbContext) : base(dbContext)
    {}
    
    public async Task<Material?> Handle(GetMaterialByIdQuery request, CancellationToken cancellationToken)
    {
        //поиск по указаному идентификатору, если не существует вернётся null
        return await _dbContext.SellingMaterials.FindAsync(request.Id);
    }
}