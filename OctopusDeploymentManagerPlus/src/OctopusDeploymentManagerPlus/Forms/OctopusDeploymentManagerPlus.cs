using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonMark;
using log4net;
using Octopus.Client;
using Octopus.Client.Model;
using OctopusDeploymentManagerPlus.Constants;
using OctopusDeploymentManagerPlus.Enums;
using OctopusDeploymentManagerPlus.ExtensionMethods;
using OctopusDeploymentManagerPlus.Forms.Prompts;
using OctopusDeploymentManagerPlus.Helpers;
using OctopusDeploymentManagerPlus.Models;
using OctopusDeploymentManagerPlus.Models.Interfaces;
using OctopusDeploymentManagerPlus.Workers;
using OctopusHelpers;
using OctopusHelpers.Enums;
using OctopusHelpers.Models;

namespace OctopusDeploymentManagerPlus.Forms
{
    public partial class frmOctopusDeploymentManagerPlus : Form, IOctopusDeploySettings
    {
        #region PrivateVars
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static Dictionary<string, string> Errors;
        private static readonly frmOctopusDeploymentManagerPlusStatus statusOctopusDeploymentForm = new frmOctopusDeploymentManagerPlusStatus();
        private static readonly frmInterruptionManager interruptionOctopusDeploymentForm = new frmInterruptionManager();
        private static readonly frmVersionSelector releaseOctopusDeploymentForm = new frmVersionSelector();
        internal OctopusDeploymentTaskManager logger = new OctopusDeploymentTaskManager();
        private AutoCompleteStringCollection projectSourceProjects = new AutoCompleteStringCollection();
        private AutoCompleteStringCollection projectSource = new AutoCompleteStringCollection();
        #endregion PrivateVars

        #region PubicVars
        public List<DeploymentTypeInfo> DeploymentTypes { get; set; }
        public List<EnvironmentResource> EnvironmentTypeList { get; set; }
        public static CancellationTokenSource CancelSource { get; set; }
        public OctopusRepository Repository { get; set; }
        public string ProjectName
        {
            get
            {
                return txtProjectName.Text;
            }
            set
            {
                txtProjectName.Text = value;
            }
        }
        public DeploymentTypeInfo DeploymentTypeForDeploy
        {
            get
            {
                return (DeploymentTypeInfo)cmbDeploymentType.SelectedValue;
            }
            set
            {
                cmbDeploymentType.SelectedValue = value;
            }
        }
        public EnvironmentResource EnvironmentTypeForDeploy
        {
            get
            {
                return (EnvironmentResource)cmbEnvironment.SelectedValue;
            }
            set
            {
                cmbEnvironment.SelectedValue = value;
            }
        }
        public DateTimeOffset DeploymentTime
        {
            get
            {
                if (rdbLater.Checked)
                {
                    return new DateTimeOffset(dtpDeploymentDate.Value);
                }
                else
                {
                    return new DateTimeOffset();
                }
            }
            set
            {
                rdbLater.Checked = true;
                dtpDeploymentDate.Value = value.DateTime;
            }
        }
        public ReleaseResource ReleaseToDeploy { get; set; }

        public string OctopusUserName { get; set; }
        #endregion PubicVars

        public frmOctopusDeploymentManagerPlus() { }

        public void LoadForm()
        {
            InitializeComponent();
            cmbDeploymentType.BindDeploymentTypeListToComboBox(DeploymentTypes);
            cmbDeploymentType.SelectedValueChanged += new EventHandler(cmbDeploymentType_SelectedValueChanged);
            cmbEnvironment.BindEnvironmentListToComboBox(EnvironmentTypeList);
            hlbDeploymentTypeText.Text = CommonMarkConverter.Convert(DeploymentTypeForDeploy.Description);
            dtpDeploymentDate.MinDate = DateTime.Now;
            dtpDeploymentTime.MinDate = DateTime.Now.AddMinutes(30);
            dtpDeploymentDate.MaxDate = DateTime.Now.AddDays(14);
            dtpDeploymentDate.Value = DateTime.Now;
            dtpDeploymentTime.Value = DateTime.Now.AddMinutes(30);
            txtProjectName.Focus();
            statusOctopusDeploymentForm.Owner = this;
            interruptionOctopusDeploymentForm.Owner = this;
            releaseOctopusDeploymentForm.Owner = this;
        }

