using FluentValidation;

namespace MaterialsStockMarket.Entities.Seller;

//Класс проверки валидности полей класса Seller
public class SellerValidator : AbstractValidator<Seller>
{
    public SellerValidator()
    {
        //Имя должно быть не пустым
        RuleFor(seller => seller.Name).NotEmpty();
    }
}