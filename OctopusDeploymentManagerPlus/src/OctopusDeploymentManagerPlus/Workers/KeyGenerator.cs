using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using OctopusDeploymentManagerPlus.Constants;

namespace OctopusDeploymentManagerPlus.Workers
{
    internal static class KeyGenerator
    {
        internal static string KeyGeneration(string AdminApiKey, string OutputDirectory)
        {
            var keyByteString = string.Empty;
            var initializationVectorByteString = string.Empty;
            var encryptedAdminApiKey = string.Empty;
            using (var aesEncryption = new AesManaged())
            {
                aesEncryption.GenerateKey();
                aesEncryption.GenerateIV();

                keyByteString = Convert.ToBase64String(aesEncryption.Key);
                initializationVectorByteString = Convert.ToBase64String(aesEncryption.IV);

                var encryptor = aesEncryption.CreateEncryptor(aesEncryption.Key, aesEncryption.IV);
                using (var encryptorMemoryStream = new MemoryStream())
                {
                    using (var encryptorCryptoStream = new CryptoStream(encryptorMemoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (var encryptorStreamWriter = new StreamWriter(encryptorCryptoStream))
                        {
                            encryptorStreamWriter.Write(AdminApiKey);
                        }
                    }
                    var encyrptedAdminApiKeyBytes = encryptorMemoryStream.ToArray();
                    encryptedAdminApiKey = Convert.ToBase64String(encyrptedAdminApiKeyBytes);
                }
            }
            var compilerParameters = new CompilerParameters();
            compilerParameters.GenerateExecutable = false;
            compilerParameters.OutputAssembly = Path.Combine(OutputDirectory, ResourceStrings.KeyFileName);
            compilerParameters.ReferencedAssemblies.Add(typeof(AesManaged).Assembly.Location);
            compilerParameters.TreatWarningsAsErrors = true;

            var provider = CodeDomProvider.CreateProvider(ResourceStrings.LanguageType);
            var splitCodeToCompile = string.Format(ResourceStrings.KeyGenerationCode, keyByteString, initializationVectorByteString);
            var compilerResults = provider.CompileAssemblyFromSource(compilerParameters, splitCodeToCompile);
            if(compilerResults.Errors.Count > 0)
            {
                var compileErrors = new StringBuilder();
                foreach(CompilerError error in compilerResults.Errors)
                {
                    compileErrors.AppendLine(string.Format("Line: {0}, Error: {1}",error.Line, error.ErrorText));
                }
                throw new Exception(compileErrors.ToString());
            }
            return encryptedAdminApiKey;
        }
    }
}
