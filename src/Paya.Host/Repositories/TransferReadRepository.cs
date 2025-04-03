using Dapper;
using Microsoft.Extensions.Options;
using Paya.Host.Domain.Transfer;
using Paya.Host.Dtos;
using Paya.Host.Options;
using System.Data.SqlClient;
using System.Data;
using Paya.Host.Domain.Transfer.Enums;

namespace Paya.Host.Repositories
{
    public class TransferReadRepository : ITransferReadRepository
    {
        private readonly PayaOptions _payaOptions;
        public TransferReadRepository(IOptions<PayaOptions> payaOptions)
        {
            _payaOptions = payaOptions.Value;
        }

        public async Task<TransferRequestDto> GetTransferRequest(int requestId)
        {
            string query = $@"select 
                                    Id ,
                                    Price,
                                    FromShebaNumber,
                                    ToShebaNumber, 
                                    Status, 
                                    CreatedAt
                              From 
                                    [transfer].[TransferRequest] 
                              WHERE Id = @Id ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", requestId, DbType.Int32, null);

            using (var connection = new SqlConnection(_payaOptions.ReadContext))
            {
                var result =
                    await connection.QueryFirstOrDefaultAsync<TransferRequestDto>(query, parameters)
                    .ConfigureAwait(false);

                return result;
            }
        }

        public async Task<IEnumerable<TransferRequestDto>> GetTransferRequests()
        {
            string query = $@"select 
                                    Id ,
                                    Price,
                                    FromShebaNumber,
                                    ToShebaNumber, 
                                    Status, 
                                    CreatedAt
                              From 
                                    [transfer].[TransferRequest]
                            ORDER BY CreatedAt DESC";

            var parameters = new DynamicParameters();
            parameters.Add("Status", TransferRequestStatus.Pending, DbType.Int32, null);

            using (var connection = new SqlConnection(_payaOptions.ReadContext))
            {
                var result =
                    await connection.QueryAsync<TransferRequestDto>(query, parameters)
                    .ConfigureAwait(false);

                return result;
            }
        }
    }
}
