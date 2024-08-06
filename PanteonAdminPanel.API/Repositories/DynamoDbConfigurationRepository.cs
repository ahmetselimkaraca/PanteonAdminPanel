using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using PanteonAdminPanel.API.Models;

namespace PanteonAdminPanel.API.Repositories
{
    public class DynamoDbConfigurationRepository : IConfigurationRepository
    {
        private readonly IDynamoDBContext _context;

        public DynamoDbConfigurationRepository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Configuration>> GetAllConfigurationsAsync()
        {
            var conditions = new List<ScanCondition>();
            return await _context.ScanAsync<Configuration>(conditions).GetRemainingAsync();
        }

        public async Task<Configuration> GetConfigurationAsync(string buildingType)
        {
            return await _context.LoadAsync<Configuration>(buildingType);
        }

        public async Task AddConfigurationAsync(Configuration configuration)
        {
            await _context.SaveAsync(configuration);
        }

        public async Task UpdateConfigurationAsync(Configuration configuration)
        {
            await _context.SaveAsync(configuration);
        }

        public async Task DeleteConfigurationAsync(string buildingType)
        {
            await _context.DeleteAsync<Configuration>(buildingType);
        }
    }
}