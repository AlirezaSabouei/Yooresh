using Yooresh.Village.WinForms.Common;
using Yooresh.Village.WinForms.Common.ViewModels;
using RestSharp;
using RestSharp.Authenticators;

namespace Yooresh.Village.WinForms.Players.ViewModels;

public class LoginFormViewModel : BaseViewModel
{
    public string Email { get; set; }

    public string Password { get; set; }

    public LoginFormViewModel(IRestClient restClient)
        : base(restClient)
    {
    }

    public async Task Login()
    {
        var request = new RestRequest
        {
            Method = Method.Get,
            Resource = "/api/CheckPlayer",
            Timeout = 5000,
            Authenticator = new HttpBasicAuthenticator(Email, Password)
        };
        var response = await RestClient.ExecuteAsync(request);

        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
        {
            Statics.Email = Email;
            Statics.Password = Password;
            return;
        }
        else if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new InformException(
                $"Login Failed{Environment.NewLine}Make sure login information is right{Environment.NewLine}Also make sure your Email is confirmed");
        }

        throw new Exception($"Login failed for unknown reason.{Environment.NewLine}Make sure you are connected to the Internet");
    }


}