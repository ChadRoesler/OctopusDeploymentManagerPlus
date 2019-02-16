using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;
using OctopusDeploymentManagerPlus.Constants;

namespace OctopusDeploymentManagerPlus.Models.CommandLine
{
    public class VerbCommands
    {
        public VerbCommands()
        {
            EncryptKeyCommand = new EncryptKeyCommands();
            DeployCommand = new DeployCommands();
        }

        [VerbOption("Deploy", HelpText = CommandStrings.DeployCommandHelp)]
        public DeployCommands DeployCommand { get; set; }

        [VerbOption("EncryptKey", HelpText = CommandStrings.EncryptKeyCommandHelp)]
        public EncryptKeyCommands EncryptKeyCommand { get; set; }

        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            return HelpText.AutoBuild(this, verb);
        }

        public string GetUsage()
        {
            return HelpText.AutoBuild(this);
        }
    }
}
