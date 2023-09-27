using RestSharp;

namespace Yooresh.Village.WinForms.Common.ViewModels;

[PropertyChanged.AddINotifyPropertyChangedInterface]
public class BaseViewModel
{
    protected readonly IRestClient RestClient;
    
    protected BaseViewModel(IRestClient restClient)
    {
        RestClient = restClient;
    }
}
