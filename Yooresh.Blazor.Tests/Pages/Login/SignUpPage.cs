using Microsoft.Playwright;

namespace Yooresh.Blazor.Tests.Pages.Login;

public class SignUpPage(IPage page)
{
    private readonly IPage _page = page;

    public async Task GoToAsync()
    {
        await _page.GotoAsync($"{Constants.BLAZOR_BASE_ADDRESS}/village");
        await _page.WaitForTimeoutAsync(200);
    }

    public async Task FillSignUpForm(string name, string email, string password)
    {
        await _page.FillAsync("#full-name", name);
        await _page.FillAsync("#email", email);
        await _page.FillAsync("#password", password);
    }

    public async Task SubmitForm()
    {
        await _page.ClickAsync("button[type=submit]");
    }
}
