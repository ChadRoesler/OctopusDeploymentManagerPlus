using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using log4net;
using Octopus.Client.Model;
using OctopusDeploymentManagerPlus.Constants;
using OctopusDeploymentManagerPlus.ExtensionMethods;

namespace OctopusDeploymentManagerPlus.Forms.Prompts
{
    public partial class frmVersionSelector : Form
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public frmVersionSelector()
        {
            InitializeComponent();
        }

        public bool CorrectRelease { get; set; }
        public bool MultiRelease { get; set; }

        public ReleaseResource ChosenRelease
        {
            get
            {
                return (ReleaseResource)cmbRelease.SelectedValue;
            }
            set
            {
                cmbRelease.SelectedValue = value;
            }
        }

        public List<ReleaseResource> ReleaseList { get; set; }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            CorrectRelease = true;
            Log.Debug("Version Selection Validated");
            Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CorrectRelease = false;
            Log.Debug("Version Selection Cancelled");
            Hide();
        }

        public void PrepFrom()
        {
            cmbRelease.BindReleaseListToComboBox(ReleaseList);
            if (MultiRelease)
            {
                cmbRelease.Visible = true;
                lblReleaseSelect.Visible = true;
                lblReleaseVersionChoice.Visible = true;
                lblReleaseVersionSelected.Visible = false;
            }
            else
            {
                cmbRelease.Visible = false;
                lblReleaseSelect.Visible = false;
                lblReleaseVersionChoice.Visible = false;
                lblReleaseVersionSelected.Text = string.Format(UiStrings.VersionVerificaiton, ChosenRelease.Version);
                lblReleaseVersionSelected.Visible = true;
            }
        }
    }
}
