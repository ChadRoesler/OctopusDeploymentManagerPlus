namespace OctopusDeploymentManagerPlus.Constants
{
    internal class MessageStrings
    {
        internal const string ProgressBegin = "Starting [{0}] Deploy {1} to {2} [{3}]...";
        internal const string ProgressComplete = "Finished [{0}] Deploy {1} to {2} [{3}].";
        internal const string ProgressFailed = "Failed [{0}] Deploy {1} to {2} [{3}], please review the errors.";
        internal const string ProgressWarning = "Warnings [{0}] Deploy {1} to {2} [{3}], please review the warnings.";
        internal const string InvalidDeploymentDate = "The selected Date and Time is in the past.";
        internal const string NoPreviousDeployments = "The Client has had no previous deployments to this environment to run against.";
        internal const string QueuedDeploys = "There are currently: {0} deployment(s) ahead of the current deployment.";
        internal const string DeploymentCancelled = "Deployment of {0} to {1} has been canceled.";
        internal const string DeploymentProgressCancelled = "Deployment of {0} to {1} has been canceled by: {2}";
        internal const string GuidingInterruptedDeployment = "{0}ing Deployment at the Failed Step.";
        internal const string GuidingIntervenedDeployment = "{0}ing Deployment at the Manual Intervention Step.";
        internal const string GuidingDeploymentChoice = "   Chosen by: {1}.";
        internal const string PendingIntervention = "A Manual Intervention step was called.";
        internal const string InterventionAlert = "The following is a Manual Intervention Step.\r\n   ● {0}\r\nPlease alert the appropriate resources.\r\nThe following directions were supplied:\r\n   ● {1}";
        internal const string InterventionResolved = "Intervention Resolved.";
        internal const string PendingInterruption = "The deployment was interrupted, please investigate.";
        internal const string InterruptionResolved = "Interruption Resolved.";
        internal const string CancellingDeployment = "Canceling current Deployment.";
        internal const string CancellationRequested = "Cancellation of current Deployment requested.";
        internal const string WaitingForRequiredSteps = "Required Steps must complete first.\r\nWaiting for required steps to complete.";
        internal const string CancellationPendingMessageClose = "The cancellation of the current deployment has not been completed.\r\nOctopus Deployment Manager will continue to run in the background until the deployment can be appropriately canceled.\r\n\r\nAre you sure you want to exit Octopus Deployment Manager?";
        internal const string CancellationPendingTitleClose = "Cancellation Pending";
        internal const string ActiveDeploymentMessageClose = "The current deployment has not completed.\r\nThe deployment will not be canceled and can still be managed and viewed on the server.\r\n\r\nAre you sure you want to exit Octopus Deployment Manager?";
        internal const string ActiveDeploymentTitleClose = "Active Deployment";
    }
}