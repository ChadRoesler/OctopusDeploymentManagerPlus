namespace OctopusDeploymentManagerPlus.Forms.Prompts
{
    partial class frmVersionSelector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVersionSelector));
            this.lblReleaseVersionChoice = new System.Windows.Forms.Label();
            this.cmbRelease = new System.Windows.Forms.ComboBox();
            this.btnOkay = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblReleaseVersionSelected = new System.Windows.Forms.Label();
            this.lblReleaseSelect = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblReleaseVersionChoice
            // 
            this.lblReleaseVersionChoice.AutoSize = true;
            this.lblReleaseVersionChoice.Location = new System.Drawing.Point(19, 64);
            this.lblReleaseVersionChoice.Name = "lblReleaseVersionChoice";
            this.lblReleaseVersionChoice.Size = new System.Drawing.Size(116, 17);
            this.lblReleaseVersionChoice.TabIndex = 0;
            this.lblReleaseVersionChoice.Text = "Release Version:";
            // 
            // cmbRelease
            // 
            this.cmbRelease.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRelease.FormattingEnabled = true;
            this.cmbRelease.Location = new System.Drawing.Point(139, 62);
            this.cmbRelease.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbRelease.Name = "cmbRelease";
            this.cmbRelease.Size = new System.Drawing.Size(371, 24);
            this.cmbRelease.TabIndex = 5;
            // 
            // btnOkay
            // 
            this.btnOkay.Location = new System.Drawing.Point(365, 101);
            this.btnOkay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOkay.Name = "btnOkay";
            this.btnOkay.Size = new System.Drawing.Size(79, 30);
            this.btnOkay.TabIndex = 6;
            this.btnOkay.Text = "&OK";
            this.btnOkay.UseVisualStyleBackColor = true;
            this.btnOkay.Click += new System.EventHandler(this.btnOkay_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(453, 101);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(79, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblReleaseVersionSelected
            // 
            this.lblReleaseVersionSelected.AutoSize = true;
            this.lblReleaseVersionSelected.Location = new System.Drawing.Point(27, 41);
            this.lblReleaseVersionSelected.Name = "lblReleaseVersionSelected";
            this.lblReleaseVersionSelected.Size = new System.Drawing.Size(494, 34);
            this.lblReleaseVersionSelected.TabIndex = 8;
            this.lblReleaseVersionSelected.Text = "Deploying version: {0}.\r\nIf this is incorrect please click \'Cancel\', and contact " +
    "an Octopus Administrator.";
            // 
            // lblReleaseSelect
            // 
            this.lblReleaseSelect.AutoSize = true;
            this.lblReleaseSelect.Location = new System.Drawing.Point(19, 15);
            this.lblReleaseSelect.Name = "lblReleaseSelect";
            this.lblReleaseSelect.Size = new System.Drawing.Size(494, 34);
            this.lblReleaseSelect.TabIndex = 9;
            this.lblReleaseSelect.Text = "Please select the appropriate release version.\r\nIf this is incorrect please click" +
    " \'Cancel\', and contact an Octopus Administrator.";
            // 
            // frmOctopusDeploymentManagerVersionSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 186);
            this.ControlBox = false;
            this.Controls.Add(this.lblReleaseSelect);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOkay);
            this.Controls.Add(this.cmbRelease);
            this.Controls.Add(this.lblReleaseVersionChoice);
            this.Controls.Add(this.lblReleaseVersionSelected);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(563, 177);
            this.Name = "frmOctopusDeploymentManagerVersionSelector";
            this.Text = "Version Verification";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblReleaseVersionChoice;
        private System.Windows.Forms.ComboBox cmbRelease;
        private System.Windows.Forms.Button btnOkay;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblReleaseVersionSelected;
        private System.Windows.Forms.Label lblReleaseSelect;
    }
}