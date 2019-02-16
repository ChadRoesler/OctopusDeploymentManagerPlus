namespace OctopusDeploymentManager.Models
{
    internal interface IOctopusCredentials
    {
        string Username { get; set; }
        string Password { get; set; }
        string ApiKey { get; set; }
    }
}
