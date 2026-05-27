using BusinessHub.Application.Features.Tenants.Commands;
using BusinessHub.Application.Features.Tenants.Queries;
using BusinessHub.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessHub.API.Controllers
{
    [Route("api/tenants")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        private readonly ITenantRepository _tenantRepository;

        public TenantController(IMediator mediator,
            IConfiguration config,
            ITenantRepository tenantRepository)
        {
            _mediator = mediator;
            _config = config;
            _tenantRepository = tenantRepository;
        }

        // POST: api/tenants/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterTenantCommand command)
        {
            var existingTenant = await _tenantRepository
                .GetByEmailAsync(command.Email);

            if (existingTenant != null)
                return BadRequest("Email already exists!");

            var tenant = await _mediator.Send(command);
            return Ok(new
            {
                message = "Company registered successfully!",
                tenantId = tenant.Id,
                company = tenant.CompanyName,
                plan = tenant.Plan
            });
        }

        // POST: api/tenants/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var tenant = await _tenantRepository
                .GetByEmailAsync(request.Email);

            if (tenant == null || tenant.Password != request.Password)
                return Unauthorized("Invalid email or password!");

            // Generate JWT Token with TenantId
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var credentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("TenantId", tenant.Id.ToString()),
                new Claim("CompanyName", tenant.CompanyName),
                new Claim("Plan", tenant.Plan),
                new Claim(ClaimTypes.Email, tenant.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                tenantId = tenant.Id,
                companyName = tenant.CompanyName,
                plan = tenant.Plan
            });
        }

        // GET: api/tenants (Super Admin only)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tenants = await _mediator.Send(new GetAllTenantsQuery());
            return Ok(tenants);
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}