        #region Methods
        public void CancelCurrentTaskWatcher()
        {
            CancelSource.Cancel();
        }

        public void CancelDeployment()
        {
            logger.RequestCancellation();
        }

        public bool ValidateForm()
        {
            Errors = new Dictionary<string, string>();
            var octopusResorucesManager = new OctopusResource((IOctopusDeploySettings)this);
            octopusResorucesManager.SetUserName(OctopusUserName);
            if (string.IsNullOrWhiteSpace(ProjectName))
            {
                Errors.Add("txtProjectName", string.Format(ErrorStrings.RequiredFieldFormat, lblProjectName.Text.Trim(':')));
            }
            else
            {
                if (!octopusResorucesManager.ValidateProjectName())
                {
                    Errors.Add("txtCompanyCode", ErrorStrings.InvalidProjectName);
                }
                else
                {
                    if (!octopusResorucesManager.ValidateClientProjectAccess())
                    {
                        Errors.Add("txtCompanyCode", ErrorStrings.NonActiveProjectName);
                    }
                    if (DeploymentTypeForDeploy.PreviousDeployCheck)
                    {
                        if (!octopusResorucesManager.ValidateLastRelease())
                        {
                            Errors.Add("cmbDeploymentType", MessageStrings.NoPreviousDeployments);
                            Errors.Add("cmbEnvironment", MessageStrings.NoPreviousDeployments);
                        }
                    }
                }
            }
            if (rdbLater.Checked && DeploymentTime < DateTimeOffset.Now)
            {
                Errors.Add("dtpDeploymentTime", MessageStrings.InvalidDeploymentDate);
            }
            return (Errors.Count == 0);
        }

