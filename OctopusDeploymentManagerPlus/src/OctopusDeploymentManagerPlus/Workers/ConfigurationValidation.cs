using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using HybridScaffolding;
using Octopus.Client;
using Octopus.Client.Model;
using OctopusHelpers;
using OctopusDeploymentManagerPlus.Constants;


namespace OctopusDeploymentManagerPlus.Workers
{
    static internal class ConfigurationValidation
    {
        static internal List<string> GeneralConfigValidation(RunTypes runType)
        {
            var errorList = new List<string>();
            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[ResourceStrings.OctopusApiUrlAppSettingKey]))
            {
                errorList.Add(ErrorStrings.OctopusApiUrlMissing);
            }
            if (runType == RunTypes.Gui && string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[ResourceStrings.AdminApiKeyAppSettingKey]))
            {
                errorList.Add(ErrorStrings.AdminApiKeynMissing);
            }
            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[ResourceStrings.DeploymentTypeProjectGroupAppSettingKey]))
            {
                errorList.Add(ErrorStrings.DeploymentTypeProjectGroupMissing);
            }
            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[ResourceStrings.RequiredStepsProjectAppSettingKey]))
            {
                errorList.Add(ErrorStrings.RequiredStepsMissing);
            }
            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[ResourceStrings.DeploymentTypeLifeCyclePhaseAppSettingKey]))
            {
                errorList.Add(ErrorStrings.DeploymentTypeLifeCycleMissing);
            }
            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[ResourceStrings.DeploymentTypeReleaseVersionAppSettingKey]))
            {
                errorList.Add(ErrorStrings.DeploymentTypeReleaseVersionMissing);
            }
            return errorList;
        }

        static internal List<string> GeneralOctopusValidation(OctopusRepository octopusRepository)
        {
            var errorList = new List<string>();
            var deploymentTypeProjectGroup = ConfigurationManager.AppSettings[ResourceStrings.DeploymentTypeProjectGroupAppSettingKey];
            var requiredStepsProject = ConfigurationManager.AppSettings[ResourceStrings.RequiredStepsProjectAppSettingKey];
            var deploymentTypeLifeCycle = ConfigurationManager.AppSettings[ResourceStrings.DeploymentTypeLifeCyclePhaseAppSettingKey];
            var deploymentTypeReleaseVersion = ConfigurationManager.AppSettings[ResourceStrings.DeploymentTypeReleaseVersionAppSettingKey];
            if (ProjectGroupHelper.GetProjectGroupByName(octopusRepository, deploymentTypeProjectGroup) == null)
            {
                errorList.Add(string.Format(ErrorStrings.DeploymentTypeProjectGroupNotFound, deploymentTypeProjectGroup));
            }
            else if (ProjectGroupHelper.GetProjectGroupByName(octopusRepository, deploymentTypeProjectGroup) == null)
            {
                errorList.Add(string.Format(ErrorStrings.DeploymentTypeProjectGroupNoProjects, deploymentTypeProjectGroup));
            }

            if (ProjectHelper.GetProjectByName(octopusRepository, requiredStepsProject) == null)
            {
                errorList.Add(string.Format(ErrorStrings.RequiredStepsProjectNotFound, requiredStepsProject));
            }
            if (LifecycleHelper.GetLifecycleByName(octopusRepository, deploymentTypeLifeCycle) == null)
            {
                errorList.Add(string.Format(ErrorStrings.DeploymentTypeLifeCycleNotFound, deploymentTypeLifeCycle));
            }
            var deploymentTypeProjects = ProjectGroupHelper.GetProjectsByProjectGroupName(octopusRepository, deploymentTypeProjectGroup);
            if (deploymentTypeProjects.Count() > 0)
            {
                var releases = new List<ReleaseResource>();
                foreach (var project in deploymentTypeProjects)
                {
                    releases.AddRange(ReleaseHelper.GetProjectReleases(octopusRepository, project).Where(r => r.Version == deploymentTypeReleaseVersion));
                }
                if (releases.Count == 0)
                {
                    errorList.Add(string.Format(ErrorStrings.DeploymentTypeReleaseVersionNotFound, deploymentTypeReleaseVersion));
                }
            }
            return errorList;
        }
    }
}
