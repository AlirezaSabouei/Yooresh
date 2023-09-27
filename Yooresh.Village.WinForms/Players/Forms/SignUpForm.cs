using Yooresh.Village.WinForms.Common;
using Yooresh.Village.WinForms.Common.Forms;
using Yooresh.Village.WinForms.Common.ViewModels;
using Yooresh.Village.WinForms.Players.Models;
using Yooresh.Village.WinForms.Players.ViewModels;
using Yooresh.Village.WinForms.Village.Forms;

namespace Yooresh.Village.WinForms.Players.Forms;

public partial class SignUpForm : BaseForm
{
    private readonly SignUpFormViewModel _viewModel;

    public SignUpForm(SignUpFormViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
    }

    protected override void AddDataBindings()
    {
        txtName.DataBindings.Add(nameof(TextBox.Text), _viewModel.SignUpDto, nameof(SignUpModel.Name));
        txtEmail.DataBindings.Add(nameof(TextBox.Text), _viewModel.SignUpDto, nameof(SignUpModel.Email));
        txtPassword.DataBindings.Add(nameof(TextBox.Text), _viewModel.SignUpDto, nameof(SignUpModel.Password));
        txtConfirmation.DataBindings.Add(nameof(TextBox.Text), _viewModel.SignUpDto, nameof(SignUpModel.PasswordConfirmation));
    }

    private async void btnSignUp_Click(object sender, EventArgs e)
    {
        try
        {
            Loader(true);
            var result = await _viewModel.SignUp();
            ShowMessage(result, MessageType.Success);
            this.Close();
        }
        catch (InformException informException)
        {
            ShowMessage(informException.Message, MessageType.Failure);
        }
        catch (Exception exception)
        {
            ShowMessage(exception.Message, MessageType.Failure);
        }
        finally
        {
            Loader(false);
        }
    }
}
