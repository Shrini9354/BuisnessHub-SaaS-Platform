namespace BusinessHub.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.Now;
        public int TenantId { get; set; }
        public Tenant? Tenant { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}