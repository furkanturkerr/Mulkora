using FluentValidation;
using Mulkora.Dto.AgentDtos;

namespace Mulkora.Business.Validators.AgentValidators;

public class CreateAgentDtoValidator : AbstractValidator<CreateAgentDto>
{
    public CreateAgentDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad alanı boş bırakılamaz.")
            .MinimumLength(2).WithMessage("Ad en az 2 karakter olmalıdır.")
            .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir.");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Soyad alanı boş bırakılamaz.")
            .MinimumLength(2).WithMessage("Soyad en az 2 karakter olmalıdır.")
            .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta adresi boş bırakılamaz.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
            .MaximumLength(256).WithMessage("E-posta adresi en fazla 256 karakter olabilir.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre boş bırakılamaz.")
            .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
            .MaximumLength(100).WithMessage("Şifre en fazla 100 karakter olabilir.")
            .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
            .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
            .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Unvan alanı boş bırakılamaz.")
            .MinimumLength(3).WithMessage("Unvan en az 3 karakter olmalıdır.")
            .MaximumLength(100).WithMessage("Unvan en fazla 100 karakter olabilir.");

        RuleFor(x => x.About)
            .NotEmpty().WithMessage("Danışman açıklaması boş bırakılamaz.")
            .MinimumLength(20).WithMessage("Danışman açıklaması en az 20 karakter olmalıdır.")
            .MaximumLength(1000).WithMessage("Danışman açıklaması en fazla 1000 karakter olabilir.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Şehir alanı boş bırakılamaz.")
            .MaximumLength(50).WithMessage("Şehir en fazla 50 karakter olabilir.");

        RuleFor(x => x.District)
            .NotEmpty().WithMessage("İlçe alanı boş bırakılamaz.")
            .MaximumLength(50).WithMessage("İlçe en fazla 50 karakter olabilir.");

        RuleFor(x => x.OfficeName)
            .MaximumLength(100).WithMessage("Ofis adı en fazla 100 karakter olabilir.");

        RuleFor(x => x.LicenseNumber)
            .MaximumLength(100).WithMessage("Lisans numarası en fazla 100 karakter olabilir.");

        RuleFor(x => x.ImageUrl)
            .MaximumLength(500).WithMessage("Görsel bağlantısı en fazla 500 karakter olabilir.")
            .Must(BeValidUrl).WithMessage("Geçerli bir görsel bağlantısı giriniz.")
            .When(x => !string.IsNullOrWhiteSpace(x.ImageUrl));

        RuleFor(x => x.ExperienceYear)
            .InclusiveBetween(0, 60)
            .WithMessage("Deneyim yılı 0 ile 60 arasında olmalıdır.");
    }

    private static bool BeValidUrl(string? imageUrl)
    {
        return Uri.TryCreate(imageUrl, UriKind.Absolute, out var uri)
               && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
    }
}