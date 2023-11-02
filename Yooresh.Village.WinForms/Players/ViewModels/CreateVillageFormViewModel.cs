using Yooresh.Client.WinForms.Common;
using Yooresh.Client.WinForms.Common.ViewModels;
using RestSharp;
using RestSharp.Authenticators;
using Yooresh.Client.WinForms.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.WebSockets;
using System.ComponentModel;
using System.Net;
using Yooresh.Client.WinForms.Players.Models;

namespace Yooresh.Client.WinForms.Players.ViewModels;

public class CreateVillageFormViewModel : BaseViewModel
{
    public List<Faction> Factions { get; set; }

    public CreateVillageDto CreateVillageDto { get; set; }

    public CreateVillageFormViewModel(IRestClient restClient)
        : base(restClient)
    {
        Factions = new List<Faction>();
        CreateVillageDto=new CreateVillageDto();
    }

    public async Task GetFactions()
    {
        var request = new RestRequest
        {
            Method = Method.Get,
            Resource = "/api/Factions",
            Authenticator = new HttpBasicAuthenticator(Statics.Email, Statics.Password)
        };
        var response = await RestClient.ExecuteAsync(request);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var result  = JsonConvert.DeserializeObject<List<Faction>>(response.Content);
            Factions = result;
            return;
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new InformException(
                $"Login Failed{Environment.NewLine}Make sure login information is right{Environment.NewLine}Also make sure your Email is confirmed");
        }

        throw new Exception($"Login failed for unknown reason.{Environment.NewLine}Make sure you are connected to the Internet");
    }

    public async Task CreateVillage()
    {
        var request = new RestRequest()
        {
            Method = Method.Post,
            Resource = "/api/villages",
            Authenticator = new HttpBasicAuthenticator(Statics.Email, Statics.Password)
        };        

        request.AddBody(CreateVillageDto);

        var response = await RestClient.ExecuteAsync(request);

        if (response.StatusCode != HttpStatusCode.OK && response.Content != null)
        {
            throw new InformException(response.Content);
        }

        throw new Exception($"Unknown error{Environment.NewLine}Check your connection to the server");
    }
}