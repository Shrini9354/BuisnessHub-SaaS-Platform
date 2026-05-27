using BusinessHub.Domain.Entities;
using BusinessHub.Domain.Interfaces;
using MediatR;

namespace BusinessHub.Application.Features.Tenants.Queries
{
    public class GetAllTenantsQuery : IRequest<List<Tenant>>
    {
    }

    public class GetAllTenantsQueryHandler
        : IRequestHandler<GetAllTenantsQuery, List<Tenant>>
    {
        private readonly ITenantRepository _repository;

        public GetAllTenantsQueryHandler(ITenantRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Tenant>> Handle(
            GetAllTenantsQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}