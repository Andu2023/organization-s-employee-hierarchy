using Microsoft.EntityFrameworkCore;
using OrgHierarchyAPI.Models;
using System;

namespace OrgHierarchyAPI.Repository
{
    public class PositionRepository: IPositionRepository
    {

        private readonly HierarchyContext _context;

        public PositionRepository(HierarchyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Position>> GetAllPositionsAsync()
        {
             // Fetch all positions, including children 
      return await _context.Positions
        .Include(p => p.Children) 
        .ToListAsync();
        }

        public async Task<Position> GetPositionByIdAsync(Guid id)
        {
            return await _context.Positions
                .Include(p => p.Children)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<Position>> GetChildrenOfPositionAsync(Guid id)
        {
            return await _context.Positions
                .Where(p => p.ParentId == id) // Fetch positions where ParentId matches the given id
                .ToListAsync();
        }

        public async Task<Position> AddPositionAsync(Position position)
        {
            _context.Positions.Add(position);
            await _context.SaveChangesAsync();
            return position;
        }

        public async Task<Position> UpdatePositionAsync(Position position)
        {
            _context.Entry(position).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return position;
        }

        public async Task DeletePositionAsync(Guid id)
        {
            var position = await _context.Positions.FindAsync(id);
            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Position>> GetPositionTreeAsync()
        {
            var allPositions = await _context.Positions.ToListAsync();
            return BuildTree(allPositions);
        }
        public async Task<bool> PositionNameExistsAsync(string name)
        {
            return await _context.Positions.AnyAsync(p => p.Name == name);  // Check for duplicate name
        }
        // New method to get the root position (ParentId is null)
        public async Task<Position> GetRootPositionAsync()
        {
            return await _context.Positions.FirstOrDefaultAsync(p => p.ParentId == null);
        }

        private List<Position> BuildTree(IEnumerable<Position> allPositions)
        {
            var positionLookup = allPositions.ToLookup(p => p.ParentId);
            var rootPositions = positionLookup[null].ToList();

            foreach (var root in rootPositions)
            {
                AssignChildren(root, positionLookup);
            }

            return rootPositions;
        }

        private void AssignChildren(Position position, ILookup<Guid?, Position> positionLookup)
        {
            position.Children = positionLookup[position.Id].ToList();
            foreach (var child in position.Children)
            {
                AssignChildren(child, positionLookup);
            }
        }
    }
}
