using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Octopus.Client.Model;
using OctopusDeploymentManagerPlus.Models;


namespace OctopusDeploymentManagerPlus.ExtensionMethods
{
    internal static class ComboBoxExtensions
    {
        internal static void BindEnvironmentListToComboBox(this ComboBox currentComboBox, List<EnvironmentResource> environmentList)
        {
            var datasource = new Dictionary<string, EnvironmentResource>();
            datasource = environmentList.ToDictionary(x => x.Name, x => x);
            currentComboBox.DataSource = new BindingSource(datasource, null);
            currentComboBox.DisplayMember = "Key";
            currentComboBox.ValueMember = "Value";
        }

        internal static void BindDeploymentTypeListToComboBox(this ComboBox currentComboBox, List<DeploymentTypeInfo> deploymentTypeList)
        {
            var datasource = new Dictionary<string, DeploymentTypeInfo>();
            datasource = deploymentTypeList.Where(x => x.Visible).OrderBy(x => x.Order).ToDictionary(x => x.Label, x => x);
            currentComboBox.DataSource = new BindingSource(datasource, null);
            currentComboBox.DisplayMember = "Key";
            currentComboBox.ValueMember = "Value";
        }

        internal static void BindReleaseListToComboBox(this ComboBox currentComboBox, List<ReleaseResource> releaseList)
        {
            var datasource = new Dictionary<string, ReleaseResource>();
            datasource = releaseList.ToDictionary(x => x.Version, x => x);
            currentComboBox.DataSource = new BindingSource(datasource, null);
            currentComboBox.DisplayMember = "Key";
            currentComboBox.ValueMember = "Value";
        }
    }
}
