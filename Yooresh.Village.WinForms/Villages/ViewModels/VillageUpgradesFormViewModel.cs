using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using Yooresh.Client.WinForms.Common;
using Yooresh.Client.WinForms.Common.ViewModels;
using Yooresh.Client.WinForms.Models;
using Yooresh.Client.WinForms.Villages.Models;

namespace Yooresh.Client.WinForms.Villages.ViewModels;

public class VillageUpgradesFormViewModel : BaseViewModel
{
    public List<ResourceBuilding> AvailableResourceBuildingUpgrades { get; set; }
    public UpdateResourceBuildingDto UpdateResourceBuildingDto { get; set; }

    public VillageUpgradesFormViewModel(IRestClient restClient)
        : base(restClient)
    {
        AvailableResourceBuildingUpgrades = new List<ResourceBuilding>();
        UpdateResourceBuildingDto=new UpdateResourceBuildingDto();
    }

    public async Task GetAvailableResourceBuildingUpgrades()
    {
        var request = new RestRequest
        {
            Method = Method.Get,
            Resource = "/api/Villages/AvailableResourceBuildingUpgrades",
            Authenticator = new HttpBasicAuthenticator(Statics.Email, Statics.Password)
        };

        var response = await RestClient.ExecuteAsync(request);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            AvailableResourceBuildingUpgrades = JsonConvert.DeserializeObject<List<ResourceBuilding>>(response.Content);
            return;
        }
        else if (response.StatusCode != HttpStatusCode.OK && response.Content != null)
        {
            throw new InformException(response.Content);
        }

        throw new Exception($"Login failed for unknown reason.{Environment.NewLine}Make sure you are connected to the Internet");
    }

    public async Task UpgradeResourceBuilding()
    {
        var request = new RestRequest
        {
            Method = Method.Post,
            Resource = "/api/Villages/UpgradeResourceBuilding",
            Authenticator = new HttpBasicAuthenticator(Statics.Email, Statics.Password)
        };

        request.AddBody(UpdateResourceBuildingDto);

        var response = await RestClient.ExecuteAsync(request);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return;
        }
        else if (response.StatusCode != HttpStatusCode.OK && response.Content != null)
        {
            throw new InformException(response.Content);
        }

        throw new Exception($"Unknown error.{Environment.NewLine}Make sure you are connected to the Internet");
    }
}