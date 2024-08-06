using Microsoft.AspNetCore.Mvc;
using PanteonAdminPanel.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PanteonAdminPanel.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BuildingTypesController : ControllerBase
    {
        private readonly IBuildingTypesRepository _buildingTypesRepository;

        public BuildingTypesController(IBuildingTypesRepository buildingTypesRepository)
        {
            _buildingTypesRepository = buildingTypesRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            return await _buildingTypesRepository.GetAllBuildingTypesAsync();
        }
    }
}