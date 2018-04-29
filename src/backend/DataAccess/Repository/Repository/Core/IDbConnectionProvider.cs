using System;
using System.Data;

namespace Repository.Core
{
    public interface IDbConnectionProvider
    {
        IDbConnection OpenConnection();

        void Perform(Action<IDbConnection> action);

        T Perform<T>(Func<IDbConnection, T> func);
    }
}
