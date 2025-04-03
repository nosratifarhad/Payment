using MediatR;
using Paya.Host.Domain.Transfer;
using Paya.Host.Dtos;

namespace Paya.Host.Features.Sheba.Queries.GetTransferRequests
{
    public class GetTransferRequestsQueryHandler : IRequestHandler<GetTransferRequestsQuery, TransferRequestCollectionDto>
    {
        private readonly ITransferReadRepository _transferReadRepository;

        public GetTransferRequestsQueryHandler(ITransferReadRepository transferReadRepository)
        {
            _transferReadRepository = transferReadRepository;
        }

        public async Task<TransferRequestCollectionDto> Handle(GetTransferRequestsQuery request, CancellationToken cancellationToken)
        {
            var transferRequestDtos = await _transferReadRepository.GetTransferRequests();

            if (!transferRequestDtos.Any())
                return new TransferRequestCollectionDto()
                {
                    Requests = Enumerable.Empty<TransferRequestDto>()
                };

            var transferRequestCollectionDto = CreateTransferRequestCollectionDto(transferRequestDtos);

            return transferRequestCollectionDto;
        }

        private TransferRequestCollectionDto CreateTransferRequestCollectionDto(IEnumerable<TransferRequestDto> transferRequestDtos)
        {
            var TransferRequestDtos = new List<TransferRequestDto>();
            foreach (var transferRequest in transferRequestDtos)
            {
                TransferRequestDtos.Add(new TransferRequestDto()
                {
                    Id = transferRequest.Id,
                    FromShebaNumber = transferRequest.FromShebaNumber,
                    ToShebaNumber = transferRequest.ToShebaNumber,
                    Price = transferRequest.Price,
                    Status = transferRequest.Status,
                    CreatedAt = transferRequest.CreatedAt
                });
            }

            var transferRequestCollectionDto = new TransferRequestCollectionDto()
            {
                Requests = TransferRequestDtos
            };

            return transferRequestCollectionDto;
        }
    }
}
