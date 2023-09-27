using Yooresh.Village.WinForms.Common.ViewModels;
using Yooresh.Village.WinForms.Properties;
using System.Configuration;
using System.Windows.Forms;

namespace Yooresh.Village.WinForms.Common.Forms;

public partial class BaseForm : Form
{
    private string LocationSettingName => $"{this.Name}Location";
    private string SizeSettingName => $"{this.Name}Size";
    private string StateSettingName => $"{this.Name}State";

    protected void Loader(bool show)
    {
        if (ProgressBar != null)
        {
            ProgressBar.Visible = show;
        }
    }

    protected virtual void ShowMessage(string message, MessageType messageType)
    {
        switch (messageType)
        {
            case MessageType.Success:
                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;
            case MessageType.Failure:
                MessageBox.Show(message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
            case MessageType.Information:
                MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;
            default:
                break;
        }

    }

    public ProgressBar ProgressBar { get; set; }

    protected virtual void AddDataBindings() { }

    protected virtual void SaveSettings() { }

    protected virtual void LoadSettings() { }

    public BaseForm()
    {
        InitializeComponent();
    }

    private void BaseForm_Load(object sender, EventArgs e)
    {
        AddDataBindings();

        LoadSettings();
        Loader(false);

        // if (Settings.Default.Properties[LocationSettingName] == null)
        // {
        //    return;
        // }

        // this.WindowState = (FormWindowState)Settings.Default[StateSettingName];
        // this.Size = (Size)Settings.Default[SizeSettingName];
        //   this.Location = (Point)Settings.Default[LocationSettingName];
    }

    private void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        // SavePositionSettings(LocationSettingName,typeof(Point), this.Location);
        //  SavePositionSettings(SizeSettingName, typeof(Size), this.Size);
        //  SavePositionSettings(StateSettingName, typeof(FormWindowState), this.WindowState);

        SaveSettings();
    }

    private void SavePositionSettings(string settingName, Type type, object value)
    {
        if (Settings.Default.Properties[settingName] == null)
        {
            SettingsProperty settingsProperty = new(settingName)
            {
                DefaultValue = value,
                Name = settingName,
                PropertyType = type
            };
            Settings.Default.Properties.Add(settingsProperty);
        }
        else
        {
            Settings.Default[settingName] = value;
        }
        Settings.Default.Save();
    }
}
