using MediatR;

namespace MaterialsStockMarket.Entities.Material.Commands;

//команда для изменения материала
public record UpdateMaterialCommand : IRequest<Material?>
{
    public Material Material { get; set; }
}