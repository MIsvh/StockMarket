using MediatR;

namespace MaterialsStockMarket.Entities.Seller.Queries;

//запрос на получение продавца по указаному Id
public record GetSellerByIdQuery : IRequest<Seller?>
{
    public int Id { get; set; }
}