using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrgHierarchyAPI.Models
{
    public class Position
    {
        public Guid Id { get; set; }           
        public string Name { get; set; }    
        public string Description { get; set; } 
        public Guid? ParentId { get; set; }     

        // Navigation property for the parent position
        public Position Parent { get; set; }    // Reference to the parent position

        // Navigation property for child positions
        public ICollection<Position> Children { get; set; } = new List<Position>();
        // Navigation properties for relationships
        public ICollection<Employee> Employees { get; set; }
    }
}
