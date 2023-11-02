using MediatR;

namespace MaterialsStockMarket.Entities.Material.Commands;

//команда для удаления материала
public record DeleteMaterialCommand : IRequest<Boolean>
{
    public int Id { get; set; }
}