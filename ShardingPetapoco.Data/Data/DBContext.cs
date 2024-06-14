using PetaPoco;

namespace ShardingPetapoco.Data.Data
{
    public class DBContext : IDisposable
    {
        private readonly Database Db;

        public DBContext(string connectionString)
        {
            Db = new Database(connectionString, "System.Data.SqlClient");
            Db.OpenSharedConnection();
        }

        public List<T> Query<T>(string sql, params object[] args)
        {
            return Db.Fetch<T>(sql, args);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
