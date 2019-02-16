using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CommandLine;
using HybridScaffolding;
using log4net;
using Octopus.Client.Model;
using OctopusHelpers;
using OctopusHelpers.Enums;
using OctopusHelpers.Models;
using OctopusDeploymentManagerPlus.Constants;
using OctopusDeploymentManagerPlus.Enums;
using OctopusDeploymentManagerPlus.Forms;
using OctopusDeploymentManagerPlus.Helpers;
using OctopusDeploymentManagerPlus.Models.CommandLine;
using OctopusDeploymentManagerPlus.Models.Interfaces;
using OctopusDeploymentManagerPlus.Workers;


namespace OctopusDeploymentManagerPlus.Models
{
    public class OctopusDeploymentManagerPlusScaffold : HybridScaffold
    {
        private static OctopusConnection AdminConnection = new OctopusConnection();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static bool SuccesfullAdminLogin = false;

        public override string[] PreConsoleExec(string[] arguments, RunTypes runType)
        {
            return base.PreConsoleExec(arguments, runType);
        }

        public override void ConsoleMain(string[] arguments, RunTypes runType)
        {
            var configFileErrors = ConfigurationValidation.GeneralConfigValidation(runType);
            if (configFileErrors.Count > 0)
            {
                Log.Error(string.Join(Environment.NewLine, configFileErrors));
            }
            else
            {

                var verbCommand = new VerbCommands();
                string invokedVerb = string.Empty; ;
                object invokedVerbInstance = new object();

                if (!Parser.Default.ParseArguments(arguments, verbCommand, (verb, subObtions) =>
                {
                    invokedVerb = verb;
                    invokedVerbInstance = subObtions;
                }))
                {
                    Environment.Exit(Parser.DefaultExitCodeFail);
                }
                else
                {
                    VerbCommandType parsedVerb;
                    Enum.TryParse(invokedVerb, true, out parsedVerb);
                    switch (parsedVerb)
                    {
                        case VerbCommandType.Deploy:
                            var deployCommands = (DeployCommands)invokedVerbInstance;
                            deployCommands.ProcessDeploymentArguments();
                            if (new OctopusConnection().Validate(deployCommands.Repository))
                            {
                                var deploymentRelease = new ReleaseResource();

                                if (deployCommands.DeploymentTypeForDeploy == null || deployCommands.EnvironmentTypeForDeploy == null || deployCommands.ProjectForDeploy == null || !string.IsNullOrWhiteSpace(deployCommands.QueueDateTime))
                                {
                                    if (deployCommands.ProjectForDeploy == null)
                                    {
                                        Log.Error("Unable to Locate the following Client: " + deployCommands.ProjectName);
                                    }
                                    if (deployCommands.EnvironmentTypeForDeploy == null)
                                    {
                                        Log.Error("Unable to Locate the following EnvironmentType: " + deployCommands.EnvironmentType);
                                    }
                                    if (deployCommands.DeploymentTypeForDeploy == null)
                                    {
                                        Log.Error("Unable to Locate the following DeploymentType: " + deployCommands.DeploymentType);
                                    }
                                    else
                                    {
                                        deploymentRelease = new OctopusResource((IOctopusDeploySettings)deployCommands, deployCommands.Release).GetDeployableRelease();
                                        if (deploymentRelease == null)
                                        {
                                            Log.Error("Unable to Locate Deployable release for the following Client: " + deployCommands.ProjectName);
                                        }
                                    }
                                    if (!string.IsNullOrWhiteSpace(deployCommands.QueueDateTime) && deployCommands.QueuedDateTimeForDeploy == new DateTimeOffset())
                                    {
                                        Log.Error("Unable to Parse Queued Date Time: " + deployCommands.QueueDateTime);
                                    }
                                }
                                else
                                {
                                    deploymentRelease = new OctopusResource((IOctopusDeploySettings)deployCommands, deployCommands.Release).GetDeployableRelease();
                                    try
                                    {

                                        var builtDeployment = new DeploymentResource();
                                        var deploymentTypeName = deployCommands.DeploymentTypeForDeploy.DeploymentType.Name;
                                        var clientProject = ProjectHelper.GetProjectByName(deployCommands.Repository, deployCommands.ProjectName);
                                        var deploymentEnvironment = deployCommands.EnvironmentTypeForDeploy;
                                        var clientProjectSteps = StepHelper.GetReleaseEnvironmentDeploymentSteps(deployCommands.Repository, deploymentRelease, deploymentEnvironment).ToList();
                                        var deploymentTypeSteps = OctopusStepHelper.GetDeploymentTypeSteps((IOctopusDeploySettings)deployCommands);
                                        var skippedSteps = StepHelper.GetStepsToSkipFromProjects(clientProjectSteps, deploymentTypeSteps);
                                        if (!string.IsNullOrWhiteSpace(deployCommands.QueueDateTime) && deployCommands.QueuedDateTimeForDeploy != new DateTimeOffset())
                                        {
                                            builtDeployment = DeploymentHelper.BuildDeployment(deployCommands.Repository, deploymentRelease, deploymentEnvironment, deploymentTypeName, null, deployCommands.DeploymentTypeForDeploy.GuidedFailure, skippedSteps, deployCommands.QueuedDateTimeForDeploy);
                                        }
                                        else
                                        {
                                            builtDeployment = DeploymentHelper.BuildDeployment(deployCommands.Repository, deploymentRelease, deploymentEnvironment, deploymentTypeName, null, deployCommands.DeploymentTypeForDeploy.GuidedFailure, skippedSteps, null);
                                        }
                                        var odmTask = new OctopusDeploymentTaskManager(deployCommands.Repository, builtDeployment);
                                        Log.Info("Deploying [" + deploymentRelease.Version + "]:[" + deployCommands.DeploymentType + "] for [" + deployCommands.ProjectName + "]:[" + deployCommands.EnvironmentType + "]");
                                        odmTask.StartDeploy();
                                        if (odmTask.Status == TaskManagerStatus.Queued)
                                        {
                                            Log.Warn("The deployment is currently queued behind " + odmTask.GetQueuedDeploymentCount().ToString() + " other tasks");
                                        }
                                        Log.Info("DeplpymentLink: " + odmTask.GetDeploymentLink());
                                    }
                                    catch (Exception ex)
                                    {
                                        Log.Error(ex.Message);
                                    }
                                }
                            }
                            else
                            {
                                Log.Error("The supplied Api Key is invalid.");
                            }
                            break;
                        case VerbCommandType.EncryptKey:
                            var encryptKeyCommands = (EncryptKeyCommands)invokedVerbInstance;
                            var here = KeyGenerator.KeyGeneration(encryptKeyCommands.AdminApiKey, encryptKeyCommands.KeyOutputDirectory);
                            Log.Info("output to " + here);
                            break;
                    }
                }
            }
            base.ConsoleMain(arguments, runType);
        }

