using MediatR;

namespace MaterialsStockMarket.Entities.Seller.Commands;

//команда на создание продавца
public record CreateSellerCommand : IRequest<Seller?>
{
    public Seller Seller { get; set; }
}