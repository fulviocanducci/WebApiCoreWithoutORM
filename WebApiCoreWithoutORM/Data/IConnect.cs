using System;
using System.Data;

namespace WebApiCoreWithoutORM.Data
{
    public interface IConnect<T, TCommand> : IDisposable
        where T: IDbConnection
    {
        T Connect { get; set; }
        T Open();
        void Close();
        TCommand CreateCommand();
    }
}
