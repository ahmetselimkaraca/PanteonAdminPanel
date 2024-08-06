using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PanteonAdminPanel.API.DTO.ConfigurationDTO;
using PanteonAdminPanel.API.DTO;
using PanteonAdminPanel.API.Models;
using PanteonAdminPanel.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PanteonAdminPanel.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IBuildingTypesRepository _buildingTypesRepository;
        private readonly IMapper _mapper;

        public ConfigurationController(
            IConfigurationRepository configurationRepository,
            IBuildingTypesRepository buildingTypesRepository,
            IMapper mapper)
        {
            _configurationRepository = configurationRepository;
            _buildingTypesRepository = buildingTypesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var configurations = await _configurationRepository.GetAllConfigurationsAsync();
            var configurationsDto = _mapper.Map<List<ConfigurationDto>>(configurations);
            return Ok(configurationsDto);
        }

        [HttpGet("{buildingType}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByBuildingType([FromRoute] string buildingType)
        {
            var configuration = await _configurationRepository.GetConfigurationAsync(buildingType);
            if (configuration == null)
            {
                return NotFound();
            }
            var configurationDto = _mapper.Map<ConfigurationDto>(configuration);
            return Ok(configurationDto);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateConfigurationDto createConfigurationDto)
        {
            if (!await _buildingTypesRepository.IsBuildingTypeValidAsync(createConfigurationDto.BuildingType))
            {
                return BadRequest("Invalid building type.");
            }

            if (createConfigurationDto.BuildingCost <= 0)
            {
                return BadRequest("Building cost must be greater than zero.");
            }

            if (createConfigurationDto.ConstructionTime < 30 || createConfigurationDto.ConstructionTime > 1800)
            {
                return BadRequest("Construction time must be between 30 and 1800 seconds.");
            }

            var configuration = _mapper.Map<Configuration>(createConfigurationDto);
            await _configurationRepository.AddConfigurationAsync(configuration);

            var configurationDto = _mapper.Map<ConfigurationDto>(configuration);
            return CreatedAtAction(nameof(GetByBuildingType), new { buildingType = configurationDto.BuildingType }, configurationDto);
        }

        [Authorize]
        [HttpPut("{buildingType}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] string buildingType, [FromBody] UpdateConfigurationDto updateConfigurationDto)
        {
            var existingConfiguration = await _configurationRepository.GetConfigurationAsync(buildingType);
            if (existingConfiguration == null)
            {
                return NotFound();
            }

            if (updateConfigurationDto.BuildingCost <= 0)
            {
                return BadRequest("Building cost must be greater than zero.");
            }

            if (updateConfigurationDto.ConstructionTime is < 30 or > 1800)
            {
                return BadRequest("Construction time must be between 30 and 1800 seconds.");
            }

            _mapper.Map(updateConfigurationDto, existingConfiguration);
            await _configurationRepository.UpdateConfigurationAsync(existingConfiguration);

            var configurationDto = _mapper.Map<ConfigurationDto>(existingConfiguration);
            return Ok(configurationDto);
        }

        [Authorize]
        [HttpDelete("{buildingType}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] string buildingType)
        {
            var existingConfiguration = await _configurationRepository.GetConfigurationAsync(buildingType);
            if (existingConfiguration == null)
            {
                return NotFound();
            }

            await _configurationRepository.DeleteConfigurationAsync(buildingType);
            var configurationDto = _mapper.Map<ConfigurationDto>(existingConfiguration);
            return Ok(configurationDto);
        }
    }
}
