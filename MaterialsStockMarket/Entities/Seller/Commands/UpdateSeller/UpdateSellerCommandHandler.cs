using MaterialsStockMarket.Data;
using MediatR;

namespace MaterialsStockMarket.Entities.Seller.Commands;

//обработчик команды на изменение продавца
public class UpdateSellerCommandHandler : BaseHandler, IRequestHandler<UpdateSellerCommand, Seller?>
{
    public UpdateSellerCommandHandler(StockMarketDb dbContext) : base(dbContext)
    { }
    
    public async Task<Seller?> Handle(UpdateSellerCommand request, CancellationToken cancellationToken)
    {
        //поиск продавца с нужным id
        var result = await _dbContext.Sellers.FindAsync(request.Seller.Id);

        if (result == null)
            return null;

        //изменение полей продавца, если был найден
        result.Name = request.Seller.Name;

        //сохранение изменений в бд
        await _dbContext.SaveChangesAsync();
        return result;
    }
}