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
        internal const string UnableToLocateProject = "Unable to Locate the following Client: {0}";
        internal const string UnableToLocateEnvironment = "Unable to Locate the following EnvironmentType: {0}";
        internal const string UnableToLocateDeploymentType = "Unable to Locate the following DeploymentType: {0}";
        internal const string UnableToLocateRelease = "Unable to Locate Deployable release for the following Client: {0}";
        internal const string UnableToParseDateTime = "Unable to Parse Queued Date Time: {0}";
        internal const string InvalidApiKey = "The supplied Api Key is invalid.";
        internal const string MissingKeyFile = "Missing KeyFile in executable directory";
        internal const string InvalidAdminApiKey = "Incorrect AdminApiKey supplied.";
        internal const string InvaidUserName = "Invalid UserName or No UserName Entered";
    }
}
