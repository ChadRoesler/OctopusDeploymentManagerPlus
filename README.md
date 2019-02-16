# OctopusDeploymentManagerPlus


Brief Overview:

Abstraction engine for deployments for when you have multiple projects, and you have to have specific steps chosen to deploy different content sets.

Case: 
  Project 1 through Project 50 all have the same deployment process.
  Each Project has a few steps that deploy content specific to that Project.
  A deployer that does not manage the deployment process, manages the deployments of the content.
  This allows you to abstract the process so that changes can be made without having to alert the deployer of the changes.
  

Configuration:
  To launch the applicaiton in GUI mode the following will need to be configured first:
    
    Configure Octopus:
      Write Notes here Later
    
    
    Generate Encrypted AdminApiKey:
      Enter Cmd and CD to the dir of the Application.
        Execute the following command: OctopusDeploymentManagerPlus Encrypt -a "#{YourAdminApiKeyHere}" -k "#{TheDirectoryOfTheApplication}
        This will return the Encyrpted API Key and the key file for decryption.
  
    AdminApiKey: Output from the above process.
    OctopusApiUrl: The url of your octopus server.
    DeploymentTypeProjectGroup: The Project Group that contains your deployment types.
    DeploymentTypeLifeCyclePhase: The LifeCycle that contains the environments you are allowing to deploy to.
      Note: All Projects in your DeploymentTypeProjectGroup should be under this lifecycle.
    DeploymentTypeReleaseVersion: The Release version to look to when gathering steps for DeploymentTypes int he DeploymentTypeProjectGroup.
    RequiredStepsProject: The Project that contains Steps that are required across all Deployment Types.
    UserName: Can be left blank, as the user will be prompted for this upon launch.  It will then update the config file with the users entered name if it is found to be valid.
    
    
    
