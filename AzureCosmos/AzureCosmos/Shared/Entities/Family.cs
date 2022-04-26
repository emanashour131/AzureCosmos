using Newtonsoft.Json;
namespace AzureCosmos.Shared;
public class Family : BaseSettingsEntity
{
    public string LastName { get; set; }
    //public Parent[] Parents { get; set; }
    //public Child[] Children { get; set; }
    //public Address Address { get; set; }
    public bool IsRegistered { get; set; }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}