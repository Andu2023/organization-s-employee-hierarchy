using System.ComponentModel.DataAnnotations;

namespace OrgHierarchyAPI.DTOs
{
    public class PositionCreateUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public Guid? ParentId { get; set; }
    }
}