        public async Task Deploy()
        {
            CancelSource = new CancellationTokenSource();
            var token = CancelSource.Token;
            var octopusResource = new OctopusResource((IOctopusDeploySettings)this);
            var builtDeployment = new DeploymentResource();
            var clientProject = octopusResource.GetClientProject();
            var deploymentEnvironment = EnvironmentTypeForDeploy;
            var deploymentRelease = ReleaseToDeploy;
            var clientProjectSteps = StepHelper.GetReleaseEnvironmentDeploymentSteps(Repository, deploymentRelease, deploymentEnvironment).ToList();
            var deploymentTypeSteps = OctopusStepHelper.GetDeploymentTypeSteps((IOctopusDeploySettings)this);

            var skippedSteps = StepHelper.GetStepsToSkipFromProjects(clientProjectSteps, deploymentTypeSteps);
            if (DeploymentTime == new DateTimeOffset())
            {
                builtDeployment = DeploymentHelper.BuildDeployment(Repository, deploymentRelease, deploymentEnvironment, DeploymentTypeForDeploy.DeploymentType.Name, null, DeploymentTypeForDeploy.GuidedFailure, skippedSteps, null);
            }
            else
            {
                builtDeployment = DeploymentHelper.BuildDeployment(Repository, deploymentRelease, deploymentEnvironment, DeploymentTypeForDeploy.DeploymentType.Name, null, DeploymentTypeForDeploy.GuidedFailure, skippedSteps, DeploymentTime);
            }

            var progress = new Progress<Dictionary<ProgressType, string>>(value =>
            {
                if (!statusOctopusDeploymentForm.IsClosedWithActive)
                {
                    if (value.ContainsKey(ProgressType.Size))
                    {
                        statusOctopusDeploymentForm.SetProgressTotal(int.Parse(value[ProgressType.Size]));
                    }
                    if (value.ContainsKey(ProgressType.StepCount))
                    {
                        statusOctopusDeploymentForm.AddProgressSteps(int.Parse(value[ProgressType.StepCount]));
                    }
                    if (value.ContainsKey(ProgressType.Name) && !string.IsNullOrWhiteSpace(value[ProgressType.Name]))
                    {
                        if (value[ProgressType.Name].Equals(MessageStrings.PendingIntervention))
                        {
                            statusOctopusDeploymentForm.ProgressLabel = MessageStrings.PendingIntervention;
                        }
                        else if (value[ProgressType.Name].Equals(MessageStrings.InterventionResolved))
                        {
                            Log.Warn(string.Format(MessageStrings.GuidingIntervenedDeployment, logger.GetManualInterventionGuidence()));
                            Log.Warn(string.Format(MessageStrings.GuidingDeploymentChoice, logger.GetManualInterventionGuidence(), logger.GetManualInterventionResponsibleUser().Username));
                            Log.Warn(logger.GetManualInterventionNote());
                        }
                        else if (value[ProgressType.Name].Equals(MessageStrings.PendingInterruption))
                        {
                            statusOctopusDeploymentForm.ProgressLabel = MessageStrings.PendingInterruption;
                            interruptionOctopusDeploymentForm.Cleanup();
                            interruptionOctopusDeploymentForm.Error = value[ProgressType.Error];
                            statusOctopusDeploymentForm.Enabled = false;
                            interruptionOctopusDeploymentForm.Show();
                            interruptionOctopusDeploymentForm.Focus();
                            interruptionOctopusDeploymentForm.BringToFront();
                        }
                        else if (value[ProgressType.Name].Equals(MessageStrings.InterruptionResolved))
                        {
                            statusOctopusDeploymentForm.Enabled = true;
                            interruptionOctopusDeploymentForm.Hide();
                            statusOctopusDeploymentForm.Focus();
                            statusOctopusDeploymentForm.BringToFront();
                            Log.Warn(string.Format(MessageStrings.GuidingInterruptedDeployment, logger.GetManagedInterruptionGuidence()));
                            Log.Warn(string.Format(MessageStrings.GuidingDeploymentChoice, logger.GetManagedInterruptionGuidence(), logger.GetManagedInterruptionResponsibleUser().Username));
                            Log.Warn(logger.GetManagedInterruptionNote());

                        }
                        else if (value[ProgressType.Name].Equals(MessageStrings.ProgressComplete))
                        {
                            statusOctopusDeploymentForm.ProgressLabel = string.Format(value[ProgressType.Name], DeploymentTypeForDeploy.Label, deploymentRelease.Version, clientProject.Name, deploymentEnvironment.Name);
                            Log.Info(string.Format(value[ProgressType.Name], DeploymentTypeForDeploy.Label, deploymentRelease.Version, clientProject.Name, deploymentEnvironment.Name));
                            statusOctopusDeploymentForm.EnableBack = true;
                            statusOctopusDeploymentForm.EnableCancel = false;
                        }
                        else if (value[ProgressType.Name].Equals(MessageStrings.DeploymentCancelled))
                        {
                            statusOctopusDeploymentForm.Enabled = true;
                            interruptionOctopusDeploymentForm.Hide();
                            statusOctopusDeploymentForm.Focus();
                            statusOctopusDeploymentForm.BringToFront();
                            statusOctopusDeploymentForm.ProgressLabel = string.Format(value[ProgressType.Name], DeploymentTypeForDeploy.Label, ProjectName);
                            Log.Error(string.Format(MessageStrings.DeploymentProgressCancelled, DeploymentTypeForDeploy.Label, ProjectName, logger.GetCancellingUser().Username));
                            statusOctopusDeploymentForm.StopProgressBar();
                            statusOctopusDeploymentForm.EnableBack = true;
                            statusOctopusDeploymentForm.EnableCancel = false;

                        }
                        else if (value[ProgressType.Name].Equals(MessageStrings.ProgressWarning))
                        {
                            statusOctopusDeploymentForm.Focus();
                            statusOctopusDeploymentForm.BringToFront();
                            statusOctopusDeploymentForm.ProgressLabel = string.Format(value[ProgressType.Name], DeploymentTypeForDeploy.Label, deploymentRelease.Version, clientProject.Name, deploymentEnvironment.Name);
                            Log.Info(string.Format(DeploymentTypeForDeploy.Label, deploymentRelease.Version, clientProject.Name, deploymentEnvironment.Name));
                            statusOctopusDeploymentForm.StopProgressBar();
                            statusOctopusDeploymentForm.EnableBack = true;
                            statusOctopusDeploymentForm.EnableCancel = false;

                        }
                        else if (value[ProgressType.Name].Equals(MessageStrings.ProgressFailed))
                        {
                            statusOctopusDeploymentForm.Focus();
                            statusOctopusDeploymentForm.BringToFront();
                            statusOctopusDeploymentForm.ProgressLabel = string.Format(value[ProgressType.Name], DeploymentTypeForDeploy.Label, deploymentRelease.Version, clientProject.Name, deploymentEnvironment.Name);
                            Log.Info(string.Format(DeploymentTypeForDeploy.Label, deploymentRelease.Version, clientProject.Name, deploymentEnvironment.Name));
                            statusOctopusDeploymentForm.StopProgressBar();
                            statusOctopusDeploymentForm.EnableBack = true;
                            statusOctopusDeploymentForm.EnableCancel = false;

                        }
                        else
                        {
                            statusOctopusDeploymentForm.ProgressLabel = string.Format(UiStrings.ProgressFormat, value[ProgressType.Name]);
                        }
                    }
                    if (value.ContainsKey(ProgressType.Output) && !string.IsNullOrWhiteSpace(value[ProgressType.Output]))
                    {
                        Log.Info(value[ProgressType.Output]);
                    }
                    if (value.ContainsKey(ProgressType.Error))
                    {
                        Log.Error(value[ProgressType.Error]);
                    }
                    if (value.ContainsKey(ProgressType.Warning))
                    {
                        Log.Warn(value[ProgressType.Warning]);
                    }
                    if (value.ContainsKey(ProgressType.BarStatus))
                    {
                        statusOctopusDeploymentForm.ProgressColor((ProgressBarStatusType)Enum.Parse(typeof(ProgressBarStatusType), value[ProgressType.BarStatus]));
                    }
                }
            });

            statusOctopusDeploymentForm.Cleanup();
            Log.Info(string.Format(MessageStrings.ProgressBegin, DeploymentTypeForDeploy.Label, deploymentRelease.Version, clientProject.Name, deploymentEnvironment.Name));
            statusOctopusDeploymentForm.ProgressLabel = string.Format(MessageStrings.ProgressBegin, DeploymentTypeForDeploy.Label, deploymentRelease.Version, clientProject.Name, deploymentEnvironment.Name);
            logger = new OctopusDeploymentTaskManager(Repository, builtDeployment);
            Log.Debug("Starting Deployment");
            logger.StartDeploy();
            var octopusServerApi = ConfigurationManager.AppSettings[ResourceStrings.OctopusApiUrlAppSettingKey];
            var deploymentLink = string.Format(ResourceStrings.ReplacedPrinting, octopusServerApi, logger.GetDeploymentLink());
            var taskView = new TaskWatcher((IOctopusDeploySettings)this);
            statusOctopusDeploymentForm.DeploymentLink = deploymentLink;
            statusOctopusDeploymentForm.ViewDeployment.Visible = true;
            interruptionOctopusDeploymentForm.DeploymentLink = deploymentLink;
            Hide();
            statusOctopusDeploymentForm.Show();
            statusOctopusDeploymentForm.Focus();
            statusOctopusDeploymentForm.BringToFront();
            Cursor.Current = Cursors.Default;
            await taskView.TaskReport(progress, logger, token);
        }

