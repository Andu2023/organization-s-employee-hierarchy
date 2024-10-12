using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrgHierarchyAPI.Models
{
    public class Position
    {
        public Guid Id { get; set; }           // Unique identifier for the position
        public string Name { get; set; }        // Position name (e.g., "CEO")
        public string Description { get; set; } // Description of the position
        public Guid? ParentId { get; set; }     // Nullable ParentId (for positions like CEO with no parent)

        // Navigation property for the parent position
        public Position Parent { get; set; }    // Reference to the parent position

        // Navigation property for child positions
        public ICollection<Position> Children { get; set; } = new List<Position>();
        // Navigation properties for relationships
        public ICollection<Employee> Employees { get; set; }
    }
}
