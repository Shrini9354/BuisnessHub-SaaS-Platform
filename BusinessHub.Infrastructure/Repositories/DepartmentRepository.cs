using BusinessHub.Domain.Entities;
using BusinessHub.Domain.Interfaces;
using BusinessHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BusinessHub.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetAllByTenantAsync(int tenantId)
        {
            return await _context.Departments
                .Where(d => d.TenantId == tenantId)
                .Include(d => d.Employees)
                .ToListAsync();
        }

        public async Task<Department> GetByIdAsync(int id, int tenantId)
        {
            return await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == id && d.TenantId == tenantId);
        }

        public async Task<Department> CreateAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<bool> DeleteAsync(int id, int tenantId)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == id && d.TenantId == tenantId);
            if (department == null) return false;
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}