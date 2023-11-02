using MediatR;

namespace MaterialsStockMarket.Entities.Material.Queries;

//запрос на получение материала по идентификатору
public record GetMaterialByIdQuery() : IRequest<Material?>
{ 
    public int Id { get; set; }
}