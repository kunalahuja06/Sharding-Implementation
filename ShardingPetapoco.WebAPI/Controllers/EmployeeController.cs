using Microsoft.AspNetCore.Mvc;
using ShardingPetapoco.Data.Entities;
using ShardingPetatpoco.Services.Contracts;

namespace ShardingPetapoco.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public List<Employee> GetEmployees()
        {
            return _employeeService.GetEmployees();
        }

        [HttpPost]
        public int AddEmployee(Employee employee)
        {
            return _employeeService.AddEmployee(employee);
        }
    }
}
