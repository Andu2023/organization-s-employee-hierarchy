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
        public Task<IEnumerable<Position>> GetChildrenOfPositionAsync(Guid id)
        {
            return _repository.GetChildrenOfPositionAsync(id);
        }

        public async Task<Position> AddPositionAsync(Position position)
        {
            // Check if the position name already exists
            if (await _repository.PositionNameExistsAsync(position.Name))
            {
                throw new ArgumentException($"A position with the name '{position.Name}' already exists.");
            }
            // Check if the position being created is a root position (ParentId is null)
            if (position.ParentId == null)
            {
                // Fetch any existing root position
                var existingRootPosition = await _repository.GetRootPositionAsync();
                if (existingRootPosition != null)
                {
                    // Throw an exception or return an error if a root position already exists
                    throw new InvalidOperationException("A root position already exists. New positions must have a parent.");
                }
            }

            return await _repository.AddPositionAsync(position);
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
