namespace AzureCosmos.Server;

public class CosmosDbUtilities
{
    public static async Task<CosmosDbService> InitiateCosmosDbService(IConfigurationSection configurationSection)
    {
        string databaseName = configurationSection["DatabaseName"];
        string containerName = configurationSection["ContainerName"];
        
        string account = configurationSection["Account"];
        string key = configurationSection["Key"];
        CosmosClient client = new CosmosClient(account, key);
        DatabaseResponse databaseResponse = await client.CreateDatabaseIfNotExistsAsync(databaseName);
        await databaseResponse.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

        return new CosmosDbService(client, databaseName, containerName);
    }
}
