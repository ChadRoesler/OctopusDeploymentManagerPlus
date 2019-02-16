namespace OctopusDeploymentManagerPlus.Constants
{
    internal class CommandStrings
    {
        internal const string ProjectNameHelp = @"The Project to the Deploy.";
        internal const string DeploymentTypeHelp = @"The Deployment to Deploy for the passed Project.";
        internal const string EnvironmentTypeHelp = @"The Environment To Deploy to.";
        internal const string ReleaseHelp = @"The Release to deploy, only used for targeted releases.";
        internal const string ApiKeyHelp = @"The ApiKey of the user to Deploy as.";
        internal const string QueueDateTimeHelp = @"Date and Time to start a queued deployment in the format of MM/DD/YYYY HH:MM AM/PM";
        internal const string HelpText = @"Displays Help information.";
        internal const string AdminApiKeyHelp = @"The Admin Api Key to encrypt for GUI users.";
        internal const string KeyOutputDirectoryHelp = @"The directory to output the keydll to";
        internal const string DeployCommandHelp = @"Deploy a release in octopus";
        internal const string EncryptKeyCommandHelp = @"Encrypt an ApiKey";
    }
}
