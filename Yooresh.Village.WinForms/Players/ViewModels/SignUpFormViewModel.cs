using Yooresh.Village.WinForms.Common;
using Yooresh.Village.WinForms.Common.ViewModels;
using RestSharp;
using System.Net;
using Yooresh.Village.WinForms.Players.Models;
using System.Security.Cryptography;
using System.Text.Unicode;
using System.Text;
using Newtonsoft.Json;

namespace Yooresh.Village.WinForms.Players.ViewModels;

public class SignUpFormViewModel : BaseViewModel
{
    public SignUpModel SignUpDto { get; set; }
    
    public SignUpFormViewModel(IRestClient restClient)
        : base(restClient)
    {
        SignUpDto = new SignUpModel();
    }

    public async Task<string> SignUp()
    {
        var request = new RestRequest()
        {
            Method = Method.Post,
            Resource = "/api/Players",
            
        };
        request.AddBody(SignUpDto);

        var response = await RestClient.ExecuteAsync(request);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return $"Your account is created{Environment.NewLine}Check your email for the activation link";
        }
        else if (response.StatusCode != HttpStatusCode.OK && response.Content != null)
        {
            throw new InformException(response.Content);
        }

        throw new Exception($"Unknown error{Environment.NewLine}Check your connection to the server");
    }
}