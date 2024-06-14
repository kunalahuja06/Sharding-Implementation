using ShardingPetapoco.Data.Entities;
using ShardingPetapoco.Data.Factory;
using ShardingPetatpoco.Services.Contracts;

namespace ShardingPetatpoco.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DbContextFactory _dbContextFactory;

        public EmployeeService(DbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public List<Employee> GetEmployees(int tenantId)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(tenantId))
            {
                return dbContext.Query<Employee>("SELECT * FROM Employee WHERE TenantId = @0", tenantId);
            }
        }
    }
}
