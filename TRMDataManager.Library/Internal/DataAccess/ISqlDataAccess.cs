using System.Collections.Generic;

namespace TRMDataManager.Library.Internal.DataAccess
{
    public interface ISqlDataAccess
    {
        void CommitTransaction();
        void Dispose();
        List<T> LoadData<T, U>(string storedParameter, U parameters, string connectionStringName);
        List<T> LoadDataInTransaction<T, U>(string storedParameter, U parameters);
        void RollbackTransaction();
        void SaveData<T>(string storedParameter, T parameters, string connectionStringName);
        void SaveDataInTransaction<T>(string storedParameter, T parameters);
        void StartTransaction(string connectionStringName);
    }
}