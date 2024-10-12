using OrgHierarchyAPI.Models;

namespace OrgHierarchyAPI.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<IEnumerable<Employee>> GetEmployeesByPositionIdAsync(Guid positionId);
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Guid id);
    }
}
