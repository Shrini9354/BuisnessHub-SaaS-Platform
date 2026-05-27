using BusinessHub.Domain.Entities;
using BusinessHub.Domain.Interfaces;
using MediatR;

namespace BusinessHub.Application.Features.Employees.Queries
{
    public class GetEmployeesByTenantQuery : IRequest<List<Employee>>
    {
        public int TenantId { get; set; }
    }

    public class GetEmployeesByTenantQueryHandler
        : IRequestHandler<GetEmployeesByTenantQuery, List<Employee>>
    {
        private readonly IEmployeeRepository _repository;

        public GetEmployeesByTenantQueryHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Employee>> Handle(
            GetEmployeesByTenantQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository.GetAllByTenantAsync(request.TenantId);
        }
    }
}