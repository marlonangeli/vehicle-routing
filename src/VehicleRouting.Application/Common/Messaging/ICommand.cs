using MediatR;

namespace VehicleRouting.Application.Common.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}