using System.Diagnostics;
using MaterialsStockMarket.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaterialsStockMarket.Entities.Material.Queries;

//обработчик запроса на получение списка всех материалов
public class GetMaterialsQueryHandler : BaseHandler, IRequestHandler<GetMaterialsQuery, List<Material>>
{
    public GetMaterialsQueryHandler(StockMarketDb dbContext) : base(dbContext)
    {}

    public async Task<List<Material>> Handle(GetMaterialsQuery request, CancellationToken cancellationToken)
    {
        //возвращение таблицы материалов в виде списка
        return await _dbContext.SellingMaterials.ToListAsync();
    }
}