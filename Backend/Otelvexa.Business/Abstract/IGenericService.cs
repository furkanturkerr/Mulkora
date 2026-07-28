namespace Otelvexa.Business.Abstract;

public interface IGenericService<TResultDto, TCreateDto, TUpdateDto>
{
    Task<List<TResultDto>> TGetListAsync();
    Task<TUpdateDto> TGetByIdAsync(int id);
    Task TInsertAsync(TCreateDto dto);
    Task TUpdateAsync(TUpdateDto dto);
    Task TDeleteAsync(int id);
}