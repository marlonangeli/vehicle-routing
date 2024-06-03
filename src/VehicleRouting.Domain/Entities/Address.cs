namespace VehicleRouting.Domain.Entities;

public class Address
{
    // public string? PlusCode { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? FullAddress { get; set; }

    public Address(double? lat, double? lon, string? address)
    {
        if (lat is null || lon is null)
            throw new ArgumentNullException(nameof(lat), "Latitude and longitude cannot be null.");


        if (!IsValidLatitudeLongitude((double)lat, (double)lon) || !IsValidFullAddress(address))
        {
            throw new ArgumentException("Invalid address.", nameof(address));
        }

        Latitude = lat;
        Longitude = lon;
        FullAddress = address;
    }

    public Address(double lat, double lon)
    {
        if (IsValidLatitudeLongitude(lat, lon))
        {
            Latitude = lat;
            Longitude = lon;
        }
    }

    public Address(string address)
    {
        // if (IsValidPlusCode(address))
        //     PlusCode = address;
        // else if (IsValidFullAddress(address))
        //     FullAddress = address;
        if (IsValidFullAddress(address))
            FullAddress = address;
    }

    public Address() {}


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