using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using USWalks.Models.DTO;
using USWalks.Repositories;

namespace USWalks.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class RegionsController : Controller
    {
        private readonly IRegionsRepository _regionsRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionsRepository regionsRepository, IMapper mapper)
        {
            _regionsRepository = regionsRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var regions = await _regionsRepository.GetAllAsync();
            var dtos = _mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(dtos);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionById")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await _regionsRepository.GetAsync(id);

            if (region == null)
                return NotFound();
            var dto = _mapper.Map<Models.DTO.Region>(region);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest request)
        {
            var region = new Models.Domain.Region
            {
                Code = request.Code,
                Name = request.Name,
                Lat = request.Lat,
                Long = request.Long,
                Populations = request.Populations,
                Area = request.Area,
            };
            await _regionsRepository.AddAsync(region);

            var dto = new Models.DTO.Region
            {
                ID = region.ID,
                Code = region.Code,
                Name = region.Name,
                Populations = region.Populations,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
            };

            return CreatedAtAction("GetRegionById", new { id = dto.ID }, dto);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
           var region =  await _regionsRepository.DeleteAsync(id);
            if (region == null)
                return NotFound();
            var dto = new Models.DTO.Region
            {
                ID = region.ID,
                Code = region.Code,
                Name = region.Name,
                Populations = region.Populations,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
            };
            return Ok(dto);
             
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id,[FromBody] AddRegionRequest request)
        {
            //convert to domain model
            var region = new Models.Domain.Region
            {
                Code = request.Code,
                Name = request.Name,
                Lat = request.Lat,
                Long = request.Long,
                Populations = request.Populations,
                Area = request.Area,
            };

            //update region using repository
            region = await _regionsRepository.UpdateAsync(id, region);

            //if not found then return null
            if (region == null) return NotFound();

            //convert back to dto
            var dto = new Models.DTO.Region
            {
                ID = region.ID,
                Code = region.Code,
                Name = region.Name,
                Populations = region.Populations,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
            };
            return Ok(dto);
        }
    }
}
