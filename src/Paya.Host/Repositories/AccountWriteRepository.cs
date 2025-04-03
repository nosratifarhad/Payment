using Dapper;
using Microsoft.Extensions.Options;
using Paya.Host.Domain.Account;
using Paya.Host.Domain.Account.Entities;
using Paya.Host.Options;
using System.Data.SqlClient;
using System.Data;

namespace Paya.Host.Repositories
{
    public class AccountWriteRepository : IAccountWriteRepository
    {
        private readonly PayaOptions _payaOptions;
        public AccountWriteRepository(IOptions<PayaOptions> payaOptions)
        {
            _payaOptions = payaOptions.Value;
        }

        public async Task UpdateUserAccount(UserAccount userAccount)
        {
            string command = GetUpdateUserAccountCommand();

            var parameters = GetUpdateUserAccountParamters(userAccount);

            using (var connection = new SqlConnection(_payaOptions.WriteContext))
            {
                await connection.ExecuteAsync(command, parameters)
                        .ConfigureAwait(false);
            }
        }

        private string GetUpdateUserAccountCommand()
              => @"UPDATE [identity].[User]
                    SET 
	                    UpdateAt = @UpdateAt ,    
                        Balance = @Balance
                    WHERE Id = @Id";

        private DynamicParameters GetUpdateUserAccountParamters(UserAccount userAccount)
        {
            var paramters = new DynamicParameters();

            paramters.Add("Id", userAccount.Id, DbType.Int32, null);
            paramters.Add("UpdateAt", userAccount.UpdateAt, DbType.DateTime, null);
            paramters.Add("Balance", userAccount.Balance, DbType.Decimal, null);

            return paramters;
        }

    }
}
