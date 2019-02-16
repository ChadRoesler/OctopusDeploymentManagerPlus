using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OctopusDeploymentManagerPlus.Constants;
using OctopusDeploymentManagerPlus.Helpers;

namespace OctopusDeploymentManagerPlus.Forms.Prompts
{
    public partial class frmInterruptionManager : Form
    {
        #region PrivateVars
        private static Dictionary<string, string> Errors;
        #endregion
        public frmInterruptionManager()
        {
            InitializeComponent();
            Focus();
        }

        #region MajorVars
        public string NoteText
        {
            get
            {
                return txtNote.Text;
            }
            set
            {
                txtNote.Text = value;
            }
        }
        public string Error
        {
            get { return rtbError.Text; }
            set
            {
                rtbError.ForeColor = Color.Red;
                rtbError.Text = value;
            }
        }
        public string DeploymentLink
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public void Cleanup()
        {
            NoteText = string.Empty;
            Error = string.Empty;
        }

        public bool ValidateForm()
        {
            Errors = new Dictionary<string, string>();
            if(string.IsNullOrWhiteSpace(NoteText))
            {
                Errors.Add("txtNote", string.Format(ErrorStrings.RequiredFieldFormat, lblNote.Text.Trim(':')));
            }
            return (Errors.Count == 0);
        }
        #endregion Methods

        #region Events
        private void btnRetry_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                foreach (var control in ControlHelper.GetAllTextBoxControls(this))
                {
                    errorProvider.SetError(control, "");
                }
                foreach (var error in Errors)
                {
                    var control = Controls.Find(error.Key, true).FirstOrDefault();
                    errorProvider.SetError(control, error.Value);
                    if (control.GetType() == typeof(DateTimePicker) || control.GetType() == typeof(ComboBox))
                    {
                        errorProvider.SetIconPadding(control, 3);
                    }
                    else
                    {
                        errorProvider.SetIconPadding(control, -20);
                    }
                }
                return;
            }
            Hide();
            ((frmOctopusDeploymentManagerPlus)Owner).RetryInterruptedDeploy();
            ((frmOctopusDeploymentManagerPlus)Owner).Focus();
        }

        private void btnFail_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                foreach (var control in ControlHelper.GetAllTextBoxControls(this))
                {
                    errorProvider.SetError(control, "");
                }
                foreach (var error in Errors)
                {
                    var control = Controls.Find(error.Key, true).FirstOrDefault();
                    errorProvider.SetError(control, error.Value);
                    if (control.GetType() == typeof(DateTimePicker) || control.GetType() == typeof(ComboBox))
                    {
                        errorProvider.SetIconPadding(control, 3);
                    }
                    else
                    {
                        errorProvider.SetIconPadding(control, -20);
                    }
                }
                return;
            }
            Hide();
            ((frmOctopusDeploymentManagerPlus)Owner).FailInterruptedDeploy();
            ((frmOctopusDeploymentManagerPlus)Owner).Focus();
        }
        private void btnViewDeployment_Click(object sender, EventArgs e)
        {
            Process.Start(ControlHelper.GetDefaultWebBrowser(), DeploymentLink);
        }
        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(rtbError.Text);
        }
        #endregion Events


    }
}
