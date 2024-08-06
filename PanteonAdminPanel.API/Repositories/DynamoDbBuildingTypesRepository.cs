using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace PanteonAdminPanel.API.Repositories
{
    public class DynamoDbBuildingTypesRepository : IBuildingTypesRepository
    {
        private readonly IDynamoDBContext _context;

        public DynamoDbBuildingTypesRepository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> GetAllBuildingTypesAsync()
        {
            var conditions = new List<ScanCondition>();
            var buildingTypes = await _context.ScanAsync<BuildingType>(conditions).GetRemainingAsync();
            return buildingTypes.Select(bt => bt.TypeName);
        }

        public async Task<bool> IsBuildingTypeValidAsync(string buildingType)
        {
            var buildingTypeEntity = await _context.LoadAsync<BuildingType>(buildingType);
            return buildingTypeEntity != null;
        }
    }

    [DynamoDBTable("BuildingTypes")]
    public class BuildingType
    {
        [DynamoDBHashKey]
        public string TypeName { get; set; }
    }
}