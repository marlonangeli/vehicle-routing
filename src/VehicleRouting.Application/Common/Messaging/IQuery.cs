using MediatR;

namespace VehicleRouting.Application.Common.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}