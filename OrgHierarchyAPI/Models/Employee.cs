namespace OrgHierarchyAPI.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        // Foreign key reference to Position
        public Guid PositionId { get; set; }
        public Position Position { get; set; }

    }
}
