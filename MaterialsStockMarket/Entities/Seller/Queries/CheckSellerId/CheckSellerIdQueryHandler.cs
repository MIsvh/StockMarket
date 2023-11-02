using MaterialsStockMarket.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaterialsStockMarket.Entities.Seller.Queries;

//обработчик запрос на проверку существования продавца
public class CheckSellerIdQueryHandler : BaseHandler, IRequestHandler<CheckSellerIdQuery, Boolean>
{
    public CheckSellerIdQueryHandler(StockMarketDb dbContext) : base(dbContext)
    { }

    public async Task<bool> Handle(CheckSellerIdQuery request, CancellationToken cancellationToken)
    {
        //поиск продавца с указаным id
        return await _dbContext.Sellers.AnyAsync(seller => seller.Id == request.Id);
    }
}