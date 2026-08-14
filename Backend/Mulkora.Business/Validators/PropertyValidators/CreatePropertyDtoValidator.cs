using FluentValidation;
using Mulkora.Dto.PropertyDtos;

namespace Mulkora.Business.Validators.PropertyValidators;

public class CreatePropertyDtoValidator : AbstractValidator<CreatePropertyDto>
{
    public CreatePropertyDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("İlan başlığı boş bırakılamaz.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("İlan açıklaması boş bırakılamaz.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Fiyat sıfırdan büyük olmalıdır.");

        RuleFor(x => x.ListingType)
            .NotEmpty()
            .WithMessage("İlan türü seçilmelidir.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .WithMessage("Kategori seçilmelidir.");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("Şehir boş bırakılamaz.");

        RuleFor(x => x.District)
            .NotEmpty()
            .WithMessage("İlçe boş bırakılamaz.");

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Adres boş bırakılamaz.");

        RuleFor(x => x.RoomCount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Oda sayısı negatif olamaz.");

        RuleFor(x => x.LivingRoomCount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Salon sayısı negatif olamaz.");

        RuleFor(x => x.BathroomCount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Banyo sayısı negatif olamaz.");

        RuleFor(x => x.GrossSquareMeter)
            .GreaterThan(0)
            .WithMessage("Brüt metrekare sıfırdan büyük olmalıdır.");

        RuleFor(x => x.NetSquareMeter)
            .GreaterThan(0)
            .WithMessage("Net metrekare sıfırdan büyük olmalıdır.");

        RuleFor(x => x.NetSquareMeter)
            .LessThanOrEqualTo(x => x.GrossSquareMeter)
            .WithMessage("Net metrekare brüt metrekareden büyük olamaz.");

        RuleFor(x => x.BuildingAge)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Bina yaşı negatif olamaz.");

        RuleFor(x => x.TotalFloor)
            .GreaterThan(0)
            .WithMessage("Toplam kat sayısı sıfırdan büyük olmalıdır.");

        RuleFor(x => x.FloorNumber)
            .LessThanOrEqualTo(x => x.TotalFloor)
            .WithMessage("Bulunduğu kat toplam kat sayısından büyük olamaz.");
    }
}