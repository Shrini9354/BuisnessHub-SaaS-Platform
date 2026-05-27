using BusinessHub.Domain.Entities;

namespace BusinessHub.Domain.Interfaces
{
    public interface ITenantRepository
    {
        Task<Tenant> GetByIdAsync(int id);
        Task<Tenant> GetByEmailAsync(string email);
        Task<Tenant> CreateAsync(Tenant tenant);
        Task<List<Tenant>> GetAllAsync();
    }
}