        public void RetryInterruptedDeploy()
        {
            logger.RespondToInterruption(InterruptionResponse.Retry, interruptionOctopusDeploymentForm.NoteText);
        }

        public void FailInterruptedDeploy()
        {
            logger.RespondToInterruption(InterruptionResponse.Fail, interruptionOctopusDeploymentForm.NoteText);
            statusOctopusDeploymentForm.ProgressColor(ProgressBarStatusType.Error);
        }

        public bool VersionPrompt()
        {
            var octopusResource = new OctopusResource((IOctopusDeploySettings)this);
            var version = string.Empty;
            var listReleaseResource = octopusResource.GetDeployableReleases();
            releaseOctopusDeploymentForm.ReleaseList = listReleaseResource;
            releaseOctopusDeploymentForm.MultiRelease = (listReleaseResource.Count > 1);
            releaseOctopusDeploymentForm.PrepFrom();
            releaseOctopusDeploymentForm.ShowDialog();
            if (releaseOctopusDeploymentForm.CorrectRelease)
            {
                ReleaseToDeploy = releaseOctopusDeploymentForm.ChosenRelease;
            }
            return releaseOctopusDeploymentForm.CorrectRelease;
        }

        public void LoadAutoComplete()
        {
            projectSource = new AutoCompleteStringCollection();
            projectSourceProjects = new AutoCompleteStringCollection();
            var octopusResorucesManager = new OctopusResource((IOctopusDeploySettings)this);
            octopusResorucesManager.SetUserName(OctopusUserName);
            projectSourceProjects.AddRange(octopusResorucesManager.GetDeployableClientsProjects());
            txtProjectName.AutoCompleteCustomSource = projectSourceProjects;
        }
        #endregion Methods

