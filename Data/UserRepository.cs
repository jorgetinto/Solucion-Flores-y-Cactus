using Dapper;
using Data.Interfaces;
using Entity;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        public UserEntity ValidateUser(string email, string password)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@email", email);
            parameters.Add("@password", password);

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<UserEntity>("dbo.ValidateUser",
                                                    parameters,
                                                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
