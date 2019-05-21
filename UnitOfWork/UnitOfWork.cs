using Data;
using Data.Interfaces;

namespace Business
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(string connectionString)
        {
            User = new UserRepository(connectionString);
        }

        public IUserRepository User { get; private set; }
    }
}
