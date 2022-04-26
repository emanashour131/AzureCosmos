
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
IConfigurationSection section = builder.Configuration.GetSection("CosmosDb");

CosmosDbService cosmosDbService = await CosmosDbUtilities.InitiateCosmosDbService(section);
builder.Services.AddSingleton<ICosmosDbService>(cosmosDbService);

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseWebAssemblyDebugging();
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


//app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
