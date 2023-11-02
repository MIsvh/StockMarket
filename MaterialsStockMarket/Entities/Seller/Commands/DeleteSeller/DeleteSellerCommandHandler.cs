using MaterialsStockMarket.Data;
using MediatR;

namespace MaterialsStockMarket.Entities.Seller.Commands;

//обработчик команды на удаление продавца
public class DeleteSellerCommandHandler : BaseHandler, IRequestHandler<DeleteSellerCommand, Boolean>
{
    public DeleteSellerCommandHandler(StockMarketDb dbContext) : base(dbContext)
    { }
    
    public async Task<Boolean> Handle(DeleteSellerCommand request, CancellationToken cancellationToken)
    {
        //поиск продавца с нужным id
        var seller = await _dbContext.Sellers.FindAsync(request.Id);

        if (seller == null)
            return false;
        
        //если найден - его удаление и сохранения изменений бд
        _dbContext.Sellers.Remove(seller);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}