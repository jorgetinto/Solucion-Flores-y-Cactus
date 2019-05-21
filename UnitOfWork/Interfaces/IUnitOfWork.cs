using Data.Interfaces;

namespace Business
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
    }
}
