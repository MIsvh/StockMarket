using MaterialsStockMarket.Data;
using MediatR;

namespace MaterialsStockMarket.Entities.Seller.Queries;

//обработчик запроса на получение продавца по указаному Id
public class GetSellerByIdQueryHandler : BaseHandler, IRequestHandler<GetSellerByIdQuery, Seller?>
{
    public GetSellerByIdQueryHandler(StockMarketDb dbContext) : base(dbContext)
    { }

    public async Task<Seller?> Handle(GetSellerByIdQuery request, CancellationToken cancellationToken)
    {
        //поиск по указаному идентификатору, если не существует вернётся null
        return await _dbContext.Sellers.FindAsync(request.Id);
    }
}