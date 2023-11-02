using MediatR;

namespace MaterialsStockMarket.Entities.Material.Queries;

//запрос на получение списка всех материалов
public record GetMaterialsQuery() : IRequest<List<Material>>;
