using ShardingPetapoco.Data.Entities;

namespace ShardingPetatpoco.Services.Contracts
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployees(int tenantId);
    }
}
