using Yooresh.Client.WinForms.Common;
using Yooresh.Client.WinForms.Common.Forms;
using Yooresh.Client.WinForms.Villages.Models;
using Yooresh.Client.WinForms.Villages.ViewModels;

namespace Yooresh.Client.WinForms.Villages.Forms
{
    public partial class VillageUpgradesForm : BaseForm
    {
        private readonly VillageUpgradesFormViewModel _viewModel;

        public VillageUpgradesForm(VillageUpgradesFormViewModel villageUpgradesFormViewModel)
        {
            InitializeComponent();
            _viewModel = villageUpgradesFormViewModel;
        }

        protected override void AddDataBindings()
        {
            if (listBoxResourceBuildings.DataBindings.Count > 0)
            {
                return;
            }

            listBoxResourceBuildings.DataBindings.Add(
                nameof(ListBox.DataSource),
                _viewModel,
                nameof(VillageUpgradesFormViewModel.AvailableResourceBuildingUpgrades),
                true,
                DataSourceUpdateMode.OnPropertyChanged);

            listBoxResourceBuildings.DataBindings.Add(
                nameof(ListBox.SelectedValue),
                _viewModel.UpdateResourceBuildingDto,
                nameof(UpdateResourceBuildingDto.ToId),
                true,
                DataSourceUpdateMode.OnPropertyChanged);
        }

        private async void VillageUpgradesForm_Load(object sender, EventArgs e)
        {

        }

        private async void VillageUpgradesForm_Shown(object sender, EventArgs e)
        {
            try
            {
                Loader(true);
                await _viewModel.GetAvailableResourceBuildingUpgrades();
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

        private async void btnUpgradeResourceBuilding_Click(object sender, EventArgs e)
        {
            try
            {
                Loader(true);
                await _viewModel.UpgradeResourceBuilding();
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

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Loader(true);
                await _viewModel.UpgradeResourceBuilding();
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

        private void listBoxResourceBuildings_SelectedIndexChanged(object sender, EventArgs e)
        {
           // this.Text = _viewModel.SelectedResourceBuildingUpgrade.ToString();
        }
    }
}
