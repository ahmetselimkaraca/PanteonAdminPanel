using System.Collections.Generic;
using System.Threading.Tasks;

namespace PanteonAdminPanel.API.Repositories
{
    public interface IBuildingTypesRepository
    {
        Task<IEnumerable<string>> GetAllBuildingTypesAsync();
        Task<bool> IsBuildingTypeValidAsync(string buildingType);
    }
}