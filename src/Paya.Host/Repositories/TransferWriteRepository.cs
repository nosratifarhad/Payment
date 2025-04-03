using Dapper;
using Microsoft.Extensions.Options;
using Paya.Host.Domain.Transfer;
using Paya.Host.Domain.Transfer.Entities;
using Paya.Host.Dtos;
using Paya.Host.Options;
using System.Data;
using System.Data.SqlClient;

namespace Paya.Host.Repositories
{
    public class TransferWriteRepository : ITransferWriteRepository
    {
        private readonly PayaOptions _payaOptions;
        public TransferWriteRepository(IOptions<PayaOptions> payaOptions)
        {
            _payaOptions = payaOptions.Value;
        }

        public async Task<int> CreateTransferRequest(TransferRequest transferRequest)
        {
            string command = GetInsertTransferRequestCommand();
            var parameters = GetTransferRequestParameters(transferRequest);

            using (var connection = new SqlConnection(_payaOptions.WriteContext))
            {
                var transferRequestId = await connection.ExecuteScalarAsync<int>(command, parameters)
                                                        .ConfigureAwait(false);

                return transferRequestId;
            }
        }

        private string GetInsertTransferRequestCommand()
            => @"
                INSERT INTO [transfer].[TransferRequest]
                    (UserId, Price, FromShebaNumber, ToShebaNumber, TransferStatus, Note, CreatedAt)
                OUTPUT INSERTED.Id
                VALUES
                    (@UserId, @Price, @FromShebaNumber, @ToShebaNumber, @TransferStatus, @Note, @CreatedAt)";


        private DynamicParameters GetTransferRequestParameters(TransferRequest dto)
        {
            var parameters = new DynamicParameters();

            parameters.Add("UserId", dto.UserId, DbType.Int32);
            parameters.Add("Price", dto.Price, DbType.Decimal);
            parameters.Add("FromShebaNumber", dto.FromShebaNumber, DbType.String);
            parameters.Add("ToShebaNumber", dto.ToShebaNumber, DbType.String);
            parameters.Add("TransferStatus", dto.TransferStatus.ToString(), DbType.String);
            parameters.Add("Note", dto.Note, DbType.String);
            parameters.Add("CreatedAt", DateTime.UtcNow, DbType.DateTime);

            return parameters;
        }

    }
}
