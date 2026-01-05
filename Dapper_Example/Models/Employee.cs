namespace DapperExample.Models
{
    public class Employee : Entity
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Position { get; set; } = string.Empty;
        public int CompanyId { get; set; }
    }
}
