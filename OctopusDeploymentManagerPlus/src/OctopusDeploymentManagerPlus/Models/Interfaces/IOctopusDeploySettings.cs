using Octopus.Client;
using Octopus.Client.Model;

namespace OctopusDeploymentManagerPlus.Models.Interfaces
{
    internal interface IOctopusDeploySettings
    {
        OctopusRepository Repository { get; set; }

        string ProjectName { get; set; }

        DeploymentTypeInfo DeploymentTypeForDeploy { get; set; }

        EnvironmentResource EnvironmentTypeForDeploy { get; set; }
    }
}
