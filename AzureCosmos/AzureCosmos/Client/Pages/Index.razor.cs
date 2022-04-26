//using AzureCosmos.Shared;
//using Microsoft.AspNetCore.Components;
//using System.Net.Http.Json;
//namespace AzureCosmos.Client;


//public partial class Index
//{
//    [Inject] public HttpClient? _httpClient { get; set; }

//    Family family = new();
//    List<Family>? families;

//    protected async Task OnInitializedAsync() => families = await GetFamilies();

//    private async Task<List<Family>?> GetFamilies() =>
//        await _httpClient.GetFromJsonAsync<List<Family>>("api/families");



//    private async Task SaveFamilies()
//    {
//        if (family == null)
//            return;  // Use Toster to tell the user the Task is empty



//        if (family.Id == null)
//            await AddFamilies();
//        else
//            await EditFamilies();
//    }



//    protected async Task AddFamilies()
//    {
//        await _httpClient.PostAsJsonAsync<Family>("api/families", family);



//        family = new();
//        families = await GetFamilies();
//    }



//    protected async Task EditFamilies()
//    {
//        await _httpClient.PutAsJsonAsync($"api/families", family);



//        family = new();
//        families = await GetFamilies();
//    }



//    protected async Task DeleteTask(Guid? id)
//    {
//        if (id == null)
//            return; // Use Toster to tell the user the Task is empty



//        await _httpClient.DeleteAsync($"api/families/{id}");
//        families = await GetFamilies();
//    }
//}
