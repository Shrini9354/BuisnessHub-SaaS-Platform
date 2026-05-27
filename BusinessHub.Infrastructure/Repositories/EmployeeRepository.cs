using BusinessHub.Domain.Entities;
using BusinessHub.Domain.Interfaces;
using BusinessHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BusinessHub.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllByTenantAsync(int tenantId)
        {
            return await _context.Employees
                .Where(e => e.TenantId == tenantId)
                .ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id, int tenantId)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id && e.TenantId == tenantId);
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteAsync(int id, int tenantId)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id && e.TenantId == tenantId);
            if (employee == null) return false;
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}