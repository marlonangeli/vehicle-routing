using VehicleRouting.Application.Common.Messaging;
using VehicleRouting.Domain.Entities;
using VehicleRouting.Domain.Interfaces;

namespace VehicleRouting.Application.Core.Places;

public static class GetPlaces
{
    public sealed record Query : IQuery<IReadOnlyList<Place>>;

    public class Handler(IPlaceRepository repository) : IQueryHandler<Query, IReadOnlyList<Place>>
    {
        public async Task<IReadOnlyList<Place>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await repository.ListAllAsync();
        }
    }
}