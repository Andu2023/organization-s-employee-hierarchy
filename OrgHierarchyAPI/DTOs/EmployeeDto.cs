namespace OrgHierarchyAPI.DTOs
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Guid PositionId { get; set; }
        public string PositionTitle { get; set; } 
    }
}
