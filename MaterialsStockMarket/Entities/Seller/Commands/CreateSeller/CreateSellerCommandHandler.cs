using MaterialsStockMarket.Data;
using MediatR;

namespace MaterialsStockMarket.Entities.Seller.Commands;

//обработчик команды на создание продавца
public class CreateSellerCommandHandler : BaseHandler, IRequestHandler<CreateSellerCommand, Seller?>
{
    public CreateSellerCommandHandler(StockMarketDb dbContext) : base(dbContext)
    { }
    
    public async Task<Seller?> Handle(CreateSellerCommand request, CancellationToken cancellationToken)
    {
        //при id не равном нулю поиск уже существующего продавца
        if (request.Seller.Id != 0 )
        {
            var existingSeller = await _dbContext.SellingMaterials.FindAsync(request.Seller.Id);
            //если материал с таким id существует, то добавление продавца не происходит
            if (existingSeller != null)
                return null;
        }
        
        //добавление продавца с указаным id или генерация id если он равен 0
        var result = await _dbContext.Sellers.AddAsync(request.Seller);
        //сохранение изменений в бд
        await _dbContext.SaveChangesAsync();
        //возвращение добавленного продавца
        return result.Entity;
    }
}