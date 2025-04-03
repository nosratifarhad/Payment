using Dapper;
using Microsoft.Extensions.Options;
using Paya.Host.Domain.Account;
using Paya.Host.Domain.Account.Entities;
using Paya.Host.Options;
using System.Data;
using System.Data.SqlClient;

namespace Paya.Host.Repositories
{
    public class AccountReadRepository : IAccountReadRepository
    {
        private readonly PayaOptions _payaOptions;
        public AccountReadRepository(IOptions<PayaOptions> payaOptions)
        {
            _payaOptions = payaOptions.Value;
        }

        public async Task<UserAccount> GetUserAccount(int UserId)
        {
            string query = $@"select Id ,
                                     UserId ,
                                     Balance ,
                                     UpdateAt
                              From 
                              UserAccount 
                                    WHERE UserId = @UserId";

            var parameters = new DynamicParameters();
            parameters.Add("UserId", UserId, DbType.Int32, null);

            using (var connection = new SqlConnection(_payaOptions.ReadContext))
            {
                var result =
                    await connection.QueryFirstOrDefaultAsync<UserAccount>(query, parameters)
                    .ConfigureAwait(false);

                return result;
            }

        }

    }
}
