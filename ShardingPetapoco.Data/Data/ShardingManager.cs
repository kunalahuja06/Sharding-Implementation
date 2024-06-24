using System.Data.SqlClient;
using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;
using Microsoft.Extensions.Configuration;

namespace ShardingPetapoco.Data.Data
{
    public class ShardingManager
    {
        private readonly IConfiguration _configuration;
        private static string _masterDbConnectionString;
        private static string _shardConnectionString;

        private static ShardMapManager shardMapManager;

        public ShardingManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _masterDbConnectionString = _configuration["ConnectionStrings:MasterDB"];
            _shardConnectionString = _configuration["ConnectionStrings:ShardDB"];
        }

        private static ShardMapManager GetShardMapManager()
        {
            if (shardMapManager == null)
            {
                ShardMapManager manager;
                ShardMapManagerFactory.TryGetSqlShardMapManager(_masterDbConnectionString, ShardMapManagerLoadPolicy.Lazy, out manager);
                shardMapManager = manager ?? ShardMapManagerFactory.CreateSqlShardMapManager(_masterDbConnectionString);
            }

            return shardMapManager;
        }

        private static ListShardMap<int> GetTenantShardMap(string shardMapName)
        {
            var shardMapManager = GetShardMapManager();
            if (!shardMapManager.TryGetListShardMap(shardMapName, out ListShardMap<int> shardMap))
            {
                shardMap = shardMapManager.CreateListShardMap<int>(shardMapName);
            }

            return shardMap;
        }

        private static void AddShard(string shardMapName, string shardLocation, int tenantId)
        {
            var shardMap = GetTenantShardMap(shardMapName);
            var shard = shardMap.CreateShard(new ShardLocation(shardLocation, "ShardedDB3"));
            shardMap.CreatePointMapping(tenantId, shard);
        }

        public static string GetShardConnectionString(string shardMapName, int tenantId)
        {
            var shardMap = GetTenantShardMap(shardMapName);
            var mapping = shardMap.GetMappingForKey(tenantId);
            return BuildConnectionString(mapping.Shard.Location.DataSource, mapping.Shard.Location.Database);

            //var sqlConnectionStringBuilder = new SqlConnectionStringBuilder
            //{
            //    DataSource = mapping.Shard.Location.DataSource,
            //    InitialCatalog = mapping.Shard.Location.Database,
            //    IntegratedSecurity = true,
            //    MaxPoolSize = 200,
            //    TrustServerCertificate = true
            //};
            //return sqlConnectionStringBuilder.ConnectionString;
        }

        private static string BuildConnectionString(string source, string initialCatalog)
        {
            string connectionString = string.Format(_shardConnectionString, source, initialCatalog);
            return connectionString;
        }
    }
}