        #region Events
        private void cmbDeploymentType_SelectedValueChanged(object sender, EventArgs e)
        {
            hlbDeploymentTypeText.Text = CommonMarkConverter.Convert(DeploymentTypeForDeploy.Description);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDeploy_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            errorProvider.Clear();
            Log.Debug("Form Validation");
            if (!ValidateForm())
            {
                foreach (var control in ControlHelper.GetAllTextBoxControls(this))
                {
                    errorProvider.SetError(control, string.Empty);
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
                Cursor.Current = Cursors.Default;
                return;
            }
            Log.Debug("Version Prompt");
            if (VersionPrompt())
            {
                Log.Debug("Deploy Initiated");
                Cursor.Current = Cursors.WaitCursor;
                Deploy();
            }
            else
            {
                BringToFront();
            }
        }

        private void rdbLater_Click(object sender, EventArgs e)
        {
            rdbNow.Checked = false;
            rdbLater.Checked = true;
            dtpDeploymentTime.Enabled = true;
            dtpDeploymentDate.Enabled = true;
        }

        private void rdbNow_Click(object sender, EventArgs e)
        {
            rdbNow.Checked = true;
            rdbLater.Checked = false;
            dtpDeploymentTime.Enabled = false;
            dtpDeploymentDate.Enabled = false;
        }

        private void dtpDeploymentDate_ValueChanged(object sender, EventArgs e)
        {
            var deploymentDateTime = dtpDeploymentDate.Value.Date;
            var deploymentTimeSpan = dtpDeploymentTime.Value.TimeOfDay;
            var futureDeployment = deploymentDateTime.Subtract(deploymentDateTime.TimeOfDay);
            futureDeployment = futureDeployment.Add(deploymentTimeSpan);
            if (futureDeployment.CompareTo(dtpDeploymentDate.MinDate) >= 0)
            {
                dtpDeploymentDate.Value = futureDeployment;
                dtpDeploymentTime.Value = futureDeployment;
            }
            else
            {
                dtpDeploymentTime.MinDate = DateTime.Now.AddMinutes(30);
                dtpDeploymentTime.Value = dtpDeploymentTime.MinDate;
            }
        }

        private void dtpDeploymentTime_ValueChanged(object sender, EventArgs e)
        {
            var deploymentDateTime = dtpDeploymentDate.Value.Date;
            var deploymentTimeSpan = dtpDeploymentTime.Value.TimeOfDay;
            var futureDeployment = deploymentDateTime.Subtract(deploymentDateTime.TimeOfDay);
            futureDeployment = futureDeployment.Add(deploymentTimeSpan);
            if (futureDeployment.CompareTo(dtpDeploymentDate.MinDate) >= 0)
            {
                dtpDeploymentDate.Value = futureDeployment;
                dtpDeploymentTime.Value = futureDeployment;
            }
            else
            {
                dtpDeploymentTime.MinDate = DateTime.Now.AddMinutes(30);
                dtpDeploymentTime.Value = dtpDeploymentTime.MinDate;
            }
        }

        private void txtProjectName_Leave(object sender, EventArgs e)
        {
            errorProvider.SetError(txtProjectName, string.Empty);
            var octopusResorucesManager = new OctopusResource((IOctopusDeploySettings)this);
            if (!string.IsNullOrWhiteSpace(ProjectName))
            {
                if (!projectSourceProjects.Contains(ProjectName))
                {
                    errorProvider.SetIconPadding(txtProjectName, -20);
                    errorProvider.SetError(txtProjectName, ErrorStrings.NonActiveProjectName);
                }
            }
        }

        private void frmOctopusDeploymentManager_Shown(object sender, EventArgs e)
        {
            LoadAutoComplete();
        }
        #endregion Events
    }
}
