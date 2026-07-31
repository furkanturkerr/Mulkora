using FluentValidation;
using Otelvexa.Dto.AuthDtos;

namespace Otelvexa.Business.Validators.AuthValidators;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Ad alanı boş bırakılamaz.")
            .MaximumLength(50)
            .WithMessage("Ad en fazla 50 karakter olabilir.");

        RuleFor(x => x.Surname)
            .NotEmpty()
            .WithMessage("Soyad alanı boş bırakılamaz.")
            .MaximumLength(50)
            .WithMessage("Soyad en fazla 50 karakter olabilir.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("E-posta alanı boş bırakılamaz.")
            .EmailAddress()
            .WithMessage("Geçerli bir e-posta adresi giriniz.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Şifre alanı boş bırakılamaz.")
            .MinimumLength(6)
            .WithMessage("Şifre en az 6 karakter olmalıdır.")
            .Matches("[A-Z]")
            .WithMessage("Şifre en az bir büyük harf içermelidir.")
            .Matches("[a-z]")
            .WithMessage("Şifre en az bir küçük harf içermelidir.")
            .Matches("[0-9]")
            .WithMessage("Şifre en az bir rakam içermelidir.");
    }
}