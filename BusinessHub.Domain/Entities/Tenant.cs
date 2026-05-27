namespace BusinessHub.Domain.Entities
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string Plan { get; set; } = "Free";
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<Employee> Employees { get; set; } = new();
    }
}