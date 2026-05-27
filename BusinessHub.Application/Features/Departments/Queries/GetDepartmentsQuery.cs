using BusinessHub.Domain.Entities;
using BusinessHub.Domain.Interfaces;
using MediatR;

namespace BusinessHub.Application.Features.Departments.Queries
{
    public class GetDepartmentsQuery : IRequest<List<Department>>
    {
        public int TenantId { get; set; }
    }

    public class GetDepartmentsQueryHandler
        : IRequestHandler<GetDepartmentsQuery, List<Department>>
    {
        private readonly IDepartmentRepository _repository;

        public GetDepartmentsQueryHandler(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Department>> Handle(
            GetDepartmentsQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository.GetAllByTenantAsync(request.TenantId);
        }
    }
}