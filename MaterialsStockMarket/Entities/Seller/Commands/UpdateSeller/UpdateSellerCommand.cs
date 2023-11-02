using MediatR;

namespace MaterialsStockMarket.Entities.Seller.Commands;

//команда на изменение продавца
public record UpdateSellerCommand : IRequest<Seller?>
{
    public Seller Seller { get; set; }
}