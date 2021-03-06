﻿using CommandLine;
using CommandLine.Text;
using OctopusDeploymentManagerPlus.Constants;
using OctopusDeploymentManagerPlus.Models.Interfaces;

namespace OctopusDeploymentManagerPlus.Models.CommandLine
{
    public class EncryptKeyCommands : IEncryptKeySettings
    {
        [Option('a', "adminApiKey", HelpText = CommandStrings.AdminApiKeyHelp, Required = true)]
        public string AdminApiKey { get; set; }

        [Option('k', "keyOutputDirectory", HelpText = CommandStrings.KeyOutputDirectoryHelp, Required = true)]
        public string KeyOutputDirectory { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpVerbOption("help", HelpText = CommandStrings.HelpText)]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
