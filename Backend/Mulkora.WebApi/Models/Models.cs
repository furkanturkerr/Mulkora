namespace Mulkora.WebApi.Models;

public class TrueWayGeocodingResponse
{
    public List<TrueWayGeocodingResult> Results { get; set; } = [];
}

public class TrueWayGeocodingResult
{
    public string Address { get; set; } = string.Empty;

    public TrueWayLocation Location { get; set; } = new();
}

public class TrueWayLocation
{
    public double Lat { get; set; }

    public double Lng { get; set; }
}

public class CoordinateResult
{
    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string FormattedAddress { get; set; } = string.Empty;
}