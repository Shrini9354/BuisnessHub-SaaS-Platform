using BusinessHub.Domain.Entities;
using BusinessHub.Domain.Interfaces;
using BusinessHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BusinessHub.Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly AppDbContext _context;

        public TenantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tenant> GetByIdAsync(int id)
        {
            return await _context.Tenants.FindAsync(id);
        }

        public async Task<Tenant> GetByEmailAsync(string email)
        {
            return await _context.Tenants
                .FirstOrDefaultAsync(t => t.Email == email);
        }

        public async Task<Tenant> CreateAsync(Tenant tenant)
        {
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();
            return tenant;
        }

        public async Task<List<Tenant>> GetAllAsync()
        {
            return await _context.Tenants.ToListAsync();
        }
    }
}