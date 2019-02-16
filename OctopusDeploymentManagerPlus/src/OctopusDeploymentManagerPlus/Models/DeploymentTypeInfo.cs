using System;
using System.Collections.Generic;
using System.Linq;
using Octopus.Client;
using Octopus.Client.Model;
using OctopusDeploymentManagerPlus.Constants;
using OctopusDeploymentManagerPlus.Enums;
using OctopusHelpers;


namespace OctopusDeploymentManagerPlus.Models
{
    public class DeploymentTypeInfo
    {
        private readonly ProjectResource DeploymentTypeForInfo;
        private readonly string DeploymentTypeDescription;
        private readonly bool DeploymentTypeVisibility;
        private readonly bool DeploymentTypeGuidedFailure;
        private readonly bool DeploymentTypePreviousDeployCheck;
        private readonly int DeploymentTypeOrder;
        private readonly string DeploymentTypeLabel;
        private readonly Dictionary<string, ReleaseType> DeploymentTypeReleaseType = new Dictionary<string, ReleaseType>();


        public DeploymentTypeInfo(ProjectResource projectResource, OctopusRepository octRepo)
        {
            DeploymentTypeForInfo = projectResource;
            DeploymentTypeDescription = projectResource.Description + ResourceStrings.CssStyle;
            var projectVars = VariableSetHelper.GetVariableSetFromProject(octRepo, projectResource).Variables;
            var label = projectVars.Where(x => x.Name.Equals(ResourceStrings.SystemProjectLabelVar, StringComparison.InvariantCultureIgnoreCase));
            if (label.Count() > 0)
            {
                DeploymentTypeLabel = label.Select(x => x.Value).FirstOrDefault();
            }
            else
            {
                DeploymentTypeLabel = projectResource.Name;
            }
            var order = projectVars.Where(x => x.Name.Equals(ResourceStrings.SystemProjectOrderVar, StringComparison.InvariantCultureIgnoreCase));
            if (order.Count() > 0)
            {
                if (!int.TryParse(order.Select(x => x.Value).FirstOrDefault(), out DeploymentTypeOrder))
                {
                    DeploymentTypeOrder = 0;
                }
            }
            var visible = projectVars.Where(x => x.Name.Equals(ResourceStrings.SystemProjectVisibleVar, StringComparison.InvariantCultureIgnoreCase));
            if (visible.Count() > 0)
            {
                if (!bool.TryParse(visible.Select(x => x.Value).FirstOrDefault(), out DeploymentTypeVisibility))
                {
                    DeploymentTypeVisibility = false;
                }
            }
            var guidedFailure = projectVars.Where(x => x.Name.Equals(ResourceStrings.SystemProjectGuidedFailVar, StringComparison.InvariantCultureIgnoreCase));
            if (guidedFailure.Count() > 0)
            {
                if (!bool.TryParse(guidedFailure.Select(x => x.Value).FirstOrDefault(), out DeploymentTypeGuidedFailure))
                {
                    DeploymentTypeGuidedFailure = true;
                }
            }
            var previousDeployCheck = projectVars.Where(x => x.Name.Equals(ResourceStrings.SystemProjectPrevDeployVar, StringComparison.InvariantCultureIgnoreCase));
            if (previousDeployCheck.Count() > 0)
            {
                if (!bool.TryParse(previousDeployCheck.Select(x => x.Value).FirstOrDefault(), out DeploymentTypePreviousDeployCheck))
                {
                    DeploymentTypePreviousDeployCheck = true;
                }
            }
            var releaseType = projectVars.Where(x => x.Name.Equals(ResourceStrings.SystemProjectReleaseTypeVar, StringComparison.InvariantCultureIgnoreCase));
            if (releaseType.Count() > 0)
            {
                foreach(var variable in releaseType)
                {
                    foreach(var environment in variable.Scope[ScopeField.Environment].ToList())
                    {
                        ReleaseType type = ReleaseType.Current;
                        if(!Enum.TryParse(variable.Value, out type))
                        {
                            type = ReleaseType.Current;
                        }
                        DeploymentTypeReleaseType.Add(environment, type);
                    }
                }
            }
        }

        public Dictionary<string, ReleaseType> ReleaseTypes
        {
            get
            {
                return DeploymentTypeReleaseType;
            }
        }

        public ProjectResource DeploymentType
        {
            get
            {
                return DeploymentTypeForInfo;
            }
        }

        public string Description
        {
            get
            {
                return DeploymentTypeDescription;
            }
        }


        public bool Visible
        {
            get
            {
                return DeploymentTypeVisibility;
            }
        }

        public bool GuidedFailure
        {
            get
            {
                return DeploymentTypeGuidedFailure;
            }
        }

        public bool PreviousDeployCheck
        {
            get
            {
                return DeploymentTypePreviousDeployCheck;
            }
        }

        public int Order
        {
            get
            {
                return DeploymentTypeOrder;
            }
        }

        public string Label
        {
            get
            {
                return DeploymentTypeLabel;
            }
        }
    }
}
