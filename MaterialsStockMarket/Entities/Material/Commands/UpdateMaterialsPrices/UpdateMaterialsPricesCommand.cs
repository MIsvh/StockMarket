using MediatR;

namespace MaterialsStockMarket.Entities.Material.Commands;

//команда для случайного изменения цен всех материалов в диапазоне MinPrice - MaxPrice
public record UpdateMaterialsPricesCommand : IRequest<Boolean>
{
    public decimal MaxPrice { get; set; } = 100;
    public decimal MinPrice { get; set; } = 0;
}