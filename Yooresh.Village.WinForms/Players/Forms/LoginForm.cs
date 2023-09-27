using Yooresh.Village.WinForms.Common;
using Yooresh.Village.WinForms.Common.Forms;
using Yooresh.Village.WinForms.Common.ViewModels;
using Yooresh.Village.WinForms.Players.ViewModels;
using Yooresh.Village.WinForms.Properties;
using Yooresh.Village.WinForms.Village.Forms;

namespace Yooresh.Village.WinForms.Players.Forms;

public partial class LoginForm : BaseForm
{
    private readonly LoginFormViewModel _viewModel;
    private readonly SignUpForm _signUpForm;
    private readonly VillageForm _villageForm;

    protected override void LoadSettings()
    {
        checkBox1.Checked = Settings.Default.LoginForm_RememberMe;
        if (!checkBox1.Checked) return;
        txtEmail.Text = Settings.Default.LoginForm_Email;
        txtPassword.Text = Settings.Default.LoginForm_Password;
    }

    protected override void SaveSettings()
    {
        Settings.Default.LoginForm_RememberMe = checkBox1.Checked;
        Settings.Default.LoginForm_Email = txtEmail.Text.Trim();
        Settings.Default.LoginForm_Password = txtPassword.Text.Trim();
        Settings.Default.Save();
    }

    protected override void AddDataBindings()
    {
        txtEmail.DataBindings.Add(nameof(TextBox.Text), _viewModel, nameof(LoginFormViewModel.Email));
        txtPassword.DataBindings.Add(nameof(TextBox.Text), _viewModel, nameof(LoginFormViewModel.Password));
    }

    public LoginForm(LoginFormViewModel viewModel, SignUpForm signUpForm, VillageForm villageForm)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _signUpForm = signUpForm;
        _villageForm = villageForm;
    }

    private void lblSignUp_Click(object sender, EventArgs e)
    {
        _signUpForm.ShowDialog();
    }

    private async void btnLogin_Click(object sender, EventArgs e)
    {
        if (checkBox1.Checked)
        {
            SaveSettings();
        }

        try
        {
            Loader(true);
            await _viewModel.Login();
            _villageForm.ShowDialog();
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