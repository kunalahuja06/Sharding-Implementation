using PetaPoco;
using ShardingPetapoco.Data.Entities;

namespace ShardingPetapoco.Data.Data
{
    public class MasterDbContext : IDisposable
    {
        public readonly Database Db;

        public MasterDbContext(Database db)
        {
            Db = db;
        }

        public Servers GetServerbyTenantId(int tenantId)
        {
            var tenantMapping = Db.SingleOrDefault<TenantMapping>("WHERE TenantId = @0", tenantId);
            if (tenantMapping == null)
                throw new Exception("Tenant not found");

            return Db.SingleOrDefault<Servers>("WHERE Id = @0", tenantMapping.ServerId);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
