using System.Collections.Generic;

namespace WebApiCoreWithoutORM.Data
{
    public interface IDal<T, TCommand>
    {
        T Add(T model);
        bool Edit(T model);
        bool Remove(T model);
        bool Remove(object id);
        T Find(object id);
        IEnumerable<T> All();
        void AddParameters(TCommand command, T model);
    }
}
