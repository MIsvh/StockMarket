using MediatR;

namespace MaterialsStockMarket.Entities.Seller.Commands;

//команда на удаление продавца
public record DeleteSellerCommand : IRequest<Boolean>
{
    public int Id { get; set; }
}