using MediatR;
using Paya.Host.Dtos;

namespace Paya.Host.Features.Sheba.Queries.GetTransferRequests
{
    public record GetTransferRequestsQuery() : IRequest<TransferRequestCollectionDto>
    {
    }
}
