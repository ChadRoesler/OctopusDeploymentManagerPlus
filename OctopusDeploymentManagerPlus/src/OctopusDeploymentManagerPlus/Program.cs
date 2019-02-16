using System;
using System.Reflection;
using System.Windows.Forms;
using log4net;
using HybridScaffolding;
using OctopusDeploymentManagerPlus.Forms;
using OctopusDeploymentManagerPlus.Models;

namespace OctopusDeploymentManagerPlus
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [STAThread]
        static void Main(string[] arguments)
        {
#if DEBUG
            System.Diagnostics.Debugger.Launch();
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var odmScaffold = new OctopusDeploymentManagerPlusScaffold();
            try
            {
                HybridExecutor.DispatchExecutor(odmScaffold, arguments, typeof(frmOctopusDeploymentManagerPlus));
            }
            catch(Exception ex)
            {
                if(odmScaffold.RunType == RunTypes.Console || odmScaffold.RunType == RunTypes.Powershell)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}