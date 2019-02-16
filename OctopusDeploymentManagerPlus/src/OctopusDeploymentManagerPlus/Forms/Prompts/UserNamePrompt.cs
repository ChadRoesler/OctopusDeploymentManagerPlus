using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using OctopusDeploymentManagerPlus.Constants;
using OctopusDeploymentManagerPlus.Helpers;
using OctopusDeploymentManagerPlus.Workers;

namespace OctopusDeploymentManagerPlus
{
    public partial class frmUserNamePrompt : Form
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static Dictionary<string, string> Errors;
        private static OctopusConnection OctopusConnection;

        public frmUserNamePrompt()
        {
            InitializeComponent();
            txtUserName.Clear();
        }

        public string EnteredUserName
        {
            get
            {
                return txtUserName.Text;
            }
            set
            {
                txtUserName.Text = value;
            }
        }

        internal void PrepForm(OctopusConnection octopusConnection)
        {
            txtUserName.Clear();
            OctopusConnection = octopusConnection;
        }

        private bool ValidateForm()
        {
            Errors = new Dictionary<string, string>();
            OctopusConnection.OctopusUserName = EnteredUserName;
            if (string.IsNullOrWhiteSpace(EnteredUserName))
            {
                Errors.Add("txtUserName", "No UserName Entered.");
            }
            else
            {
                if (!OctopusConnection.ValidateUser())
                {
                    Errors.Add("txtUserName", "Invalid UserName Entered.");
                }
            }
            return (Errors.Count == 0);
        }



        private void btnOkay_Click(object sender, EventArgs e)
        {
            Log.Debug("UserName Chosen.");
            if (!ValidateForm())
            {
                foreach (var control in ControlHelper.GetAllTextBoxControls(this))
                {
                    errorProvider.SetError(control, string.Empty);
                }
                foreach (var error in Errors)
                {
                    var control = Controls.Find(error.Key, true).FirstOrDefault();
                    errorProvider.SetError(control, error.Value);
                    if (control.GetType() == typeof(DateTimePicker) || control.GetType() == typeof(ComboBox))
                    {
                        errorProvider.SetIconPadding(control, 3);
                    }
                    else
                    {
                        errorProvider.SetIconPadding(control, -20);
                    }
                }
                Cursor.Current = Cursors.Default;
                return;
            }
            else
            {
                Hide();
                DialogResult = DialogResult.OK;
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Log.Debug("UserName prompt cancelled.");
            Hide();
        }
    }
}
