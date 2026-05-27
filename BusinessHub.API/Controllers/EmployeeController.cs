using BusinessHub.Application.Features.Employees.Commands;
using BusinessHub.Application.Features.Employees.Queries;
using BusinessHub.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusinessHub.API.Controllers
{
    [Route("api/employees")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITenantRepository _tenantRepository;

        public EmployeeController(
            IMediator mediator,
            IEmployeeRepository employeeRepository,
            ITenantRepository tenantRepository)
        {
            _mediator = mediator;
            _employeeRepository = employeeRepository;
            _tenantRepository = tenantRepository;
        }

        private int GetTenantId()
        {
            return int.Parse(User.FindFirst("TenantId")!.Value);
        }

        // GET: api/employees
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tenantId = GetTenantId();
            var employees = await _mediator.Send(
                new GetEmployeesByTenantQuery { TenantId = tenantId });
            return Ok(employees);
        }

        // POST: api/employees
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeCommand command)
        {
            var tenantId = GetTenantId();

            // ✅ Get tenant plan
            var tenant = await _tenantRepository.GetByIdAsync(tenantId);

            // ✅ Get current employee count
            var employees = await _employeeRepository
                .GetAllByTenantAsync(tenantId);
            var employeeCount = employees.Count;

            // ✅ Check plan limits
            if (tenant.Plan == "Free" && employeeCount >= 5)
                return BadRequest(new
                {
                    message = "❌ Free plan limit reached! Maximum 5 employees allowed.",
                    currentCount = employeeCount,
                    limit = 5,
                    upgrade = "Upgrade to Pro plan for 50 employees!"
                });

            if (tenant.Plan == "Pro" && employeeCount >= 50)
                return BadRequest(new
                {
                    message = "❌ Pro plan limit reached! Maximum 50 employees allowed.",
                    currentCount = employeeCount,
                    limit = 50,
                    upgrade = "Upgrade to Enterprise plan for unlimited employees!"
                });

            // ✅ Enterprise has no limit
            command.TenantId = tenantId;
            var employee = await _mediator.Send(command);
            return Ok(employee);
        }
    }
}