using System;
using System.Collections.Generic;
using System.Linq;
using Octopus.Client.Model;
using OctopusHelpers;
using OctopusHelpers.ExtensionMethods;
using OctopusDeploymentManagerPlus.Models.Interfaces;
using OctopusDeploymentManagerPlus.Enums;

namespace OctopusDeploymentManagerPlus.Workers
{
    internal class OctopusResource
    {
        private readonly IOctopusDeploySettings sessionOctopusSettings;
        private readonly string releaseVersion;
        private OctopusConnection octopusConnection;

        internal OctopusResource(IOctopusDeploySettings octopusSettingsData)
        {
            sessionOctopusSettings = octopusSettingsData;
            octopusConnection = new OctopusConnection();
            octopusConnection.GuiConfigure();
        }
        
        internal void SetUserName(string octopusUserName)
        {
            octopusConnection.OctopusUserName = octopusUserName;
        }

        internal OctopusResource(IOctopusDeploySettings octopusSettingsData, string release)
        {
            sessionOctopusSettings = octopusSettingsData;
            releaseVersion = release;
        }

        internal ProjectResource GetClientProject()
        {
            var clientProject = ProjectHelper.GetProjectByName(sessionOctopusSettings.Repository, sessionOctopusSettings.ProjectName) ;
            return clientProject;
        }

        internal bool ValidateProjectName()
        {
            return (!(GetClientProject() == null));
        }

        internal bool ValidateClientProjectAccess()
        {
            var clientProject = GetClientProject();
            var projectGroupIds = octopusConnection.GetUserProjectGroupAccess();
            return projectGroupIds.Contains(clientProject.ProjectGroupId);
        }

        internal bool ValidateLastRelease()
        {
            var clientProject = GetClientProject();
            var previousDeployments = ReleaseHelper.GetDeployedReleasesFromProjectEnvironment(sessionOctopusSettings.Repository, clientProject, sessionOctopusSettings.EnvironmentTypeForDeploy);
            return (previousDeployments.Count() > 0);
        }

        internal ReleaseResource GetLatestMinorOfMajorRelease()
        {
            var clientProject = GetClientProject();
            var lastDeployedRelease = ReleaseHelper.GetLastDeployedReleaseFromProjectEnvironment(sessionOctopusSettings.Repository, clientProject, sessionOctopusSettings.EnvironmentTypeForDeploy);
            var latestPatchRelease = ReleaseHelper.GetProjectReleases(sessionOctopusSettings.Repository, clientProject).Where(r => r.GetSemanticVerionObject().Major.Equals(lastDeployedRelease.GetSemanticVerionObject().Major) && r.GetSemanticVerionObject().CompareTo(lastDeployedRelease.GetSemanticVerionObject()) >= 0).FirstOrDefault();
            return latestPatchRelease;
        }

        internal ReleaseResource GetDeployableRelease()
        {
            var clientProject = GetClientProject();
            var releaseType = sessionOctopusSettings.DeploymentTypeForDeploy.ReleaseTypes[sessionOctopusSettings.EnvironmentTypeForDeploy.Id];
            var release = new ReleaseResource();
            var releaseSemanticVersionObject = new SemanticVersion("0.0.0.0");
            switch (releaseType)
            {
                case ReleaseType.LatestRelease:
                    release = ReleaseHelper.GetLatestReleaseSemanticVersionFromProject(sessionOctopusSettings.Repository, clientProject);
                    break;
                case ReleaseType.LatestMinor:
                    release = GetLatestMinorOfMajorRelease();
                    break;
                case ReleaseType.Targeted:    
                    if (SemanticVersion.TryParse(releaseVersion, out releaseSemanticVersionObject))
                    {
                        release = ReleaseHelper.GetProjectReleaseByVersion(sessionOctopusSettings.Repository, clientProject, releaseSemanticVersionObject);
                    }
                    else
                    {
                        release = null;
                    }
                    break;
                case ReleaseType.FullList:
                    if (SemanticVersion.TryParse(releaseVersion, out releaseSemanticVersionObject))
                    {
                        release = ReleaseHelper.GetProjectReleaseByVersion(sessionOctopusSettings.Repository, clientProject, releaseSemanticVersionObject);
                    }
                    else
                    {
                        release = ReleaseHelper.GetLatestReleaseSemanticVersionFromProject(sessionOctopusSettings.Repository, clientProject);
                    }
                    break;
                case ReleaseType.Current:
                    release = ReleaseHelper.GetLastDeployedReleaseFromProjectEnvironment(sessionOctopusSettings.Repository, clientProject, sessionOctopusSettings.EnvironmentTypeForDeploy);
                    break;
            }
            return release;
        }

        internal List<ReleaseResource> GetDeployableReleases()
        {
            var releaseList = new List<ReleaseResource>();
            var clientProject = GetClientProject();
            var releaseType = sessionOctopusSettings.DeploymentTypeForDeploy.ReleaseTypes[sessionOctopusSettings.EnvironmentTypeForDeploy.Id];
            switch (releaseType)
            {
                case ReleaseType.FullList:
                    releaseList.AddRange(ReleaseHelper.GetProjectReleases(sessionOctopusSettings.Repository, clientProject));
                    break;
                case ReleaseType.Targeted:
                    var releaseSemanticVersionObject = new SemanticVersion("0.0.0.0");
                    if (SemanticVersion.TryParse(releaseVersion, out releaseSemanticVersionObject))
                    {
                        releaseList.Add(ReleaseHelper.GetProjectReleaseByVersion(sessionOctopusSettings.Repository, clientProject, releaseSemanticVersionObject));
                    }
                    break;
                case ReleaseType.LatestRelease:
                    releaseList.Add(ReleaseHelper.GetLatestReleaseSemanticVersionFromProject(sessionOctopusSettings.Repository, clientProject));
                    break;
                case ReleaseType.LatestMinor:
                    releaseList.Add(GetLatestMinorOfMajorRelease());
                    break;
                case ReleaseType.Current:
                    releaseList.Add(ReleaseHelper.GetLastDeployedReleaseFromProjectEnvironment(sessionOctopusSettings.Repository, clientProject, sessionOctopusSettings.EnvironmentTypeForDeploy));
                    break;
            }
            return releaseList;

        }

        public string[] GetDeployableClientsProjects()
        {
            var projectListNames = new List<string>();
            var projectGroupIds = octopusConnection.GetUserProjectGroupAccess();
            foreach (var projectGroupId in projectGroupIds)
            {
                projectListNames.AddRange((ProjectGroupHelper.GetProjectsByProjectGroupId(sessionOctopusSettings.Repository, projectGroupId).Select(x => x.Name).ToList()));
            }
            return projectListNames.ToArray();
        }
    }
}
