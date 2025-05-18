using Yooresh.API;
using Yooresh.API.Controllers;

namespace Yooresh.AcceptanceTests.Controllers;

public abstract class ControllerTests<TController> : IDisposable where TController : BaseApiController
{
    private static HttpClient? _client;

    protected HttpClient Client
    {
        get
        {
            if (_client != null) return _client;

            _client = Factory.CreateClient();
            _client.BaseAddress = new Uri(@"https://localhost:44333/");
            return _client;
        }
    }

    protected readonly CustomWebApplicationFactory<Program> Factory;

    protected ControllerTests()
    {
        Factory = new CustomWebApplicationFactory<Program>();
    }

    protected abstract Task ClearDatabase();

    public void Dispose()
    {
        ClearDatabase()
            .Wait();
    }
}