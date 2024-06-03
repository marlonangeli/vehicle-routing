using FluentValidation;
using VehicleRouting.Application.Common.Messaging;
using VehicleRouting.Domain.Constants;
using VehicleRouting.Domain.Entities;
using VehicleRouting.Domain.Interfaces;

namespace VehicleRouting.Application.Core.Places;

public static class CreatePlace
{
    public sealed record Command(
        string Name,
        string? Description,
        string? FullAddress,
        double? Latitude,
        double? Longitude) : ICommand<Guid>;

    internal class Handler(IPlaceRepository repository) : ICommandHandler<Command, Guid>
    {
        public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
        {
            var place = new Place
            {
                Name = request.Name,
                Description = request.Description,
                Address = new Address
                {
                    FullAddress = request.FullAddress,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude
                }
            };

            await repository.AddAsync(place);
            await repository.SaveChangesAsync();

            return place.Id;
        }
    }

    internal class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(EntityConstants.Place.NameMaxLength);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(EntityConstants.Place.DescriptionMaxLength);

            When(x => !string.IsNullOrEmpty(x.FullAddress), () =>
            {
                RuleFor(x => x.FullAddress)
                    .MaximumLength(EntityConstants.Place.FullAddressMaxLength);
            });

            When(x => x.Latitude is not null && x.Longitude is not null, () =>
            {
                RuleFor(x => x.Latitude)
                    .InclusiveBetween(-90, 90);

                RuleFor(x => x.Longitude)
                    .InclusiveBetween(-180, 180);
            });

            When(x => string.IsNullOrEmpty(x.FullAddress) && (x.Latitude is null || x.Longitude is null), () =>
            {
                RuleFor(x => x.FullAddress)
                    .NotEmpty();

                RuleFor(x => x.Latitude)
                    .NotNull();

                RuleFor(x => x.Longitude)
                    .NotNull();
            });

            RuleFor(x => x)
                .Must(x => x.FullAddress is not null || (x.Latitude is not null && x.Longitude is not null))
                .WithMessage("Full address or latitude and longitude must be provided.");
        }
    }
}