namespace OrgHierarchyAPI.DTOs
{
    public class PositionTreeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PositionTreeDto> Children { get; set; } = new List<PositionTreeDto>();
    }
}
