using BusinessHub.Domain.Entities;

namespace BusinessHub.Domain.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllByTenantAsync(int tenantId);
        Task<Department> GetByIdAsync(int id, int tenantId);
        Task<Department> CreateAsync(Department department);
        Task<bool> DeleteAsync(int id, int tenantId);
    }
}