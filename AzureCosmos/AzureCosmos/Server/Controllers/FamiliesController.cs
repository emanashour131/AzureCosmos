namespace AzureCosmos.Server;

[Route("api/[controller]")]
[ApiController]
public class FamiliesController : ControllerBase
{
    private readonly ICosmosDbService _cosmosDbService;
    public FamiliesController(ICosmosDbService cosmosDbService) =>
        _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));

    [HttpGet]
    public async Task<IEnumerable<Family>> GetBulk() => await _cosmosDbService.Get("SELECT * FROM c");

    [HttpGet("{id}")]
    public async Task<Family> Get(Guid id) => await _cosmosDbService.Get(id);

    [HttpPost]
    public async Task<Family> Create([FromBody] Family family) => await _cosmosDbService.Add(family);

    [HttpPut("{id}")]
    public async Task<Family> Edit([FromRoute] Guid id, [FromBody] Family family) => await _cosmosDbService.Update(id, family);

    [HttpDelete("{id}")]
    public async Task<Family> Delete(Guid id) => await _cosmosDbService.Delete(id);
}
