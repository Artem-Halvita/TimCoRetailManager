using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDataManager.Library.Internal.DataAccess
{
    internal class SqlDataAccess : IDisposable
    {
        string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public List<T> LoadData<T, U>(string storedParameter, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storedParameter, parameters,
                    commandType: CommandType.StoredProcedure).ToList();

                return rows;
            }
        }

        public void SaveData<T>(string storedParameter, T parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storedParameter, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public void StartTransaction(string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            _connection = new SqlConnection(connectionString);
            _connection.Open();

            _transaction = _connection.BeginTransaction();
        }

        public List<T> LoadDataInTransaction<T, U>(string storedParameter, U parameters)
        {
            List<T> rows = _connection.Query<T>(storedParameter, parameters,
                    commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();

            return rows;
        }

        public void SaveDataInTransaction<T>(string storedParameter, T parameters)
        {
            _connection.Execute(storedParameter, parameters,
                    commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();
        }

        public void Dispose()
        {
            CommitTransaction();
        }

        // Open connect/start transaction method
        // load using transaction
        // save using transaction
        // Close connection/stop transaction method
        // Dispose
    }
}
