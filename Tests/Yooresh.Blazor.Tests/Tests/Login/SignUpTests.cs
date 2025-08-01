using NUnit.Framework;
using Shouldly;
using System.ComponentModel.DataAnnotations;
using Yooresh.Village.Tests.Pages.Login;
using Yooresh.Village.Models.Players;
using Yooresh.Village.Tests.Common;
using Yooresh.Village.Tests.Helpers;
using Yooresh.Village.Validations;

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

        await Page.WaitForURLAsync("**/counter");
        Assert.That(Page.Url, Does.Contain("/counter"));
    }

    [Test]
    public async Task SignUp_InvalidName_ShouldError()
    {
        var signUpPage = new SignUpPage(Page);

        await signUpPage.GoToAsync();
        await signUpPage.FillSignUpForm("", "test@example.com", "Password123@");
        await signUpPage.SubmitForm();

        var errorText = await Page.InnerTextAsync("css=div.validation-message");
        var expectedMessage = ValidationHelper.GetErrorMessage<SignUpDto>(nameof(SignUpDto.Name));
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
        var expectedMessage = ValidationHelper.GetErrorMessage<SignUpDto>(nameof(SignUpDto.Email), typeof(EmailAddressAttribute));
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
        var expectedMessage = ValidationHelper.GetErrorMessage<SignUpDto>(nameof(SignUpDto.Email));
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
