using MediatR;
using Paya.Host.Domain.Transfer;
using Paya.Host.Dtos;

namespace Paya.Host.Features.Sheba.Queries.GetTransferRequestList
{
    public class GetTransferRequestQueryHandler : IRequestHandler<GetTransferRequestListQuery, IEnumerable<GetTransferRequestDto>>
    {
        private readonly ITransferReadRepository _transferReadRepository;
        public GetTransferRequestQueryHandler(ITransferReadRepository transferReadRepository)
        {
            _transferReadRepository = transferReadRepository;
        }

        public async Task<IEnumerable<GetTransferRequestDto>> Handle(GetTransferRequestListQuery request, CancellationToken cancellationToken)
        {
            var transferRequestDtos = await _transferReadRepository.GetTransferRequests();
            if (!transferRequestDtos.Any())
                return Enumerable.Empty<GetTransferRequestDto>();
            
            return transferRequestDtos;
        }
    }
}
