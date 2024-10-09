using OrgHierarchyAPI.Models;

namespace OrgHierarchyAPI.Repository
{
    public interface IPositionRepository
    {
        Task<IEnumerable<Position>> GetAllPositionsAsync();
        Task<Position> GetPositionByIdAsync(Guid id);
        Task<Position> AddPositionAsync(Position position);
        Task<Position> UpdatePositionAsync(Position position);
        Task DeletePositionAsync(Guid id);
        Task<IEnumerable<Position>> GetPositionTreeAsync();
        Task<IEnumerable<Position>> GetChildrenOfPositionAsync(Guid id);
    }
}
