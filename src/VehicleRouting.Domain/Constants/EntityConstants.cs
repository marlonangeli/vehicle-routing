namespace VehicleRouting.Domain.Constants;

public static class EntityConstants
{
    public static class Driver
    {
        public const int NameMaxLength = 64;
    }

    public static class Place
    {
        public const int NameMaxLength = 100;
        public const int DescriptionMaxLength = 255;
        public const int FullAddressMaxLength = 255;
    }

    public static class Vehicle
    {
        public const int PlateMaxLength = 8;
        public const int ModelBrandMaxLength = 32;
        public const int ModelNameMaxLength = 32;
    }
}