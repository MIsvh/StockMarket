using MediatR;

namespace MaterialsStockMarket.Entities.Seller.Queries;

//запрос на проверку существования продавца с указаным id
public record CheckSellerIdQuery : IRequest<Boolean>
{
    public int Id { get; set; }
}