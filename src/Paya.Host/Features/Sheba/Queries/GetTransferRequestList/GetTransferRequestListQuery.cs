using MediatR;
using Paya.Host.Dtos;

namespace Paya.Host.Features.Sheba.Queries.GetTransferRequestList
{
    public record GetTransferRequestListQuery() : IRequest<IEnumerable<GetTransferRequestDto>>
    {
    }
}
