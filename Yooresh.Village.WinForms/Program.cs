using Microsoft.Extensions.Configuration;
using RestSharp;
using Unity;
using Unity.Lifetime;
using Yooresh.Client.WinForms.Players.Forms;
using Yooresh.Client.WinForms.Players.ViewModels;
using Yooresh.Client.WinForms.Villages.Forms;

namespace Yooresh.Client.WinForms;

static class Program
{
    private static IUnityContainer container;
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        var configuration = builder.Build();
        var baseAddress = configuration.GetSection("BaseUrl").Value;

        container =new UnityContainer();
        container.RegisterType<SignUpForm,SignUpForm>();

        container.RegisterType<LoginForm, LoginForm>();
        container.RegisterType<SignUpFormViewModel, SignUpFormViewModel>();
        container.RegisterType<CreateVillageForm, CreateVillageForm>();


        container.RegisterType<VillageForm, VillageForm>();

        container.RegisterInstance<IRestClient>(new RestClient(baseAddress!)/*,new SingletonLifetimeManager()*/);

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(container.Resolve<LoginForm>());
       // Application.Run(new CreateVillageForm());
    }
}