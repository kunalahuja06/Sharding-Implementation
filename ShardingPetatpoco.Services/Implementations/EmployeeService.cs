using ShardingPetapoco.Data.Data;
using ShardingPetapoco.Data.Entities;
using ShardingPetapoco.Data.Factory;
using ShardingPetatpoco.Services.Contracts;

namespace ShardingPetatpoco.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DBContext _dbContext;
        private readonly RequestContext _requestContext;

        public EmployeeService(DBContext dbContext, RequestContext requestContext)
        {
            _dbContext = dbContext;
            _requestContext = requestContext;
        }

        public List<Employee> GetEmployees()
        {
            return _dbContext.Query<Employee>("SELECT * FROM Employee WHERE TenantId = @0", _requestContext.TenantId);
        }

        public int AddEmployee(Employee employee)
        {
            return _dbContext.Execute("INSERT INTO Employee (Id, TenantId, Name) VALUES (@0, @1, @2)", employee.Id, _requestContext.TenantId, employee.Name);
        }
    }
}
