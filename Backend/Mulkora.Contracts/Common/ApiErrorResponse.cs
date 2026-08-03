namespace Mulkora.Dto.Common;

public class ApiErrorResponse
{
    public int StatusCode { get; set; }

    public string Message { get; set; } = string.Empty;

    public string? Path { get; set; }

    public string? TraceId { get; set; }

    public Dictionary<string, string[]>? Errors { get; set; }
}