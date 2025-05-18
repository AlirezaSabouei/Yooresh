using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eloy.DTO;

namespace Yooresh.Client.WinForms.Common.Controls;

public partial class ResourceControl : UserControl
{
    public VillageDto Village { get; set; }
    public ResourceControl()
    {
        InitializeComponent();
    }

    private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
    {
        var village = e.Argument as VillageDto;
        while (true)
        {
            if (backgroundWorker1.CancellationPending)
            {
                break;
            }
            
            foreach (var villageVesourceBuilding in village.VillageResourceBuildings)
            {
                var duration = (DateTime.UtcNow - villageVesourceBuilding.LastHarvestTime).TotalHours;
                var resource= villageVesourceBuilding.ResourceBuilding.HourlyProduction * duration;
                if (resource>new ResourceDto(0,0,0,0,0))
                {
                    village.Resource += resource;
                    villageVesourceBuilding.LastHarvestTime = DateTimeOffset.UtcNow;
                }                
            }
            backgroundWorker1.ReportProgress(1, village.Resource);
            Thread.Sleep(1000);
        }
    }

    private void ResourceControl_Load(object sender, EventArgs e)
    {

    }

    private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        var resource = e.UserState as ResourceDto;

        lblFood.Text = resource!.Food.ToString();
        lblLumber.Text = resource!.Lumber.ToString();
        lblStone.Text = resource!.Stone.ToString();
        lblMetal.Text = resource!.Metal.ToString();
    }

    public void StartCounter()
    {
        backgroundWorker1.CancelAsync();

        while (backgroundWorker1.IsBusy)
        {
            Application.DoEvents();
        }

        if (Village != null)
        {
            backgroundWorker1.RunWorkerAsync(Village);
        }
    }
}
