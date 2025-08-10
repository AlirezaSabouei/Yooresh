using NUnit.Framework;
using Shouldly;
using Yooresh.Village.Tests.Pages.Login;
using Yooresh.Village.Tests.Common;
using Yooresh.Village.Tests.Helpers;
using Yooresh.Village.Validations;
using Yooresh.Village.Models.Accounts;

namespace Yooresh.Village.Tests.Tests.Login;

public class SignUpTests : TestBase
{
    [Test]
    public async Task SignUp_ShouldWork()
    {
        var signUpPage = new SignUpPage(Page);

        await signUpPage.GoToAsync();
        await signUpPage.FillSignUpForm("Alireza Sabouei", "test@example.com", "Password123@");
        await signUpPage.SubmitForm();

        await Page.WaitForURLAsync("**/confirmaccount?playerId=*");
        Assert.That(Page.Url, Does.Contain("/confirmaccount"));
    }

    [Test]
    public async Task SignUp_InvalidName_ShouldError()
    {
        var signUpPage = new SignUpPage(Page);

        await signUpPage.GoToAsync();
        await signUpPage.FillSignUpForm("", "test@example.com", "Password123@");
        await signUpPage.SubmitForm();

        var errorText = await Page.InnerTextAsync("css=div.validation-message");
        var expectedMessage = "The Name field is required.";
        errorText.ShouldBe(expectedMessage);
    }
    
    [Test]
    public async Task SignUp_InvalidEmail_ShouldError()
    {
        var signUpPage = new SignUpPage(Page);

        await signUpPage.GoToAsync();
        await signUpPage.FillSignUpForm("Some Name", "test", "Password123@");
        await signUpPage.SubmitForm();

        var errorText = await Page.InnerTextAsync("css=div.validation-message");
        var expectedMessage = "The Email field is not a valid e-mail address.";
        errorText.ShouldBe(expectedMessage);
    }

    [Test]
    public async Task SignUp_EmptyEmail_ShouldError()
    {
        var signUpPage = new SignUpPage(Page);

        await signUpPage.GoToAsync();
        await signUpPage.FillSignUpForm("Some Name", "", "Password123@");
        await signUpPage.SubmitForm();

        var errorText = await Page.InnerTextAsync("css=div.validation-message");
        var expectedMessage = "The Email field is required.";
        errorText.ShouldBe(expectedMessage);
    }

    [Test]
    [TestCase("")]
    [TestCase("ader")] //Less than 5 chars
    [TestCase("aderd")] //Having No Digit
    [TestCase("123")] //Less than 5 chars
    [TestCase("12345")] //Having no letters
    public async Task SignUp_InvalidPassword_ShouldError(string password)
    {
        var signUpPage = new SignUpPage(Page);

        await signUpPage.GoToAsync();
        await signUpPage.FillSignUpForm("Some Name", "a@a.com", password);
        await signUpPage.SubmitForm();

        var errorText = await Page.InnerTextAsync("css=div.validation-message");
        var expectedMessage = ValidationHelper.GetErrorMessage<SignUpDto>(nameof(SignUpDto.Password),typeof(PasswordStrength));
        errorText.ShouldBe(expectedMessage);
    }
}
