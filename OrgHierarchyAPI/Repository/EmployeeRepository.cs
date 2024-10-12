using Microsoft.EntityFrameworkCore;
using OrgHierarchyAPI.Models;

namespace OrgHierarchyAPI.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HierarchyContext _context;

        public EmployeeRepository(HierarchyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            // Fetch all employees and include their position data
            return await _context.Employees
                .Include(e => e.Position) // Include the Position data
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByPositionIdAsync(Guid positionId)
        {
            return await _context.Employees
                .Include(e => e.Position) // Include the position relationship
                .Where(e => e.PositionId == positionId)
                .ToListAsync();
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }

}


