using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TRMApi.Data.Repository.DataAccess
{
    internal class SqlDataAccess : IDisposable
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        private bool isClosed = false;
        private IConfiguration _config;
        private string _connectionString;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
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

        public async Task<IEnumerable<T>> LoadDataAsync<T, U>(string storedParameter, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<T>(storedParameter, parameters,
                    commandType: CommandType.StoredProcedure);

                return rows;
            }
        }

        public async Task SaveDataAsync<T>(string storedParameter, T parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(storedParameter, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public void RunInTransaction(Action<SqlDataAccess> action, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            try
            {
                StartTransaction(connectionString);

                action(this);

                CommitTransaction();
            }
            catch (Exception)
            {
                RollbackTransaction();
                throw;
            }
        }

        public void StartTransaction(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();

            _transaction = _connection.BeginTransaction();

            isClosed = false;
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

        private string GetConnectionString(string name)
        {
            if (_connectionString == null)
            {
                _connectionString = _config.GetConnectionString(name);
            }

            return _connectionString;
        }

        private void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();

            isClosed = true;
        }

        private void RollbackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();

            isClosed = true;
        }

        public void Dispose()
        {
            if (isClosed == false)
            {
                try
                {
                    CommitTransaction();
                }
                catch 
                {
                    // TODO - Log this issue
                }
            }

            _transaction = null;
            _connection = null;
        }

        // Open connect/start transaction method
        // load using transaction
        // save using transaction
        // Close connection/stop transaction method
        // Dispose
    }
}
