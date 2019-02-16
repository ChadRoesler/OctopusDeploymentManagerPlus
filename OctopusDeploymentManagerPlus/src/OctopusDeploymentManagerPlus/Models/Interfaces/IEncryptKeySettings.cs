namespace OctopusDeploymentManagerPlus.Models.Interfaces
{
    internal interface IEncryptKeySettings
    {
        string AdminApiKey { get; set; }
        string KeyOutputDirectory { get; set; }
    }
}
