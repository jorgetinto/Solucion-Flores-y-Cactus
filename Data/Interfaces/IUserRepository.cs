using Entity;

namespace Data.Interfaces
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        UserEntity ValidateUser(string email, string password);
    }
}
