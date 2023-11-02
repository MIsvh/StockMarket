using FluentValidation;

namespace MaterialsStockMarket.Entities.Material;

//Класс проверки валидности полей класса Material
public class MaterialValidator : AbstractValidator<Material>
{
    public MaterialValidator()
    {
        //Имя должно быть не пустым
        RuleFor(material => material.Name).NotEmpty();
        
        //Цена должна быть больше 0
        RuleFor(material => material.Price).GreaterThan(0);
    }
}