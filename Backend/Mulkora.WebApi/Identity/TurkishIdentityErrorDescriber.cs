using Microsoft.AspNetCore.Identity;

namespace Mulkora.WebApi.Identity;

public class TurkishIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateEmail),
            Description = "Bu e-posta adresi zaten kayıtlı."
        };
    }

    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateUserName),
            Description = "Bu e-posta adresi zaten kayıtlı."
        };
    }
}