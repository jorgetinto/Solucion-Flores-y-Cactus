using System.Collections.Generic;

namespace Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        bool Delete(T entientity);
        bool Update(T entientity);
        int Insert(T entientity);
        IEnumerable<T> GetList();
        T GetById(int id);
    }
}
