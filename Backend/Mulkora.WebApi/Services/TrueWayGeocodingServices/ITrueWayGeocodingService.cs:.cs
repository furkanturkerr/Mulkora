using Mulkora.WebApi.Models;

namespace Mulkora.WebApi.Services.TrueWayGeocodingServices;

public interface ITrueWayGeocodingService
{
    Task<CoordinateResult?> GetCoordinatesAsync(string city, string district, string address);
}