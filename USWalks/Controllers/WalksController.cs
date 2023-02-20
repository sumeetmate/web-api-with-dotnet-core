using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using USWalks.Models.Domain;
using USWalks.Models.DTO;
using USWalks.Repositories;

namespace USWalks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository repository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        { 
            var walks = await repository.GetAllAsync();
            var dtos= mapper.Map<List<Models.DTO.Walk>>(walks);

            return Ok(dtos);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetAsync")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var walk = await repository.GetAsync(id);

            if (walk == null) return NotFound();
            
            var dto = mapper.Map<Models.DTO.Walk>(walk);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddWalkRequest request)
        {
            var walk = new Models.Domain.Walk
            { 
                Name= request.Name,
                Length=request.Length,
                RegionId=request.RegionId,
                WalkDifficultyId=request.WalkDifficultyId,
            };

           walk = await repository.AddAsync(walk);

            var dto = new Models.DTO.Walk 
            { 
                ID = walk.ID,
                Name = walk.Name,
                Length = walk.Length,
                RegionId=walk.RegionId,
                WalkDifficultyId=walk.WalkDifficultyId,
            };

            return CreatedAtAction("GetAsync", new { id = dto.ID }, dto); 
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id,[FromBody] AddWalkRequest request)
        {
            var walk = new Models.Domain.Walk
            {
                Name = request.Name,
                Length = request.Length,
                RegionId = request.RegionId,
                WalkDifficultyId = request.WalkDifficultyId,
            };

            walk = await repository.UpdateAsync(id,walk);

            if (walk == null)
                return NotFound("walk not found");

            var dto = new Models.DTO.Walk
            {
                ID = walk.ID,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId,
            };

            return Ok(dto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var walk = await repository.DeleteAsync(id);
            if (walk == null)
                return NotFound();

           var dto = mapper.Map<Models.DTO.Walk>(walk);

            return Ok(dto);
        }

    }
}
