using VehicleRouting.Application.Common.Messaging;
using VehicleRouting.Domain.Entities;
using VehicleRouting.Domain.Interfaces;

namespace VehicleRouting.Application.Core.Vehicles;

public class GetVehicles
{
    public sealed record Query : IQuery<IReadOnlyList<Vehicle>>;

    internal sealed class Handler(IVehicleRepository repository) : IQueryHandler<Query, IReadOnlyList<Vehicle>>
    {
        public async Task<IReadOnlyList<Vehicle>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await repository.ListAllAsync();
        }
    }
}