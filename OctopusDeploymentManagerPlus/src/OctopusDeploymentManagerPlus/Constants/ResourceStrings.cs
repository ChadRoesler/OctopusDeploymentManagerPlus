namespace OctopusDeploymentManagerPlus.Constants
{
    internal class ResourceStrings
    {
        internal const string ActiveClientsGroup = "Active Clients";
        internal const string WebBrowserRegistryPath = @"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http";
        internal const string WebBrowserUserChoiceRegistryPath = @"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice";
        internal const string BrowserPathRegistryKey = @"{0}\shell\open\command";
        internal const string BrowserRegistryKey = @"HTTP\shell\open\command";
        internal const string ProgramID = "ProgId";
        internal const string ReplacedPrinting = "{0}{1}";
        internal const string ApiKeyCreationNote = "Created by Octopus Deployment Manager Plus.";
        internal const string StepTrim = "Step";
        internal const string CssStyle = "<style type='text/css'>p { margin: 0px; font-size: 8.25pt; font-family: Microsoft Sans Serif; } ul { margin-left: 15px; font-size: 8.25pt; font-family: Microsoft Sans Serif; }</style>";
        internal const string SystemProjectVisibleVar = "Visible";
        internal const string SystemProjectOrderVar = "Order";
        internal const string SystemProjectLabelVar = "Label";
        internal const string SystemProjectPrevDeployVar = "PreviousDeployCheck";
        internal const string SystemProjectGuidedFailVar = "GuidedFailureMode";
        internal const string SystemProjectReleaseTypeVar = "ReleaseType";
        internal const string LatestMajorListReleaseType = "LatestMajorList";
        internal const string LatestMajorReleaseType = "LatestMajor";
        internal const string LatestMinorReleaseType = "LatestMinor";
        internal const string CurrentReleaseType = "Current";
        internal const string DeploymentTypeProjectGroupAppSettingKey = "DeploymentTypeProjectGroup";
        internal const string RequiredStepsProjectAppSettingKey = "RequiredStepsProject";
        internal const string DeploymentTypeLifeCyclePhaseAppSettingKey = "DeploymentTypeLifeCyclePhase";
        internal const string DeploymentTypeReleaseVersionAppSettingKey = "DeploymentTypeReleaseVersion";
        internal const string AdminApiKeyAppSettingKey = "AdminApiKey";
        internal const string OctopusApiUrlAppSettingKey = "OctopusApiUrl";
        internal const string UserNameAppSettingKey = "UserName";
        internal const string ActiveDirectoryUserName = "ActiveDirectory";
        internal const string KeyFileName = "OctopusDeploymentManagerPlusKey.dll";
        internal const string LanguageType = "CSharp";
        internal const string OctopusDeploymentManagerKeyType = "OctopusDeploymentManagerPlusKey.CryptographyManagement";
        internal const string DecryptionMethodName = "Decryption";
        internal const string KeyGenerationCode = @"using System;
using System.IO;
using System.Security.Cryptography;

namespace OctopusDeploymentManagerPlusKey
{{
    public class CryptographyManagement
    {{
        public string Decryption(string Value)
        {{
            var valueAsBytes = Convert.FromBase64String(Value);
            var decryptedString = string.Empty;
            using (var aesEncryption = new AesManaged())
            {{
                aesEncryption.Key = Convert.FromBase64String(""{0}"");
                aesEncryption.IV = Convert.FromBase64String(""{1}"");
                
                var decryptor = aesEncryption.CreateDecryptor(aesEncryption.Key, aesEncryption.IV);

                using (var decryptionMemoryStream = new MemoryStream(valueAsBytes))
                {{
                    using (var decryptionCryptoStream = new CryptoStream(decryptionMemoryStream, decryptor, CryptoStreamMode.Read))
                    {{
                        using (var decryptionStreamReader = new StreamReader(decryptionCryptoStream))
                        {{
                            decryptedString = decryptionStreamReader.ReadToEnd();
                        }}
                    }}
                }}
            }}
            return decryptedString;
        }}
    }}
}}";
    }
}