namespace AzureCosmos.Server;

public interface ICosmosDbService
{
    Task<IEnumerable<Family>> Get(string queryString);
    Task<Family> Get(Guid id);
    Task<Family> Add(Family family);
    Task<IEnumerable<Family>> Add(IEnumerable<Family> families);
    Task<Family> Update(Guid id, Family family);
    Task<IEnumerable<Family>> Update(IEnumerable<Family> families);
    Task<Family> Delete(Guid id);
}
