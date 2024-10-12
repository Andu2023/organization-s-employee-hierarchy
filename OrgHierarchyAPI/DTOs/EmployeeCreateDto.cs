namespace OrgHierarchyAPI.DTOs
{
    public class EmployeeCreateDto
    {
        public string FirstName { get; set; }  // Employee's first name
        public string MiddleName { get; set; } // Employee's middle name
        public string LastName { get; set; }   // Employee's last name
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }    // Employee's address
        public Guid PositionId { get; set; }   // Foreign key for Position
    }
}
