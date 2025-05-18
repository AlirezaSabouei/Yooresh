using Yooresh.Client.WinForms.Common;
using Yooresh.Client.WinForms.Common.Forms;
using Yooresh.Client.WinForms.Players.ViewModels;
using Yooresh.Client.WinForms.Properties;
using Yooresh.Client.WinForms.Villages.Forms;

namespace Yooresh.Client.WinForms.Players.Forms;

public partial class LoginForm : BaseForm
{
    private readonly LoginFormViewModel _viewModel;
    private readonly SignUpForm _signUpForm;
    private readonly VillageForm _villageForm;
    private readonly CreateVillageForm _createVillageForm;

    protected override void LoadSettings()
    {
        checkBox1.Checked = Settings.Default.LoginForm_RememberMe;
        if (!Settings.Default.LoginForm_RememberMe) return;
        _viewModel.Email = Settings.Default.LoginForm_Email;
        _viewModel.Password = Settings.Default.LoginForm_Password;
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

    public LoginForm(LoginFormViewModel viewModel, SignUpForm signUpForm, VillageForm villageForm, CreateVillageForm createVillageForm)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _signUpForm = signUpForm;
        _villageForm = villageForm;
        _createVillageForm = createVillageForm;
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
            await _viewModel.VillageExists();
            if (_viewModel.HasVillage)
            {
                _villageForm.ShowDialog();
            }
            else
            {
                _createVillageForm.ShowDialog();
            }
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