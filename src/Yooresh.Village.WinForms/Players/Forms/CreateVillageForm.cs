using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yooresh.Client.WinForms.Common;
using Yooresh.Client.WinForms.Common.Forms;
using Yooresh.Client.WinForms.Players.Models;
using Yooresh.Client.WinForms.Players.ViewModels;
using Yooresh.Client.WinForms.Villages.Forms;

namespace Yooresh.Client.WinForms.Players.Forms
{
    public partial class CreateVillageForm : BaseForm
    {
        private readonly CreateVillageFormViewModel _viewModel;
        private readonly VillageForm _villageForm;

        public CreateVillageForm(CreateVillageFormViewModel viewModel,VillageForm villageForm)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _villageForm = villageForm;
        }

        protected override void AddDataBindings()
        {
            listBox1.DataBindings.Add(
                nameof(listBox1.DataSource),
                _viewModel,
                nameof(CreateVillageFormViewModel.Factions), true, DataSourceUpdateMode.OnPropertyChanged);

            listBox1.DataBindings.Add(
                nameof(listBox1.SelectedValue),
                _viewModel.CreateVillageDto,
                nameof(CreateVillageDto.FactionId), true, DataSourceUpdateMode.OnPropertyChanged);

            txtVillageName.DataBindings.Add(
                nameof(txtVillageName.Text),
                _viewModel.CreateVillageDto,
                nameof(CreateVillageDto.Name), true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            (sender as PictureBox).BorderStyle = BorderStyle.Fixed3D;
        }

        private async void CreateVillageForm_Shown(object sender, EventArgs e)
        {
            try
            {
                Loader(true);
                await _viewModel.GetFactions();
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

        private async void btnCreateVillage_Click(object sender, EventArgs e)
        {
            try
            {
                Loader(true);
                await _viewModel.CreateVillage();
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
}
