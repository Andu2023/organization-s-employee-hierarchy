using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrgHierarchyAPI.DTOs;
using OrgHierarchyAPI.Models;
using OrgHierarchyAPI.Services;

namespace OrgHierarchyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionService _positionService;
        private readonly IMapper _mapper;

        public PositionsController(IPositionService positionService, IMapper mapper)
        {
            _positionService = positionService;
            _mapper = mapper;
        }

        // GET: api/positions
        [HttpGet]
        public async Task<IActionResult> GetAllPositions()
        {
            var positions = await _positionService.GetAllPositionsAsync();
            var positionDtos = _mapper.Map<IEnumerable<PositionDto>>(positions);
            return Ok(positionDtos);
        }

        // GET: api/positions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPositionById(Guid id)
        {
            var position = await _positionService.GetPositionByIdAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            var positionDto = _mapper.Map<PositionDto>(position);
            return Ok(positionDto);
        }
        // GET: api/positions/{id}/children
        [HttpGet("{id}/children")]
        public async Task<IActionResult> GetChildrenOfPosition(Guid id)
        {
            var children = await _positionService.GetChildrenOfPositionAsync(id);
            if (children == null || !children.Any())
            {
                return NotFound("No children found for this position.");
            }

            var childrenDtos = _mapper.Map<IEnumerable<PositionDto>>(children);
            return Ok(childrenDtos);
        }

        // GET: api/positions/tree
        [HttpGet("tree")]
        public async Task<IActionResult> GetPositionTree()
        {
            var positionTree = await _positionService.GetPositionTreeAsync();
            var positionTreeDtos = _mapper.Map<IEnumerable<PositionTreeDto>>(positionTree);
            return Ok(positionTreeDtos);
        }

        // POST: api/positions
        [HttpPost]
        public async Task<IActionResult> CreatePosition(PositionCreateUpdateDto positionDto)
        {
            var position = _mapper.Map<Position>(positionDto);
            var createdPosition = await _positionService.AddPositionAsync(position);
            var createdPositionDto = _mapper.Map<PositionDto>(createdPosition);

            return CreatedAtAction(nameof(GetPositionById), new { id = createdPosition.Id }, createdPositionDto);
        }

        // PUT: api/positions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePosition(Guid id, PositionCreateUpdateDto positionDto)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID");
            }

            var existingPosition = await _positionService.GetPositionByIdAsync(id);
            if (existingPosition == null)
            {
                return NotFound();
            }

            _mapper.Map(positionDto, existingPosition);
            var updatedPosition = await _positionService.UpdatePositionAsync(existingPosition);

            var updatedPositionDto = _mapper.Map<PositionDto>(updatedPosition);
            return Ok(updatedPositionDto);
        }

        // DELETE: api/positions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(Guid id)
        {

            // Fetch the position by ID
            var position = await _positionService.GetPositionByIdAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            // Check if the position has any children
            if (position.Children != null && position.Children.Any())
            {
                return BadRequest("Cannot delete a position that has child positions. Please remove or reassign the children first.");
            }

            // If there are no children, proceed with deletion
            await _positionService.DeletePositionAsync(id);
            return NoContent();
        }
    }
}
