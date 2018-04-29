//#define WRAP_CONNECTION

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq.Expressions;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace Repository.Core
{
    public class SqlConnectionProvider : IDbConnectionProvider
    {
        private readonly IConnectionStringProvider _connectionStringProvider;
        private readonly DbProviderFactory _factory;

        public SqlConnectionProvider(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
           
            OrmLiteConfig.DialectProvider = SqlServerOrmLiteDialectProvider.Instance;
            _factory = SqlClientFactory.Instance;
        }

        public IDbConnection OpenConnection()
        {
            if(!_connectionStringProvider.HasConnectionString)
                throw new Exception("There is no connection string configured!");

            var connection = _factory.CreateConnection();
            if (connection == null) throw new Exception("Couldn't create the connection from the factory.");
            connection.ConnectionString = _connectionStringProvider.ConnectionString;
            connection.Open();
 
            return connection;
 
        }

        public void Perform(Action<IDbConnection> action)
        {
            using (var conn = OpenConnection())
                action(conn);
        }

        public T Perform<T>(Func<IDbConnection, T> func)
        {
            using (var conn = OpenConnection())
                return func(conn);
        }

        private void SetupUtcDateTime<TType>(Expression<Func<TType, object>> property, Action<TType, DateTime> updateInstance)
        {
            ModelDefinition<TType>.Definition.GetFieldDefinition(property).SetValueFn =
                (instance, value) => updateInstance((TType)instance, DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc));
        }

        private void SetupUtcNullableDateTime<TType>(Expression<Func<TType, object>> property, Action<TType, DateTime?> updateInstance)
        {
            ModelDefinition<TType>.Definition.GetFieldDefinition(property).SetValueFn =
                (instance, value) => updateInstance((TType)instance, value == null ? (DateTime?)null : DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc));
        }

 
    }
}
