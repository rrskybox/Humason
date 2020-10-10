using Planetarium;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Humason
{
    public partial class FormDome : Form
    {
        public FormDome()
        {
            InitializeComponent();
            //This tab will not be enabled if the DomeAddOnCheckBox is not checked
            SessionControl openSession = new SessionControl();
            DomeHomeAz.Value = (int)openSession.DomeHomeAz;
            HomeDomeButton.BackColor = Color.LightGreen;
            OpenSlitButton.BackColor = Color.LightGreen;
            CloseSlitButton.BackColor = Color.LightGreen;
            GoToAzButton.BackColor = Color.LightGreen;
        }

        private void DomeHomeAz_ValueChanged(object sender, EventArgs e)
        {
            SessionControl openSession = new SessionControl();
            openSession.DomeHomeAz = (int)DomeHomeAz.Value;
            return;
        }

        private void HomeDomeButton_Click(object sender, EventArgs e)
        {
            HomeDomeButton.BackColor = Color.LightSalmon;
            TSXLink.Dome.HomeDome();
            HomeDomeButton.BackColor = Color.LightGreen;
            return;
        }

        private void OpenSlitButton_Click(object sender, EventArgs e)
        {
            OpenSlitButton.BackColor = Color.LightSalmon;
            TSXLink.Dome.OpenDome();
            OpenSlitButton.BackColor = Color.LightGreen;
            return;
        }

        private void CloseSlitButton_Click(object sender, EventArgs e)
        {
            CloseSlitButton.BackColor = Color.LightSalmon;
            TSXLink.Dome.CloseDome();
            CloseSlitButton.BackColor = Color.LightGreen;
            return;
        }

        /// <summary>
        /// Causes dome to rotate to value in degrees of the TargetAz 
        /// Mount will be parked when executing this routine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToAzButton_Click(object sender, EventArgs e)
        {
            GoToAzButton.BackColor = Color.LightSalmon;
            DomeControl.GoToDomeAz((int)TargetAz.Value);
            GoToAzButton.BackColor = Color.LightGreen;
            return;
        }
    }
}
