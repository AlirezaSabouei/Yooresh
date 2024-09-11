using Yooresh.Client.WinForms.Common.Forms;
using Yooresh.Client.WinForms.Common;
using Yooresh.Client.WinForms.Villages.ViewModels;
using Yooresh.Client.WinForms.Common.Controls;

namespace Yooresh.Client.WinForms.Villages.Forms
{
    public partial class VillageForm : BaseForm
    {
        private readonly VillageFormViewModel _viewModel;
        private readonly VillageUpgradesForm _villageUpgradesForm;

        protected override void AddDataBindings()
        {
            //  resourceControl1.DataBindings.Add(nameof(ResourceControl.Village), _viewModel, nameof(VillageFormViewModel.Village));
        }

        public VillageForm(
            VillageFormViewModel villageFormViewModel,
            VillageUpgradesForm villageUpgradesForm)
        {
            InitializeComponent();
            _viewModel = villageFormViewModel;
            _villageUpgradesForm = villageUpgradesForm;
        }

        private void VillageForm_Load(object sender, EventArgs e)
        {

        }

        private async void VillageForm_Shown(object sender, EventArgs e)
        {            
            await LoadVillage();
        }

        private async Task LoadVillage()
        {
            try
            {
                Loader(true);
                await _viewModel.GetVillage();
                resourceControl1.Village = _viewModel.Village;
                resourceControl1.StartCounter();
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

        private async void btnUpgrades_Click(object sender, EventArgs e)
        {
            _villageUpgradesForm.ShowDialog();
            await LoadVillage();
        }
    }
}
