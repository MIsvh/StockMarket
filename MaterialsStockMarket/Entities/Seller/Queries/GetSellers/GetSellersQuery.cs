using MediatR;

namespace MaterialsStockMarket.Entities.Seller.Queries;

//запрос на получение списка продавцов
public record GetSellersQuery : IRequest<List<Seller>>;