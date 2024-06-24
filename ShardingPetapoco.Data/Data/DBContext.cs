using Microsoft.Extensions.Logging;
using PetaPoco;

namespace ShardingPetapoco.Data.Data
{
    public class DBContext
    {
        private readonly Database Db;

        private readonly ILogger<Database> Logger;

        public DBContext(string connectionString, ILogger<Database> logger)
        {
            Logger = logger;
            Db = new Database(connectionString, "System.Data.SqlClient");
            Db.CommandExecuting += (sender, args) =>
            {
                Logger.LogInformation($"Executing command: {args.Command.CommandText}");
            };
            Db.OpenSharedConnection();
        }

        public List<T> Query<T>(string sql, params object[] args)
        {
            IEnumerable<T> result = Db.Query<T>(sql, args);
            return result.ToList();
        }

        public int Execute(string sql, params object[] args)
        {
            var result = Db.Execute(sql, args);
            return result;
        }
    }
}
