using System.Threading;
using System.Windows.Forms;

namespace OctopusDeploymentManagerPlus.Forms
{
    public partial class frmOctopusDeploymentManagerPlusLoading : Form
    {
        public frmOctopusDeploymentManagerPlusLoading()
        {
            InitializeComponent();
        }

        private delegate void CloseDelegate();
        private static frmOctopusDeploymentManagerPlusLoading LoadingForm;
        public static void ShowLoadingForm()
        {
            if (LoadingForm != null)
            {
                return;
            }
            Thread thread = new Thread(new ThreadStart(ShowForm));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            //Sleeping the main thread to ensure that the loading form loads....  This is stupid.
            Thread.Sleep(100);
        }

        private static void ShowForm()
        {
            LoadingForm = new frmOctopusDeploymentManagerPlusLoading();
            LoadingForm.ShowDialog();
        }

        public static void CloseForm()
        {
            LoadingForm.Invoke(new CloseDelegate(CloseFormInternal));
        }

        private static void CloseFormInternal()
        {
            LoadingForm.Close();
        }
    }
}
