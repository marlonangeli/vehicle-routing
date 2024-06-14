using FluentValidation;
using VehicleRouting.Application.Common.Messaging;
using VehicleRouting.Domain.Constants;
using VehicleRouting.Domain.Entities;
using VehicleRouting.Domain.Interfaces;

namespace VehicleRouting.Application.Core.Drivers;

public class CreateDriver
{
    public sealed record Command(
        string Name,
        LicenseType License) : ICommand<Guid>;

    internal sealed class Handler(IDriverRepository repository) : ICommandHandler<Command, Guid>
    {
        public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
        {
            var driver = new Driver
            {
                Name = request.Name,
                License = request.License,
                Status = DriverStatus.NotAvailable
            };

            await repository.AddAsync(driver);
            await repository.SaveChangesAsync();

            return driver.Id;
        }
    }

    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(EntityConstants.Driver.NameMaxLength);

            RuleFor(x => x.License)
                .IsInEnum();
        }
    }
}