using Mulkora.Dto.PropertyDtos;

namespace Mulkora.WebApi.Services.OpenAIServices;

public interface IOpenAIService
{
    Task<bool> CheckPropertyAsync(GetByIdPropertyDto property);
}