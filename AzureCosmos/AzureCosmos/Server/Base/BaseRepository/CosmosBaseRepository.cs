namespace AzureCosmos.Server;
public class CosmosBaseRepository<TEntity> : ICosmosBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly Container _container;
    public CosmosBaseRepository(CosmosClient cosmosDbClient, string databaseName, string containerName) =>
        _container = cosmosDbClient.GetContainer(databaseName, containerName);

    public async Task<IEnumerable<TEntity>> Get(string queryString)
    {
        FeedIterator<TEntity> query = _container.GetItemQueryIterator<TEntity>(new QueryDefinition(queryString));

        List<TEntity> entities = new();
        while (query.HasMoreResults)
            entities.AddRange(await query.ReadNextAsync());

        return entities;
    }
    public async Task<TEntity> Get(Guid id)
    {
        try
        {
            ItemResponse<TEntity> itemResponse = await _container.ReadItemAsync<TEntity>(id.ToString(), new PartitionKey(id.ToString()));
            return itemResponse.Resource;
        }
        catch (CosmosException exception)
        {
            Log.Error(exception.Message);
            throw;
        }
    }
    public async Task<TEntity> Add(TEntity TEntity) => (await _container.CreateItemAsync(TEntity, new PartitionKey(TEntity.Id.ToString()))).Resource;
    public async Task<IEnumerable<TEntity>> Add(IEnumerable<TEntity> entities)
    {
        IEnumerable<Task<TEntity>> creationTasks =
            entities.Select(value => Add(value)).ToList();
        await Task.WhenAll(creationTasks).ConfigureAwait(false);
        return (IEnumerable<TEntity>)creationTasks;
    }
    public async Task<TEntity> Update(Guid id, TEntity TEntity) => (await _container.UpsertItemAsync(TEntity, new PartitionKey(id.ToString()))).Resource;
    public async Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entities)
    {
        IEnumerable<Task<TEntity>> UpdateTasks =
            entities.Select(value => Update(value.Id, value)).ToList();
        await Task.WhenAll(UpdateTasks).ConfigureAwait(false);
        return (IEnumerable<TEntity>)UpdateTasks;
    }
    public async Task<TEntity> Delete(Guid id) => (await _container.DeleteItemAsync<TEntity>(id.ToString(), new PartitionKey(id.ToString()))).Resource;
}
