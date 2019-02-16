namespace OctopusDeploymentManagerPlus.Constants
{
    internal class UiStrings
    {
        internal const string ProgressFormat = "Executing: {0}";
        internal const string ErrorRetrievingApiKey = "Unable to Create Api Key for User: {0}.  Please contact an Octopus Administrator.";
        internal const string ErrorRetrievingApiKeyMessageBox = "User: {0} does not exist in Octopus.  Please contact an Octopus Administrator.";
        internal const string ErrorCaptionText = "Octopus User Error";
        internal const string ConfirmCancelation = "Are you sure you want to cancel the current deployment?";
        internal const string ConfirmCancelationMessageBox = "Cancelation Confirmation";
        internal const string ErrorMessage = "An error has occured.\r\nPlease contact an Octopus Administrator\r\nError: {0}{1}";
        internal const string InnerException = "\r\nInnerException: {0}";
        internal const string ErrorConnectingMessageBox = "Error Connecting to Octopus";
        internal const string ErrorConfigrationMessageBox = "Error in Octopus Deployment Manager configuration";
        internal const string VersionVerificaiton = "Deploying version: {0}.\r\nIf this is incorrect please click 'Cancel', and contact an Octopus Administrator.";
    }
}