        public override Form PreGuiExec(Form mainForm)
        {
            frmOctopusDeploymentManagerPlusLoading.ShowLoadingForm();
            var openLoadingForm = true;
            var octopusDeploymentForm = new frmOctopusDeploymentManagerPlus();
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            try
            {
                if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ResourceStrings.KeyFileName)))
                {
                    throw new Exception("Missing KeyFile in executable directory");
                }
                var configFileErrors = ConfigurationValidation.GeneralConfigValidation(RunTypes.Gui);
                if(configFileErrors.Count > 0)
                {
                    throw new Exception(string.Join(Environment.NewLine, configFileErrors));
                }
                AdminConnection.GuiConfigure();
                SuccesfullAdminLogin = AdminConnection.ValidateAdminConnection();
                if (!SuccesfullAdminLogin)
                {
                    throw new Exception("Incorrect AdminApiKey supplied.");
                }
                var userName = ConfigurationManager.AppSettings[ResourceStrings.UserNameAppSettingKey];
           
                Log.Debug("Generating User API Key");
                AdminConnection.OctopusUserName = userName;
                if(!AdminConnection.ValidateUser())
                {
                    var userNamePrompt = new frmUserNamePrompt();
                    userNamePrompt.EnteredUserName = userName;
                    userNamePrompt.PrepForm(AdminConnection);

                    if (userNamePrompt.ShowDialog() == DialogResult.Cancel)
                    {
                        throw new Exception("Invalid UserName or No UserName Entered");
                    }
                    else
                    {
                        userName = userNamePrompt.EnteredUserName;
                        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        configuration.AppSettings.Settings[ResourceStrings.UserNameAppSettingKey].Value = userName;
                        configuration.Save(ConfigurationSaveMode.Full, true);
                        ConfigurationManager.RefreshSection("appSettings");
                        AdminConnection.OctopusUserName = userName;
                    }
                }
                octopusDeploymentForm.OctopusUserName = userName;
                AdminConnection.GenerateUserApiKey(userName);
                Log.Debug("GeneratiedUser API Key");
                if (AdminConnection.Validate())
                {
                    var octopusConfigurationErrors = ConfigurationValidation.GeneralOctopusValidation(AdminConnection.AdminConnection());
                    if (octopusConfigurationErrors.Count > 0)
                    {
                        throw new Exception(string.Join(Environment.NewLine, octopusConfigurationErrors));
                    }
                    Log.Debug("Valid Key");
                    var userconnection = AdminConnection.UserConnection();
                    Log.Debug("Connection Created");
                    octopusDeploymentForm.Repository = userconnection;
                    Log.Debug("Gathering Deployment Types");
                    octopusDeploymentForm.DeploymentTypes = AdminConnection.GetDeploymentTypes();
                    Log.Debug("Gathering Environment Types");
                    octopusDeploymentForm.EnvironmentTypeList = AdminConnection.GetEnvironmentTypes();
                    Log.Debug("Loading Form");
                    octopusDeploymentForm.LoadForm();
                    frmOctopusDeploymentManagerPlusLoading.CloseForm();
                    openLoadingForm = false;
                    Log.Debug("Application Run");
                    return base.PreGuiExec(octopusDeploymentForm);
                }
                else
                {
                    Log.Debug("Invalid Key");
                    octopusDeploymentForm.errorProvider.SetError(octopusDeploymentForm.grpDeploymentInformation, string.Format(UiStrings.ErrorRetrievingApiKey, AdminConnection.OctopusUserName));
                    octopusDeploymentForm.errorProvider.SetIconPadding(octopusDeploymentForm.grpDeploymentInformation, -20);
                    octopusDeploymentForm.Enabled = false;
                    frmOctopusDeploymentManagerPlusLoading.CloseForm();
                    openLoadingForm = false;
                    octopusDeploymentForm.Enabled = false;
                    MessageBox.Show(string.Format(UiStrings.ErrorRetrievingApiKeyMessageBox, AdminConnection.OctopusUserName), UiStrings.ErrorCaptionText, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return base.PreGuiExec(octopusDeploymentForm);
                }
            }
            catch (Exception ex)
            {
                var errorString = string.Format(UiStrings.ErrorMessage, ex.Message, string.Empty);
                if (openLoadingForm)
                {
                    frmOctopusDeploymentManagerPlusLoading.CloseForm();
                }
                if (ex.InnerException != null)
                {
                    errorString = string.Format(UiStrings.ErrorMessage, ex.Message, string.Format(UiStrings.InnerException, ex.InnerException.Message));
                }
                Log.Fatal(errorString);
                MessageBox.Show(errorString, UiStrings.ErrorConnectingMessageBox, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                octopusDeploymentForm.Close();
                return base.PreGuiExec(octopusDeploymentForm);
            }
        }

        static void OnApplicationExit(object sender, EventArgs e)
        {
            if (SuccesfullAdminLogin)
            {
                Log.Debug("Revoking API Key");
                AdminConnection.RevokeUserApiKey();
            }
        }
    }
}
