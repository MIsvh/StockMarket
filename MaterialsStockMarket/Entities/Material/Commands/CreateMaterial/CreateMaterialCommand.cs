using MediatR;

namespace MaterialsStockMarket.Entities.Material.Commands;

//команда для создания материала
public record CreateMaterialCommand : IRequest<Material?>
{
    public Material Material { get; set; }
}