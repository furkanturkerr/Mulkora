namespace Mulkora.WebUI.Services.Abstract;

public interface IGenericService<TResultDto, TCreateDto, TUpdateDto>
{
    Task<List<TResultDto>> GetAllAsync();
    Task<TUpdateDto?> TGetByIdAsync(int id);
    Task<HttpResponseMessage> TInsertAsync(TCreateDto dto);
    Task<HttpResponseMessage> TUpdateAsync(TUpdateDto dto);
    Task<HttpResponseMessage> TDeleteAsync(int id);
}