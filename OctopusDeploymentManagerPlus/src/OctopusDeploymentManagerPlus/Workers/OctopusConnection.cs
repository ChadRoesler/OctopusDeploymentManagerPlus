using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Configuration;
using System.Reflection;
using Octopus.Client;
using Octopus.Client.Model;
using OctopusDeploymentManagerPlus.Constants;
using OctopusDeploymentManagerPlus.Models;
using OctopusHelpers;


namespace OctopusDeploymentManagerPlus.Workers
{
    internal sealed class OctopusConnection
    {

        Assembly KeyFile = null;
        Type CryptographyManagement = null;
        object CryptographyManagementObject = null;
        MethodInfo Decryption = null;
        public string OctopusUserName { get; set; }
        private OctopusRepository StoredConnection;


        private ApiKeyResource OctopusUserAPIKey { get; set; }

        internal OctopusConnection()
        {
        }

        internal void GuiConfigure()
        {
            var userName = ConfigurationManager.AppSettings[ResourceStrings.UserNameAppSettingKey];
            KeyFile = Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ResourceStrings.KeyFileName));
            CryptographyManagement = KeyFile.GetType(ResourceStrings.OctopusDeploymentManagerKeyType);
            CryptographyManagementObject = KeyFile.CreateInstance(ResourceStrings.OctopusDeploymentManagerKeyType);
            Decryption = CryptographyManagement.GetMethod(ResourceStrings.DecryptionMethodName, new Type[] { typeof(string) });
            StoredConnection = AdminConnection();
        }

        internal bool Validate()
        {
            return OctopusUserAPIKey != null;
        }

        internal bool ValidateUser()
        {
            var octopusUserResource = StoredConnection.Users.FindAll().Where(x => x.Username == OctopusUserName).FirstOrDefault();
            return octopusUserResource != null;
        }

        internal bool Validate(OctopusRepository octRepo)
        {
            try
            {
                octRepo.Users.GetCurrent();
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal bool ValidateAdminConnection()
        {
            try
            {
                StoredConnection.Users.GetCurrent();
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal OctopusRepository UserConnection()
        {
            var octopusServerApi = ConfigurationManager.AppSettings[ResourceStrings.OctopusApiUrlAppSettingKey];
            var repository = new OctopusRepository(new OctopusServerEndpoint(octopusServerApi, OctopusUserAPIKey.ApiKey));
            repository.Users.SignOut();
            return repository;
        }

        internal OctopusRepository UserConnection(string apiKey)
        {
            var octopusServerApi = ConfigurationManager.AppSettings[ResourceStrings.OctopusApiUrlAppSettingKey];
            var repository = new OctopusRepository(new OctopusServerEndpoint(octopusServerApi, apiKey));
            OctopusUserName = repository.Users.GetCurrent().Username;
            StoredConnection = repository;
            return repository;
        }

        internal OctopusRepository AdminConnection()
        {
            var octopusServerApi = ConfigurationManager.AppSettings[ResourceStrings.OctopusApiUrlAppSettingKey];
            var adminEncryptedKey = ConfigurationManager.AppSettings[ResourceStrings.AdminApiKeyAppSettingKey];
            var encryptedStringAsObject = new object[] { adminEncryptedKey };
            var adminDecryptedKey = (string)Decryption.Invoke(CryptographyManagementObject, encryptedStringAsObject);
            var endpoint = new OctopusServerEndpoint(octopusServerApi, adminDecryptedKey);
            var repository = new OctopusRepository(endpoint);
            return repository;
        }

        internal void GenerateUserApiKey(string userName)
        {
            OctopusUserName = userName;
            var octUser = UserHelper.GetUserFromUserName(StoredConnection, OctopusUserName);
            var createdAPIKey = UserHelper.CreateApiKeyForUser(StoredConnection, octUser, ResourceStrings.ApiKeyCreationNote);
            OctopusUserAPIKey = createdAPIKey;
        }

        internal void RevokeUserApiKey()
        {
            if (!(OctopusUserAPIKey == null))
            {
                UserHelper.RevokeApiKey(StoredConnection, OctopusUserAPIKey);
            }
        }

        internal List<EnvironmentResource> GetEnvironmentTypes()
        {
            var requiredStepsValue = ConfigurationManager.AppSettings[ResourceStrings.RequiredStepsProjectAppSettingKey];
            var deploymentTypeLifePhaseCycle = ConfigurationManager.AppSettings[ResourceStrings.DeploymentTypeLifeCyclePhaseAppSettingKey];
            var environmentList = new List<EnvironmentResource>();
            var requiredStepsProject = ProjectHelper.GetProjectByName(StoredConnection, requiredStepsValue);
            environmentList.AddRange(EnvironmentHelper.GetProjectEnvironments(StoredConnection, requiredStepsProject, deploymentTypeLifePhaseCycle));
            return environmentList;
        }

        private List<string> GetUserDeploymentTypeAccess()
        {
            var teamName = string.Empty;
            var octUser = UserHelper.GetUserFromUserName(StoredConnection, OctopusUserName);
            var fullTeamList = UserHelper.GetUserTeams(StoredConnection, octUser).ToList();

            var projectList = new List<string>();
            foreach (var team in fullTeamList)
            {
                if (team.ProjectIds.Count > 0)
                {
                    projectList = projectList.Union(team.ProjectIds).ToList();
                }
            }
            return projectList;
        }

        internal List<string> GetUserProjectGroupAccess()
        {
            var teamName = string.Empty;
            var octUser = UserHelper.GetUserFromUserName(StoredConnection, OctopusUserName);
            var fullTeamList = UserHelper.GetUserTeams(StoredConnection, octUser).ToList();

            var projectGroupList = new List<string>();
            foreach (var team in fullTeamList)
            {
                if (team.ProjectGroupIds.Count > 0)
                {
                    projectGroupList = projectGroupList.Union(team.ProjectGroupIds.ToList()).ToList();
                }
                else
                {
                    var allProjectGroups = StoredConnection.ProjectGroups.GetAll().Select(g => g.Id).ToList();
                    projectGroupList = projectGroupList.Union(allProjectGroups).ToList();
                }
            }
            return projectGroupList;
        }

        internal List<DeploymentTypeInfo> GetDeploymentTypes()
        {
            var deploymentTypeList = new List<DeploymentTypeInfo>();
            var userTeamList = GetUserDeploymentTypeAccess();
            var deploymentTypeProjectGroup = ConfigurationManager.AppSettings[ResourceStrings.DeploymentTypeProjectGroupAppSettingKey];
            var systemProject = ProjectGroupHelper.GetProjectGroupByName(StoredConnection, deploymentTypeProjectGroup);
            var deploymentProjects = ProjectGroupHelper.GetProjectsByProjectGroup(StoredConnection, systemProject);
            foreach (var project in deploymentProjects)
            {
                if(userTeamList.Contains(project.Id))
                {
                    deploymentTypeList.Add(new DeploymentTypeInfo(project, StoredConnection));
                }
            }
            return deploymentTypeList;
        }
    }
}
