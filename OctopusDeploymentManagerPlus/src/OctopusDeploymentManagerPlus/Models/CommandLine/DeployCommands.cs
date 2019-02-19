using System;
using System.Linq;
using CommandLine;
using CommandLine.Text;
using Octopus.Client;
using Octopus.Client.Model;
using OctopusHelpers;
using OctopusDeploymentManagerPlus.Constants;
using OctopusDeploymentManagerPlus.Models.Interfaces;
using OctopusDeploymentManagerPlus.Workers;

namespace OctopusDeploymentManagerPlus.Models.CommandLine
{
    public class DeployCommands : IOctopusDeploySettings
    {
        private string StoredQueuedDateTime;

        [Option('p', "projectName", HelpText = CommandStrings.ProjectNameHelp, Required = true)]
        public string ProjectName { get; set; }

        [Option('d', "deploymentType", HelpText = CommandStrings.DeploymentTypeHelp, Required = true)]
        public string DeploymentType { get; set; }

        [Option('e', "environmentType", HelpText = CommandStrings.EnvironmentTypeHelp, Required = true)]
        public string EnvironmentType { get; set; }

        [Option('r', "release", HelpText = CommandStrings.ReleaseHelp)]
        public string Release { get; set; }

        [Option('a', "apiKey", HelpText = CommandStrings.ApiKeyHelp, Required = true)]
        public string ApiKey { get; set; }

        [Option('q', "queueDateTime", HelpText = CommandStrings.QueueDateTimeHelp, Required = false)]
        public string QueueDateTime
        {
            get
            {
                return StoredQueuedDateTime;
            }
            set
            {
                StoredQueuedDateTime = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    var dateTimeOffsetOutput = new DateTimeOffset();
                    if(DateTimeOffset.TryParse(StoredQueuedDateTime, out dateTimeOffsetOutput))
                    {
                        QueuedDateTimeForDeploy = dateTimeOffsetOutput;
                    }
                    else
                    {
                        QueuedDateTimeForDeploy = new DateTimeOffset();
                    }
                }
            }
        }

        public void ProcessDeploymentArguments()
        {
            var userOctopusConnection = new OctopusConnection();
            if (!string.IsNullOrWhiteSpace(ApiKey))
            {
                Repository = userOctopusConnection.UserConnection(ApiKey);
            }
            else
            {
                Repository = null;
            }
            if (!string.IsNullOrWhiteSpace(EnvironmentType))
            {
                EnvironmentTypeForDeploy = userOctopusConnection.GetEnvironmentTypes().Where(x => x.Name.Equals(EnvironmentType, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            }
            else
            {
                EnvironmentTypeForDeploy = null;
            }
            if (!string.IsNullOrWhiteSpace(DeploymentType))
            {
                DeploymentTypeForDeploy = userOctopusConnection.GetDeploymentTypes().Where(x => x.DeploymentType.Name.Equals(DeploymentType, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            }
            else
            {
                DeploymentTypeForDeploy = null;
            }
            if (!string.IsNullOrWhiteSpace(ProjectName))
            {
                ProjectForDeploy = ProjectHelper.GetProjectByName(Repository, ProjectName);
            }
            else
            {
                ProjectForDeploy = null;
            }
        }

        [ParserState]
        public IParserState LastParserState { get; set; }

        public OctopusRepository Repository { get; set; }

        public DeploymentTypeInfo DeploymentTypeForDeploy { get; set; }

        public EnvironmentResource EnvironmentTypeForDeploy { get; set; }

        public ProjectResource ProjectForDeploy { get; set; }

        public DateTimeOffset QueuedDateTimeForDeploy { get; set; }

        [HelpVerbOption("help", HelpText = CommandStrings.HelpText)]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
