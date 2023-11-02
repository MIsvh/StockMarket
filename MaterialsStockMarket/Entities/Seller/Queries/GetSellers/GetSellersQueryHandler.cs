using MaterialsStockMarket.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaterialsStockMarket.Entities.Seller.Queries;

//обработчик запроса на получение списка продавцов
public class GetSellersQueryHandler : BaseHandler, IRequestHandler<GetSellersQuery, List<Seller>>
{
    public GetSellersQueryHandler(StockMarketDb dbContext) : base(dbContext)
    { }

    public async Task<List<Seller>> Handle(GetSellersQuery request, CancellationToken cancellationToken)
    {
        //возвращение таблицы продавцов в виде списка
        return await _dbContext.Sellers.ToListAsync();
    }
}