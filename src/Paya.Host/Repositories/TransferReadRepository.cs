using Dapper;
using Microsoft.Extensions.Options;
using Paya.Host.Domain.Account.Entities;
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

        public async Task<IEnumerable<GetTransferRequestDto>> GetTransferRequests()
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
            parameters.Add("Status", TransferRequestStatus.Reserved, DbType.Int32, null);

            using (var connection = new SqlConnection(_payaOptions.ReadContext))
            {
                var result =
                    await connection.QueryAsync<GetTransferRequestDto>(query, parameters)
                    .ConfigureAwait(false);

                return result;
            }
        }
    }
}
