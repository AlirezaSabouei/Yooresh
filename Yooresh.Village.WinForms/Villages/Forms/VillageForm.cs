using Yooresh.Client.WinForms.Common.Forms;
using Yooresh.Client.WinForms.Common;
using Yooresh.Client.WinForms.Villages.ViewModels;

namespace Yooresh.Client.WinForms.Villages.Forms
{
    public partial class VillageForm : BaseForm
    {
        private readonly VillageFormViewModel _viewModel;
        private readonly VillageUpgradesForm _villageUpgradesForm;

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
            try
            {
                Loader(true);
                await _viewModel.GetVillage();
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

        private void btnUpgrades_Click(object sender, EventArgs e)
        {
            _villageUpgradesForm.ShowDialog();
        }
    }
}
