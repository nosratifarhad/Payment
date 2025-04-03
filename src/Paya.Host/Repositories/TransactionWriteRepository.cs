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
    public class TransactionWriteRepository : ITransactionWriteRepository
    {
        private readonly PayaOptions _payaOptions;
        public TransactionWriteRepository(IOptions<PayaOptions> payaOptions)
        {
            _payaOptions = payaOptions.Value;
        }

        public async Task CreateTransferRequest(Transaction transaction)
        {
            string command = GetInsertTransactionCommand();

            var parameters = GetTransactionParameters(transaction);

            using (var connection = new SqlConnection(_payaOptions.WriteContext))
            {
                await connection.ExecuteAsync(command, parameters)
                                .ConfigureAwait(false);
            }
        }

        private string GetInsertTransactionCommand()
            => @"INSERT INTO [transaction].[Transaction]
                    (TransferRequestId, Price, Description, TransactionType, CreatedAt)
                VALUES
                    (@TransferRequestId, @Price, @Description, @TransactionType, @CreatedAt)";

        private DynamicParameters GetTransactionParameters(Transaction transaction)
        {
            var parameters = new DynamicParameters();

            parameters.Add("TransferRequestId", transaction.TransferRequestId, DbType.Int32);
            parameters.Add("Price", transaction.Price, DbType.Decimal);
            parameters.Add("Description", transaction.Description, DbType.String);
            parameters.Add("TransactionType", transaction.TransactionType.ToString(), DbType.String);
            parameters.Add("CreatedAt", DateTime.UtcNow, DbType.DateTime);

            return parameters;
        }
    }
}
