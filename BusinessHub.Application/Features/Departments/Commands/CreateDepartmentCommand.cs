using BusinessHub.Domain.Entities;
using BusinessHub.Domain.Interfaces;
using MediatR;

namespace BusinessHub.Application.Features.Departments.Commands
{
    public class CreateDepartmentCommand : IRequest<Department>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TenantId { get; set; }
    }

    public class CreateDepartmentCommandHandler
        : IRequestHandler<CreateDepartmentCommand, Department>
    {
        private readonly IDepartmentRepository _repository;

        public CreateDepartmentCommandHandler(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Department> Handle(
            CreateDepartmentCommand request,
            CancellationToken cancellationToken)
        {
            var department = new Department
            {
                Name = request.Name,
                Description = request.Description,
                TenantId = request.TenantId
            };

            return await _repository.CreateAsync(department);
        }
    }
}