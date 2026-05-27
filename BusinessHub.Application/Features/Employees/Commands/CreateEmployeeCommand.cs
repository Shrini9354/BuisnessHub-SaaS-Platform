using BusinessHub.Domain.Entities;
using BusinessHub.Domain.Interfaces;
using MediatR;

namespace BusinessHub.Application.Features.Employees.Commands
{
    public class CreateEmployeeCommand : IRequest<Employee>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public int TenantId { get; set; }
    }

    public class CreateEmployeeCommandHandler
        : IRequestHandler<CreateEmployeeCommand, Employee>
    {
        private readonly IEmployeeRepository _repository;

        public CreateEmployeeCommandHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Employee> Handle(
            CreateEmployeeCommand request,
            CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                Name = request.Name,
                Email = request.Email,
                Position = request.Position,
                Salary = request.Salary,
                TenantId = request.TenantId
            };

            return await _repository.CreateAsync(employee);
        }
    }
}