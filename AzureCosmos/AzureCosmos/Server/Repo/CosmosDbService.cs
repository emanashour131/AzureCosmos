namespace AzureCosmos.Server;

public class CosmosDbService : ICosmosDbService
{
    protected readonly Container _container;
    public CosmosDbService(CosmosClient cosmosDbClient, string databaseName, string containerName) =>
        _container = cosmosDbClient.GetContainer(databaseName, containerName);

    public async Task<IEnumerable<Family>> Get(string queryString)
    {
        FeedIterator<Family> query = _container.GetItemQueryIterator<Family>(new QueryDefinition(queryString));

        List<Family> families = new();
        while (query.HasMoreResults)
            families.AddRange(await query.ReadNextAsync());

        return families;
    }
    public async Task<Family> Get(Guid id)
    {
        try
        {
            ItemResponse<Family> itemResponse = await _container.ReadItemAsync<Family>(id.ToString(), new PartitionKey(id.ToString()));
            return itemResponse.Resource;
        }
        catch (CosmosException exception)
        {
            Log.Error(exception.Message);
            throw;
        }
    }

    public async Task<Family> Add(Family family) => (await _container.CreateItemAsync(family, new PartitionKey(family.Id.ToString()))).Resource;
    public async Task<IEnumerable<Family>> Add(IEnumerable<Family> families)
    {
        IEnumerable<Task<Family>> creationTasks =
            families.Select(value => Add(value)).ToList();
        await Task.WhenAll(creationTasks).ConfigureAwait(false);
        return (IEnumerable<Family>)creationTasks;
    }
    public async Task<Family> Update(Guid id, Family family) => (await _container.UpsertItemAsync(family, new PartitionKey(id.ToString()))).Resource;
    public async Task<IEnumerable<Family>> Update(IEnumerable<Family> families)
    {
        IEnumerable<Task<Family>> UpdateTasks =
            families.Select(value => Update(value.Id, value)).ToList();
        await Task.WhenAll(UpdateTasks).ConfigureAwait(false);
        return (IEnumerable<Family>)UpdateTasks;
    }
    public async Task<Family> Delete(Guid id) => (await _container.DeleteItemAsync<Family>(id.ToString(), new PartitionKey(id.ToString()))).Resource;
}