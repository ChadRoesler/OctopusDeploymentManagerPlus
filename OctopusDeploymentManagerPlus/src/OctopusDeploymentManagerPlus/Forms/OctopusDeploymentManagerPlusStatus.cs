using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using log4net;
using log4net.Appender;
using OctopusDeploymentManagerPlus.Constants;
using OctopusDeploymentManagerPlus.Enums;
using OctopusDeploymentManagerPlus.ExtensionMethods;
using OctopusDeploymentManagerPlus.Helpers;
using OctopusHelpers.Enums;

namespace OctopusDeploymentManagerPlus.Forms
{
    public partial class frmOctopusDeploymentManagerPlusStatus : Form
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public frmOctopusDeploymentManagerPlusStatus()
        {
            InitializeComponent();
            RichTextBoxAppender.SetRichTextBox(rtbOutputConsole, "RichTextBoxAppender");
        }

        internal bool IsClosedWithActive = false;
        internal void ProgressColor(ProgressBarStatusType status)
        {
            progressDeployment.SetState(status);
        }
        public string ProgressLabel
        {
            get
            {
                return lblProgress.Text;
            }
            set
            {
                lblProgress.Text = value;
            }
        }

        public bool EnableBack
        {
            get
            {
                return btnBack.Enabled;
            }
            set
            {
                btnBack.Enabled = value;
            }
        }

        public void AddProgressSteps(int count)
        {
            if (count > 0)
            {
                var previousState = progressDeployment.GetState();
                progressDeployment.SetState(ProgressBarStatusType.Normal);
                progressDeployment.Value = count;
                progressDeployment.SetState(previousState);
            };
        }

        public void StopProgressBar()
        {
            progressDeployment.Value = progressDeployment.Maximum;
        }

        public void SetProgressTotal(int total)
        {
            progressDeployment.Maximum = total;
        }

        public bool EnableCancel
        {
            get
            {
                return btnCancelDeployment.Enabled;
            }
            set
            {
                btnCancelDeployment.Enabled = value;
            }
        }

        public void Cleanup()
        {
            ProgressColor(ProgressBarStatusType.Normal);
            progressDeployment.Maximum = 0;
            progressDeployment.Value = 0;
            ProgressLabel = MessageStrings.ProgressBegin;
            rtbOutputConsole.Text = string.Empty;
            EnableCancel = true;
            EnableBack = false;
        }

        public string DeploymentLink { get; set; }

        private void btnViewDeployment_Click(object sender, EventArgs e)
        {
            Process.Start(ControlHelper.GetDefaultWebBrowser(), DeploymentLink);
        }

        public Control ViewDeployment
        {
            get { return btnViewDeployment; }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ((frmOctopusDeploymentManagerPlus)Owner).CancelCurrentTaskWatcher();
            ((frmOctopusDeploymentManagerPlus)Owner).Location = this.Location;
            ((frmOctopusDeploymentManagerPlus)Owner).Show();
            Hide();
        }

        private void frmOctopusDeploymentManagerStatus_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(UiStrings.ConfirmCancelation, UiStrings.ConfirmCancelationMessageBox, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                btnCancelDeployment.Enabled = false;
                ((frmOctopusDeploymentManagerPlus)Owner).CancelDeployment();
            }
        }

        private void frmOctopusDeploymentManagerStatus_Shown(object sender, EventArgs e)
        {
            IsClosedWithActive = false;
        }

        private void frmOctopusDeploymentManagerStatus_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (((frmOctopusDeploymentManagerPlus)Owner).logger.CancellationStatus == CancellationStatus.CancellationRequested)
            {
                if (!IsClosedWithActive)
                {
                    Log.Debug("Exit with Active Cancellation Initiated");
                    var closewidow = MessageBox.Show(MessageStrings.CancellationPendingMessageClose, MessageStrings.CancellationPendingTitleClose, MessageBoxButtons.OKCancel);
                    if (closewidow == DialogResult.OK)
                    {
                        IsClosedWithActive = true;
                        this.Hide();
                        while (((frmOctopusDeploymentManagerPlus)Owner).logger.Status != TaskManagerStatus.Canceled)
                        {
                            Thread.Sleep(3000);
                        }
                        Log.Debug("Exit");
                        Application.Exit();
                    }
                    else
                    {
                        Log.Debug("Exit Cancelled");
                        e.Cancel = true;
                        IsClosedWithActive = false;
                    }
                }
            }
            else if (((frmOctopusDeploymentManagerPlus)Owner).logger.Status == TaskManagerStatus.Executing || ((frmOctopusDeploymentManagerPlus)Owner).logger.Status == TaskManagerStatus.Interrupted || ((frmOctopusDeploymentManagerPlus)Owner).logger.Status == TaskManagerStatus.Queued || ((frmOctopusDeploymentManagerPlus)Owner).logger.Status == TaskManagerStatus.Intervention)
            {
                if (!IsClosedWithActive)
                {
                    Log.Debug("Exit with Active Deployment Initiated");
                    var closewidow = MessageBox.Show(MessageStrings.ActiveDeploymentMessageClose, MessageStrings.ActiveDeploymentTitleClose, MessageBoxButtons.OKCancel);
                    if (closewidow == DialogResult.OK)
                    {
                        IsClosedWithActive = true;
                        Log.Debug("Exit");
                        Application.Exit();
                    }
                    else
                    {
                        Log.Debug("Exit Cancelled");
                        e.Cancel = true;
                        IsClosedWithActive = false;
                    }
                }
            }
            else
            {
                Log.Debug("Exit");
                Application.Exit();
            }
        }
    }
}
