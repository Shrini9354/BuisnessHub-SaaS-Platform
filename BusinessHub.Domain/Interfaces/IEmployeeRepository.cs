using BusinessHub.Domain.Entities;

namespace BusinessHub.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllByTenantAsync(int tenantId);
        Task<Employee> GetByIdAsync(int id, int tenantId);
        Task<Employee> CreateAsync(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id, int tenantId);
    }
}