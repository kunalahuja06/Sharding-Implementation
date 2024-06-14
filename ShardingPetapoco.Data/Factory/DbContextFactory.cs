using ShardingPetapoco.Data.Data;

namespace ShardingPetapoco.Data.Factory
{
    public class DbContextFactory
    {
        private readonly MasterDbContext _masterDbContext;

        public DbContextFactory(MasterDbContext masterDbContext)
        {
            _masterDbContext = masterDbContext;
        }

        public DBContext CreateDbContext(int tenantId)
        {
            string connectionString = _masterDbContext.GetServerbyTenantId(tenantId).ConnectionString;
            return new DBContext(connectionString);
        }
    }
}
