using AutoMapper;
using Serilog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrgHierarchyAPI.DTOs;
using OrgHierarchyAPI.Models;
using OrgHierarchyAPI.Services;
using OrgHierarchyAPI.Repository;

namespace OrgHierarchyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionService _positionService;
        private readonly IMapper _mapper;
        private readonly ILogger<PositionRepository> _logger;

        public PositionsController(IPositionService positionService, IMapper mapper,ILogger<PositionRepository> _logger)
        {
            _positionService = positionService;
            _mapper = mapper;
            this._logger = _logger;
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
                _logger.LogWarning("Position with ID: {Id} not found", id);
                return BadRequest("postion is not found.");
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
                _logger.LogWarning("Position with ID: {Id} not found", id);
                return BadRequest("Cannot delete a Null  position that has child positions. Please remove or reassign the children first.");
            }

            // Check if the position has any children
            if (position.Children != null && position.Children.Any())
            {
                return BadRequest("Cannot delete a position that has child positions. Please remove or reassign the children first.");
            }

            if (position.Children != null && position.Children.Any())
            {
                _logger.LogWarning("Attempt to delete Position ID: {Id} that has child positions. Deletion aborted.", id);
                return BadRequest("Cannot delete a position that has child positions. Please remove or reassign the children first.");
            }
            // If there are no children, proceed with deletion
            try
            {
                // If there are no children, proceed with deletion
                await _positionService.DeletePositionAsync(id);
                _logger.LogInformation("Position with ID: {Id} successfully deleted", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting Position ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the position.");
            }
        }
    }
}
