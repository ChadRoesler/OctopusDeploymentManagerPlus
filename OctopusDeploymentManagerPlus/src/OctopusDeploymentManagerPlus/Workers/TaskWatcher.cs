using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OctopusDeploymentManagerPlus.Constants;
using OctopusDeploymentManagerPlus.Enums;
using OctopusDeploymentManagerPlus.Helpers;
using OctopusHelpers.Enums;
using OctopusHelpers.Models;
using OctopusDeploymentManagerPlus.Models.Interfaces;

namespace OctopusDeploymentManagerPlus.Workers
{
    internal class TaskWatcher
    {
        private readonly IOctopusDeploySettings sessionOctopusSettings;

        internal TaskWatcher(IOctopusDeploySettings octopusSettingsData)
        {
            sessionOctopusSettings = octopusSettingsData;
        }

        internal Task TaskReport(IProgress<Dictionary<ProgressType, string>> progress, OctopusDeploymentTaskManager deploymentTaskManager, CancellationToken cancelToken)
        {
            var previousDeploymentCount = 0;
            var requiredSteps = OctopusStepHelper.GetRequiredSteps(sessionOctopusSettings);
            var cancelationNotificationSent = false;
            return Task.Run(() =>
            {
                var progressDictionary = new Dictionary<ProgressType, string>();
                try
                {
                    while (deploymentTaskManager.Status != TaskManagerStatus.Completed && deploymentTaskManager.Status != TaskManagerStatus.Canceled && !cancelToken.IsCancellationRequested)
                    {
                        var progressUserCancellationDictionary = new Dictionary<ProgressType, string>();
                        var progressDictionaryBar = new Dictionary<ProgressType, string>();
                        progressDictionaryBar[ProgressType.Size] = deploymentTaskManager.GetStepCount().ToString();
                        progress.Report(progressDictionaryBar);
                        if (deploymentTaskManager.Status == TaskManagerStatus.Intervention)
                        {
                            var progressManualInterventionDictionary = new Dictionary<ProgressType, string>();
                            progressManualInterventionDictionary[ProgressType.Name] = MessageStrings.PendingIntervention;
                            progressManualInterventionDictionary[ProgressType.Output] = deploymentTaskManager.GetManualInterventionStepInfo();
                            progressManualInterventionDictionary[ProgressType.Warning] = string.Format(MessageStrings.InterventionAlert, deploymentTaskManager.GetManualInterventionStepInfo(), deploymentTaskManager.GetManualInterventionDirections());
                            progress.Report(progressManualInterventionDictionary);
                            while (deploymentTaskManager.Status == TaskManagerStatus.Intervention && deploymentTaskManager.Status != TaskManagerStatus.Canceled && !deploymentTaskManager.HasNewInterruption)
                            {
                                Thread.Sleep(1000);
                                if (deploymentTaskManager.CancellationStatus == CancellationStatus.CancellationRequested)
                                {
                                    progressUserCancellationDictionary[ProgressType.Warning] = MessageStrings.CancellingDeployment;
                                    progressUserCancellationDictionary[ProgressType.Name] = MessageStrings.CancellingDeployment;
                                    progress.Report(progressUserCancellationDictionary);
                                    deploymentTaskManager.CancelDeploy();
                                }
                            }
                            deploymentTaskManager.UpdateIntervention();
                            Thread.Sleep(1000);
                            if (deploymentTaskManager.Status != TaskManagerStatus.Canceled)
                            {
                                var progressManualInterventionGuideDictionary = new Dictionary<ProgressType, string>();

                                progressManualInterventionGuideDictionary[ProgressType.Name] = MessageStrings.InterventionResolved;
                                progress.Report(progressManualInterventionGuideDictionary);
                                progressDictionary[ProgressType.Output] = deploymentTaskManager.GetLastStepExecuted();
                                progressDictionary[ProgressType.Name] = deploymentTaskManager.GetLastStepExecuted();
                                progress.Report(progressDictionary);
                            }
                        }
                        else if (deploymentTaskManager.Status == TaskManagerStatus.Interrupted)
                        {
                            var progressInterruptionDictionary = new Dictionary<ProgressType, string>();

                            progressInterruptionDictionary[ProgressType.Name] = MessageStrings.PendingInterruption;
                            progressInterruptionDictionary[ProgressType.Error] = deploymentTaskManager.GetInterruptedStepInfo();
                            progress.Report(progressInterruptionDictionary);
                            while (deploymentTaskManager.Status == TaskManagerStatus.Interrupted && deploymentTaskManager.Status != TaskManagerStatus.Canceled && !deploymentTaskManager.HasNewInterruption)
                            {
                                Thread.Sleep(1000);
                            }
                            deploymentTaskManager.UpdateInterruption();
                            Thread.Sleep(1000);
                            if (deploymentTaskManager.Status != TaskManagerStatus.Canceled)
                            {
                                var progressInterruptionGuideDictionary = new Dictionary<ProgressType, string>();

                                progressInterruptionGuideDictionary[ProgressType.Name] = MessageStrings.InterruptionResolved;
                                progress.Report(progressInterruptionGuideDictionary);
                                progressDictionary[ProgressType.Output] = deploymentTaskManager.GetLastStepExecuted();
                                progressDictionary[ProgressType.Name] = deploymentTaskManager.GetLastStepExecuted();
                                progress.Report(progressDictionary);
                            }
                        }
                        else if (deploymentTaskManager.Status == TaskManagerStatus.Queued)
                        {
                            if (deploymentTaskManager.CancellationStatus == CancellationStatus.CancellationRequested)
                            {
                                progressUserCancellationDictionary[ProgressType.Warning] = MessageStrings.CancellingDeployment;
                                progressUserCancellationDictionary[ProgressType.Name] = MessageStrings.CancellingDeployment;
                                progress.Report(progressUserCancellationDictionary);
                                deploymentTaskManager.CancelDeploy();
                            }
                            else
                            {
                                var currentDeploymentCount = deploymentTaskManager.GetQueuedDeploymentCount();
                                if (previousDeploymentCount != currentDeploymentCount && currentDeploymentCount != 0)
                                {
                                    var progressQueuedDictionary = new Dictionary<ProgressType, string>();

                                    progressQueuedDictionary[ProgressType.Name] = string.Format(MessageStrings.QueuedDeploys, currentDeploymentCount.ToString());
                                    progressQueuedDictionary[ProgressType.Output] = string.Format(MessageStrings.QueuedDeploys, currentDeploymentCount.ToString());
                                    progress.Report(progressQueuedDictionary);
                                    previousDeploymentCount = currentDeploymentCount;
                                }
                                Thread.Sleep(1000);
                            }
                        }
                        else
                        {

                            progressDictionary[ProgressType.StepCount] = deploymentTaskManager.GetStepCompletedCount().ToString();
                            progressDictionary[ProgressType.Output] = deploymentTaskManager.GetLog();

                            var lastStepRun = deploymentTaskManager.GetLastStepExecuted();

                            progressDictionary[ProgressType.Name] = lastStepRun;
                            progress.Report(progressDictionary);
                            if (deploymentTaskManager.CancellationStatus == CancellationStatus.CancellationRequested)
                            {

                                if (lastStepRun.Substring(0, 4).Equals(ResourceStrings.StepTrim))
                                {
                                    lastStepRun = lastStepRun.Substring(lastStepRun.IndexOf(":") + 2);
                                }
                                if (!cancelationNotificationSent)
                                {
                                    var progressUserCancelRequestDictionary = new Dictionary<ProgressType, string>();

                                    progressUserCancelRequestDictionary[ProgressType.Warning] = MessageStrings.CancellationRequested;
                                    progressUserCancelRequestDictionary[ProgressType.Name] = MessageStrings.CancellationRequested;
                                    progressUserCancelRequestDictionary[ProgressType.BarStatus] = ProgressBarStatusType.Warning.ToString();
                                    progress.Report(progressUserCancelRequestDictionary);
                                }
                                if (!string.IsNullOrWhiteSpace(lastStepRun) && !requiredSteps.Exists(x => x.ActionName.Equals(lastStepRun, StringComparison.OrdinalIgnoreCase)))
                                {
                                    var progressUserCancellationCompleteDictionary = new Dictionary<ProgressType, string>();

                                    progressUserCancellationCompleteDictionary[ProgressType.Warning] = MessageStrings.CancellingDeployment;
                                    progressUserCancellationCompleteDictionary[ProgressType.Name] = MessageStrings.CancellingDeployment;
                                    progressUserCancellationCompleteDictionary[ProgressType.BarStatus] = ProgressBarStatusType.Error.ToString();
                                    progress.Report(progressUserCancellationCompleteDictionary);
                                    deploymentTaskManager.CancelDeploy();
                                    cancelationNotificationSent = true;
                                }
                                if (!cancelationNotificationSent)
                                {
                                    progressUserCancellationDictionary[ProgressType.Warning] = MessageStrings.WaitingForRequiredSteps;
                                    progress.Report(progressUserCancellationDictionary);
                                    cancelationNotificationSent = true;
                                }
                            }
                        }
                    }
                    if (!cancelToken.IsCancellationRequested && deploymentTaskManager.Status != TaskManagerStatus.Canceled)
                    {
                        progressDictionary[ProgressType.StepCount] = deploymentTaskManager.GetStepCompletedCount().ToString();
                        progressDictionary[ProgressType.Output] = deploymentTaskManager.GetLog();
                        progressDictionary[ProgressType.Name] = deploymentTaskManager.GetLastStepExecuted();
                        progress.Report(progressDictionary);

                        var progressCompleteDictionary = new Dictionary<ProgressType, string>();

                        if (deploymentTaskManager.ErrorStatus != ErrorStatus.None)
                        {
                            if (deploymentTaskManager.ErrorStatus == ErrorStatus.Warnings)
                            {
                                progressCompleteDictionary[ProgressType.Name] = MessageStrings.ProgressWarning;
                                progressCompleteDictionary[ProgressType.Warning] = deploymentTaskManager.GetWarnings();
                                progressCompleteDictionary[ProgressType.BarStatus] = ProgressBarStatusType.Warning.ToString();
                                progressCompleteDictionary[ProgressType.StepCount] = deploymentTaskManager.GetStepCount().ToString();
                                progress.Report(progressCompleteDictionary);
                            }
                            if (deploymentTaskManager.ErrorStatus == ErrorStatus.Error)
                            {
                                progressCompleteDictionary[ProgressType.Name] = MessageStrings.ProgressFailed;
                                progressCompleteDictionary[ProgressType.Error] = deploymentTaskManager.GetErrors();
                                progressCompleteDictionary[ProgressType.BarStatus] = ProgressBarStatusType.Error.ToString();
                                progressCompleteDictionary[ProgressType.StepCount] = deploymentTaskManager.GetStepCount().ToString();
                                progress.Report(progressCompleteDictionary);
                            }
                        }
                        else
                        {
                            progressCompleteDictionary[ProgressType.Name] = MessageStrings.ProgressComplete;
                            progressCompleteDictionary[ProgressType.StepCount] = deploymentTaskManager.GetStepCount().ToString();
                            progress.Report(progressCompleteDictionary);
                        }
                    }
                    if (deploymentTaskManager.Status == TaskManagerStatus.Canceled)
                    {
                        var progressCancelledDictionary = new Dictionary<ProgressType, string>();

                        progressCancelledDictionary[ProgressType.Name] = MessageStrings.DeploymentCancelled;
                        progressCancelledDictionary[ProgressType.BarStatus] = ProgressBarStatusType.Error.ToString();
                        progressCancelledDictionary[ProgressType.StepCount] = deploymentTaskManager.GetStepCount().ToString();
                        progress.Report(progressCancelledDictionary);
                    }
                }
                catch (Exception ex)
                {
                    var progressCriticalErrorDictionary = new Dictionary<ProgressType, string>();

                    progressCriticalErrorDictionary[ProgressType.Error] = ex.Message;
                    progressCriticalErrorDictionary[ProgressType.BarStatus] = ProgressBarStatusType.Error.ToString();
                    progressCriticalErrorDictionary[ProgressType.StepCount] = deploymentTaskManager.GetStepCount().ToString();
                    progressCriticalErrorDictionary[ProgressType.Name] = MessageStrings.ProgressFailed;
                    progress.Report(progressCriticalErrorDictionary);
                }
            }, cancelToken);
        }
    }
}
;