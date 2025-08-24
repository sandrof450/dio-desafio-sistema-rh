using Azure.Data.Tables;
using sistema_de_rh_API.Models;

namespace sistema_de_rh_API.Services
{
    public class FuncionarioLogService
    {
        private readonly TableClient _tableClient;

        public FuncionarioLogService(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("ConnectionStrings:SAConnectionString");
            var tableName = configuration.GetValue<string>("ConnectionStrings:AzureTableName");

            var serviceClient = new TableServiceClient(connectionString);
            _tableClient = serviceClient.GetTableClient(tableName);

            _tableClient.CreateIfNotExists();
        }

        public async Task RegistrarLogAsync(FuncionarioLog log)
        {
            await _tableClient.AddEntityAsync(log);
        }
    }
}