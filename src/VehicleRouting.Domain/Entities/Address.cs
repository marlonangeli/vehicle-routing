namespace VehicleRouting.Domain.Entities;

public class Address
{
    public string? PlusCode { get; set; }
    public (double Lat, double Lon) LatitudeLongitude { get; set; }
    public string? FullAddress { get; set; }

    public Address(double lat, double lon)
    {
        if (IsValidLatitudeLongitude(lat, lon))
            LatitudeLongitude = (lat, lon);
    }

    public Address(string address)
    {
        if (IsValidPlusCode(address))
            PlusCode = address;
        else if (IsValidFullAddress(address))
            FullAddress = address;
    }


    private bool IsValidLatitudeLongitude(double lat, double lon)
    {
        if (lat is < -90 or > 90)
        {
            throw new ArgumentOutOfRangeException(nameof(lat), "Latitude must be between -90 and 90 degrees.");
        }
        if (lon is < -180 or > 180)
        {
            throw new ArgumentOutOfRangeException(nameof(lon), "Longitude must be between -180 and 180 degrees.");
        }

        return true;
    }

    private bool IsValidPlusCode(string plusCode)
    {
        if (plusCode.Length is < 4 or > 11)
            return false;

        const string validChars = "23456789CFGHJMPQRVWX";
        return !plusCode.Any(c => c != '+' && !validChars.Contains(c)) && plusCode.Contains('+');
    }

    private bool IsValidFullAddress(string fullAddress)
    {
        if (string.IsNullOrWhiteSpace(fullAddress))
        {
            throw new ArgumentException("Full address cannot be null or empty.", nameof(fullAddress));
        }

        return true;
    }
}