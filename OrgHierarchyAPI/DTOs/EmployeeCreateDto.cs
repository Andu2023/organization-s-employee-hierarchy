namespace OrgHierarchyAPI.DTOs
{
    public class EmployeeCreateDto
    {
        public string FirstName { get; set; } 
        public string MiddleName { get; set; } 
        public string LastName { get; set; }  
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }    
        public Guid PositionId { get; set; }   
    }
}
