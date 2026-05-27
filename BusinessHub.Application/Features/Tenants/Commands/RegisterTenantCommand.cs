using BusinessHub.Domain.Entities;
using BusinessHub.Domain.Interfaces;
using MediatR;

namespace BusinessHub.Application.Features.Tenants.Commands
{
    public class RegisterTenantCommand : IRequest<Tenant>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string Plan { get; set; } = "Free";
    }

    public class RegisterTenantCommandHandler
        : IRequestHandler<RegisterTenantCommand, Tenant>
    {
        private readonly ITenantRepository _repository;

        public RegisterTenantCommandHandler(ITenantRepository repository)
        {
            _repository = repository;
        }

        public async Task<Tenant> Handle(
            RegisterTenantCommand request,
            CancellationToken cancellationToken)
        {
            var tenant = new Tenant
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                CompanyName = request.CompanyName,
                Plan = request.Plan
            };

            return await _repository.CreateAsync(tenant);
        }
    }
}
