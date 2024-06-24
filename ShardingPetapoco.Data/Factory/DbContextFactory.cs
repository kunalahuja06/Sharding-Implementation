using Microsoft.Extensions.Logging;
using PetaPoco;
using ShardingPetapoco.Data.Data;

namespace ShardingPetapoco.Data.Factory
{
    public class DbContextFactory
    {
        private readonly string _shardMapName;

        private readonly ILoggerFactory _loggerFactory;

        public DbContextFactory(string shardMapName, ILoggerFactory loggerFactory)
        {
            _shardMapName = shardMapName;
            _loggerFactory = loggerFactory;
        }

        public DBContext CreateDbContext(int tenantId)
        {
            string connectionString = ShardingManager.GetShardConnectionString(_shardMapName, tenantId);
            var logger = _loggerFactory.CreateLogger<Database>();
            return new DBContext(connectionString, logger);
        }
    }
}
