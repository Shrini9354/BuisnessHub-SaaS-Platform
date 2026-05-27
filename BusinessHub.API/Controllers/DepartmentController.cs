using BusinessHub.Application.Features.Departments.Commands;
using BusinessHub.Application.Features.Departments.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusinessHub.API.Controllers
{
    [Route("api/departments")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private int GetTenantId()
        {
            return int.Parse(User.FindFirst("TenantId")!.Value);
        }

        // GET: api/departments
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tenantId = GetTenantId();
            var departments = await _mediator.Send(
                new GetDepartmentsQuery { TenantId = tenantId });
            return Ok(departments);
        }

        // POST: api/departments
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentCommand command)
        {
            command.TenantId = GetTenantId();
            var department = await _mediator.Send(command);
            return Ok(department);
        }
    }
}