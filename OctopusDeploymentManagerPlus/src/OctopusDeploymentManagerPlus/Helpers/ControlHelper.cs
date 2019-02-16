using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32;
using OctopusDeploymentManagerPlus.Constants;

namespace OctopusDeploymentManagerPlus.Helpers
{
    internal class ControlHelper
    {
        internal static IEnumerable<Control> GetAllTextBoxControls(Control controlToProcess)
        {
            List<Control> controlList = new List<Control>();
            foreach (Control c in controlToProcess.Controls)
            {
                if (c is TextBox)
                {
                    controlList.Add(c);
                }
                if (c is GroupBox)
                {
                    controlList.AddRange(GetAllTextBoxControls(c));
                }
            }
            return controlList;
        }

        internal static string GetDefaultWebBrowser()
        {
            var webBrowserRegistry = ResourceStrings.WebBrowserRegistryPath;
            var webBrowserUserChoiceRegistry = ResourceStrings.WebBrowserUserChoiceRegistryPath;
            var browserPathKey = ResourceStrings.BrowserPathRegistryKey;
            var browserKeyRegistry = ResourceStrings.BrowserRegistryKey;
            RegistryKey userChoiceKey = null;
            var browserPath = string.Empty;
            try
            {
                userChoiceKey = Registry.CurrentUser.OpenSubKey(webBrowserUserChoiceRegistry, false);
                if (userChoiceKey == null)
                {
                    var browserKey = Registry.ClassesRoot.OpenSubKey(browserKeyRegistry, false);
                    if (browserKey == null)
                    {
                        browserKey = Registry.CurrentUser.OpenSubKey(webBrowserRegistry, false);
                    }
                    var path = ((browserKey.GetValue(null) as string).Split('"'))[1];
                    browserKey.Close();
                    return path;
                }
                else
                {
                    string progId = userChoiceKey.GetValue(ResourceStrings.ProgramID).ToString();
                    userChoiceKey.Close();
                    string concreteBrowserKey = string.Format(browserPathKey, progId);
                    var keyPath = Registry.ClassesRoot.OpenSubKey(concreteBrowserKey, false);
                    browserPath = ((keyPath.GetValue(null) as string).Split('"'))[1];
                    keyPath.Close();
                    return browserPath;
                }
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
