using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Octopus.Client.Model;
using OctopusDeploymentManagerPlus.Constants;
using OctopusHelpers;
using OctopusDeploymentManagerPlus.Models.Interfaces;

namespace OctopusDeploymentManagerPlus.Helpers
{
    internal class OctopusStepHelper
    {
        internal static List<DeploymentTemplateStep> GetRequiredSteps(IOctopusDeploySettings octopusSettingsData)
        {
            var requiredStepsValue = ConfigurationManager.AppSettings[ResourceStrings.RequiredStepsProjectAppSettingKey];
            var deploymentTypeReleaseVersion = ConfigurationManager.AppSettings[ResourceStrings.DeploymentTypeReleaseVersionAppSettingKey];
            var systemProjectSemanticVersion = new SemanticVersion("0.0.0.0");
            SemanticVersion.TryParse(deploymentTypeReleaseVersion, out systemProjectSemanticVersion);
            var project = ProjectHelper.GetProjectByName(octopusSettingsData.Repository, requiredStepsValue);
            var release = ReleaseHelper.GetProjectReleaseByVersion(octopusSettingsData.Repository, project, systemProjectSemanticVersion);
            var enviornment = EnvironmentHelper.GetEnvironmentByName(octopusSettingsData.Repository, octopusSettingsData.EnvironmentTypeForDeploy.Name);
            var steps = StepHelper.GetReleaseEnvironmentDeploymentSteps(octopusSettingsData.Repository, release, enviornment);
            return steps.ToList();
        }

        internal static List<DeploymentTemplateStep> GetDeploymentTypeSteps(IOctopusDeploySettings octopusSettingsData)
        {
            var deploymentTypeReleaseVersion = ConfigurationManager.AppSettings[ResourceStrings.DeploymentTypeReleaseVersionAppSettingKey];
            var systemProjectSemanticVersion = new SemanticVersion("0.0.0.0");
            SemanticVersion.TryParse(deploymentTypeReleaseVersion, out systemProjectSemanticVersion);
            var stepNameList = GetRequiredSteps(octopusSettingsData);
            var release = ReleaseHelper.GetProjectReleaseByVersion(octopusSettingsData.Repository, octopusSettingsData.DeploymentTypeForDeploy.DeploymentType, systemProjectSemanticVersion);
            var enviornment = EnvironmentHelper.GetEnvironmentByName(octopusSettingsData.Repository, octopusSettingsData.EnvironmentTypeForDeploy.Name);
            var steps = StepHelper.GetReleaseEnvironmentDeploymentSteps(octopusSettingsData.Repository, release, enviornment);
            stepNameList.AddRange(steps);
            return stepNameList;
        }
    }
}