namespace AzureCosmos.Server;
public interface ICosmosBaseRepository<TEntity> 
{
    Task<IEnumerable<TEntity>> Get(string queryString);
    Task<TEntity> Get(Guid id);
    Task<TEntity> Add(TEntity entity);
    Task<IEnumerable<TEntity>> Add(IEnumerable<TEntity> entities);
    Task<TEntity> Update(Guid id, TEntity entity);
    Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entities);
    Task<TEntity> Delete(Guid id);
}
