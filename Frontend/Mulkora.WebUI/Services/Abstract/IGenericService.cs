namespace Mulkora.WebUI.Services.Abstract;

public interface IGenericService<TResultDto, TCreateDto, TUpdateDto>
{
    Task<List<TResultDto>> GetAllAsync();
    Task<TUpdateDto?> TGetByIdAsync(int id, string token);
    Task<HttpResponseMessage> TInsertAsync(TCreateDto dto, string token);
    Task<HttpResponseMessage> TUpdateAsync(TUpdateDto dto, string token);
    Task<HttpResponseMessage> TDeleteAsync(int id, string token);
}