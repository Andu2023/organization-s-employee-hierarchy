using OrgHierarchyAPI.Models;
using OrgHierarchyAPI.Repository;

namespace OrgHierarchyAPI.Services
{
    public class PositionService: IPositionService
    {
        private readonly IPositionRepository _repository;

        public PositionService(IPositionRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Position>> GetAllPositionsAsync()
        {
            return _repository.GetAllPositionsAsync();
        }

        public Task<Position> GetPositionByIdAsync(Guid id)
        {
            return _repository.GetPositionByIdAsync(id);
        }

        public Task<Position> AddPositionAsync(Position position)
        {
            return _repository.AddPositionAsync(position);
        }

        public Task<Position> UpdatePositionAsync(Position position)
        {
            return _repository.UpdatePositionAsync(position);
        }

        public Task DeletePositionAsync(Guid id)
        {
            return _repository.DeletePositionAsync(id);
        }

        public Task<IEnumerable<Position>> GetPositionTreeAsync()
        {
            return _repository.GetPositionTreeAsync();
        }
    }
}
