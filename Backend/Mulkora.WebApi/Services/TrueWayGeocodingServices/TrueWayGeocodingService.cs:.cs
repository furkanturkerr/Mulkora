using Mulkora.WebApi.Models;

namespace Mulkora.WebApi.Services.TrueWayGeocodingServices;

public class TrueWayGeocodingService : ITrueWayGeocodingService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;
    private readonly ILogger<TrueWayGeocodingService> _logger;

    public TrueWayGeocodingService(HttpClient client, IConfiguration configuration, ILogger<TrueWayGeocodingService> logger)
    {
        _client = client;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<CoordinateResult?> GetCoordinatesAsync(string city, string district, string address)
    {
        var rapidApiKey = _configuration["RapidApi:Key"];

        if (string.IsNullOrWhiteSpace(rapidApiKey))
        {
            throw new Exception("RapidAPI anahtarı bulunamadı.");
        }

        var fullAddress = $"{address}, {district}, {city}, Türkiye";

        var requestUrl = $"Geocode?address={Uri.EscapeDataString(fullAddress)}&language=tr";

        using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

        request.Headers.Add(
            "X-RapidAPI-Key",
            rapidApiKey
        );

        request.Headers.Add(
            "X-RapidAPI-Host",
            "trueway-geocoding.p.rapidapi.com"
        );

        using var response = await _client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();

            _logger.LogWarning(
                "TrueWay isteği başarısız: {StatusCode} {Error}",
                response.StatusCode,
                error
            );

            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<TrueWayGeocodingResponse>();

        var firstResult = result?.Results.FirstOrDefault();

        if (firstResult == null)
        {
            return null;
        }

        return new CoordinateResult
        {
            Latitude = firstResult.Location.Lat,
            Longitude = firstResult.Location.Lng,
            FormattedAddress = firstResult.Address
        };
    }
}