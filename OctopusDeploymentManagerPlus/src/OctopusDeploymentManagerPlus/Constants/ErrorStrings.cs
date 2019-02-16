namespace OctopusDeploymentManagerPlus.Constants
{
    internal class ErrorStrings
    {
        internal const string RequiredFieldFormat = "The field \"{0}\" is required.";
        internal const string InvalidProjectName = "Invalid project name.";
        internal const string NonActiveProjectName = "User does not have access to the Group this project is in.";
        internal const string OctopusApiUrlMissing = "OctopusApiUrl is not configured.";
        internal const string AdminApiKeynMissing = "AdminApiKey is not configured.";
        internal const string DeploymentTypeProjectGroupMissing = "DeploymentTypeProjectGroup is not configured";
        internal const string RequiredStepsMissing = "RequiredStepsProject is not configured.";
        internal const string DeploymentTypeLifeCycleMissing = "DeploymentTypeLifeCycle is not configured.";
        internal const string DeploymentTypeReleaseVersionMissing = "DeploymentTypeReleaseVersion is not configured.";
        internal const string DeploymentTypeProjectGroupNotFound = "Deployment Type Project Group: {0} was not found";
        internal const string DeploymentTypeProjectGroupNoProjects = "Deployment Type Project Group: {0} does not contain any projects";
        internal const string RequiredStepsProjectNotFound = "Required Steps Project: {0} was not found.";
        internal const string DeploymentTypeLifeCycleNotFound = "Deployment Type Life Cycle: {0} was not found.";
        internal const string DeploymentTypeReleaseVersionNotFound = "Deployment Type Release Version: {0} was not found in the projects listed in DeploymentTypeProjectGroup.";
    }
}
