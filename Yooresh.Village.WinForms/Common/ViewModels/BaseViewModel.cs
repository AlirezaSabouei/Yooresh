using RestSharp;

namespace Yooresh.Client.WinForms.Common.ViewModels;

[PropertyChanged.AddINotifyPropertyChangedInterface]
public class BaseViewModel
{
    protected readonly IRestClient RestClient;
    
    protected BaseViewModel(IRestClient restClient)
    {
        RestClient = restClient;
    }
}
