using VehicleRouting.Application.Common.Messaging;
using VehicleRouting.Domain.Entities;
using VehicleRouting.Domain.Interfaces;

namespace VehicleRouting.Application.Core.Drivers;

public class GetDrivers
{
    public sealed record Query : IQuery<IReadOnlyList<Driver>>;

    internal sealed class Handler(IDriverRepository repository) : IQueryHandler<Query, IReadOnlyList<Driver>>
    {
        public async Task<IReadOnlyList<Driver>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await repository.ListAllAsync();
        }
    }
}