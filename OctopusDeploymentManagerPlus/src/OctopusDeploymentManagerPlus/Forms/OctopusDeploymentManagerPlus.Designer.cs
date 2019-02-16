using System.Drawing;

namespace OctopusDeploymentManagerPlus.Forms
{
    partial class frmOctopusDeploymentManagerPlus
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOctopusDeploymentManagerPlus));
            this.pctBanner = new System.Windows.Forms.PictureBox();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblInfoText = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDeploy = new System.Windows.Forms.Button();
            this.shpContainer = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lnsTop = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lnsBottom = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.lblEnvironment = new System.Windows.Forms.Label();
            this.cmbEnvironment = new System.Windows.Forms.ComboBox();
            this.lblDeploymentType = new System.Windows.Forms.Label();
            this.cmbDeploymentType = new System.Windows.Forms.ComboBox();
            this.dtpDeploymentDate = new System.Windows.Forms.DateTimePicker();
            this.dtpDeploymentTime = new System.Windows.Forms.DateTimePicker();
            this.rdbNow = new System.Windows.Forms.RadioButton();
            this.rdbLater = new System.Windows.Forms.RadioButton();
            this.lblDeploymentTime = new System.Windows.Forms.Label();
            this.grpDeploymentInformation = new System.Windows.Forms.GroupBox();
            this.hlbDeploymentTypeText = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pctBanner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpDeploymentInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // pctBanner
            // 
            this.pctBanner.Image = global::OctopusDeploymentManagerPlus.Properties.Resources.banner;
            this.pctBanner.Location = new System.Drawing.Point(0, 0);
            this.pctBanner.Margin = new System.Windows.Forms.Padding(4);
            this.pctBanner.Name = "pctBanner";
            this.pctBanner.Size = new System.Drawing.Size(657, 71);
            this.pctBanner.TabIndex = 0;
            this.pctBanner.TabStop = false;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.BackColor = System.Drawing.Color.White;
            this.lblSubtitle.Location = new System.Drawing.Point(33, 36);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(515, 17);
            this.lblSubtitle.TabIndex = 11;
            this.lblSubtitle.Text = "Runs the selected deployment type against the chosen environment and project.";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.White;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(20, 11);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(267, 18);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Octopus Deployment Manager Plus";
            // 
            // lblInfoText
            // 
            this.lblInfoText.Location = new System.Drawing.Point(16, 89);
            this.lblInfoText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfoText.Name = "lblInfoText";
            this.lblInfoText.Size = new System.Drawing.Size(627, 69);
            this.lblInfoText.TabIndex = 12;
            this.lblInfoText.Text = "Enter a company code, choose the environment, deployment type, and time to deploy" +
    ".";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(564, 639);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(79, 30);
            this.btnExit.TabIndex = 32;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDeploy
            // 
            this.btnDeploy.Location = new System.Drawing.Point(477, 639);
            this.btnDeploy.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeploy.Name = "btnDeploy";
            this.btnDeploy.Size = new System.Drawing.Size(79, 30);
            this.btnDeploy.TabIndex = 31;
            this.btnDeploy.Text = "&Deploy";
            this.btnDeploy.UseVisualStyleBackColor = true;
            this.btnDeploy.Click += new System.EventHandler(this.btnDeploy_Click);
            // 
            // shpContainer
            // 
            this.shpContainer.Location = new System.Drawing.Point(0, 0);
            this.shpContainer.Margin = new System.Windows.Forms.Padding(0);
            this.shpContainer.Name = "shpContainer";
            this.shpContainer.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lnsTop,
            this.lnsBottom});
            this.shpContainer.Size = new System.Drawing.Size(656, 668);
            this.shpContainer.TabIndex = 33;
            this.shpContainer.TabStop = false;
            // 
            // lnsTop
            // 
            this.lnsTop.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.lnsTop.Name = "lnsTop";
            this.lnsTop.X1 = 0;
            this.lnsTop.X2 = 493;
            this.lnsTop.Y1 = 58;
            this.lnsTop.Y2 = 58;
            // 
            // lnsBottom
            // 
            this.lnsBottom.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.lnsBottom.Name = "lnsBottom";
            this.lnsBottom.X1 = 0;
            this.lnsBottom.X2 = 493;
            this.lnsBottom.Y1 = 501;
            this.lnsBottom.Y2 = 501;
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            this.errorProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider.Icon")));
            // 
            // txtProjectName
            // 
            this.txtProjectName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtProjectName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtProjectName.Location = new System.Drawing.Point(171, 36);
            this.txtProjectName.Margin = new System.Windows.Forms.Padding(4);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(371, 22);
            this.txtProjectName.TabIndex = 0;
            this.txtProjectName.Leave += new System.EventHandler(this.txtProjectName_Leave);
            // 
            // lblProjectName
            // 
            this.lblProjectName.AutoSize = true;
            this.lblProjectName.Location = new System.Drawing.Point(32, 39);
            this.lblProjectName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(97, 17);
            this.lblProjectName.TabIndex = 2;
            this.lblProjectName.Text = "Project Name:";
            // 
            // lblEnvironment
            // 
            this.lblEnvironment.AutoSize = true;
            this.lblEnvironment.Location = new System.Drawing.Point(41, 68);
            this.lblEnvironment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnvironment.Name = "lblEnvironment";
            this.lblEnvironment.Size = new System.Drawing.Size(91, 17);
            this.lblEnvironment.TabIndex = 3;
            this.lblEnvironment.Text = "Environment:";
            // 
            // cmbEnvironment
            // 
            this.cmbEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnvironment.FormattingEnabled = true;
            this.cmbEnvironment.Location = new System.Drawing.Point(171, 64);
            this.cmbEnvironment.Margin = new System.Windows.Forms.Padding(4);
            this.cmbEnvironment.Name = "cmbEnvironment";
            this.cmbEnvironment.Size = new System.Drawing.Size(371, 24);
            this.cmbEnvironment.TabIndex = 4;
            // 
            // lblDeploymentType
            // 
            this.lblDeploymentType.AutoSize = true;
            this.lblDeploymentType.Location = new System.Drawing.Point(25, 97);
            this.lblDeploymentType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDeploymentType.Name = "lblDeploymentType";
            this.lblDeploymentType.Size = new System.Drawing.Size(123, 17);
            this.lblDeploymentType.TabIndex = 5;
            this.lblDeploymentType.Text = "Deployment Type:";
            // 
            // cmbDeploymentType
            // 
            this.cmbDeploymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeploymentType.FormattingEnabled = true;
            this.cmbDeploymentType.Location = new System.Drawing.Point(171, 94);
            this.cmbDeploymentType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDeploymentType.Name = "cmbDeploymentType";
            this.cmbDeploymentType.Size = new System.Drawing.Size(371, 24);
            this.cmbDeploymentType.TabIndex = 6;
            // 
            // dtpDeploymentDate
            // 
            this.dtpDeploymentDate.Enabled = false;
            this.dtpDeploymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDeploymentDate.Location = new System.Drawing.Point(315, 123);
            this.dtpDeploymentDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDeploymentDate.Name = "dtpDeploymentDate";
            this.dtpDeploymentDate.Size = new System.Drawing.Size(125, 22);
            this.dtpDeploymentDate.TabIndex = 35;
            this.dtpDeploymentDate.ValueChanged += new System.EventHandler(this.dtpDeploymentDate_ValueChanged);
            // 
            // dtpDeploymentTime
            // 
            this.dtpDeploymentTime.CustomFormat = "hh:mm tt";
            this.dtpDeploymentTime.Enabled = false;
            this.dtpDeploymentTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDeploymentTime.Location = new System.Drawing.Point(449, 123);
            this.dtpDeploymentTime.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDeploymentTime.Name = "dtpDeploymentTime";
            this.dtpDeploymentTime.ShowUpDown = true;
            this.dtpDeploymentTime.Size = new System.Drawing.Size(92, 22);
            this.dtpDeploymentTime.TabIndex = 36;
            this.dtpDeploymentTime.ValueChanged += new System.EventHandler(this.dtpDeploymentTime_ValueChanged);
            // 
            // rdbNow
            // 
            this.rdbNow.AutoSize = true;
            this.rdbNow.Checked = true;
            this.rdbNow.Location = new System.Drawing.Point(171, 126);
            this.rdbNow.Margin = new System.Windows.Forms.Padding(4);
            this.rdbNow.Name = "rdbNow";
            this.rdbNow.Size = new System.Drawing.Size(56, 21);
            this.rdbNow.TabIndex = 38;
            this.rdbNow.TabStop = true;
            this.rdbNow.Text = "Now";
            this.rdbNow.UseVisualStyleBackColor = true;
            this.rdbNow.Click += new System.EventHandler(this.rdbNow_Click);
            // 
            // rdbLater
            // 
            this.rdbLater.AutoSize = true;
            this.rdbLater.Location = new System.Drawing.Point(241, 126);
            this.rdbLater.Margin = new System.Windows.Forms.Padding(4);
            this.rdbLater.Name = "rdbLater";
            this.rdbLater.Size = new System.Drawing.Size(62, 21);
            this.rdbLater.TabIndex = 39;
            this.rdbLater.Text = "Later";
            this.rdbLater.UseVisualStyleBackColor = true;
            this.rdbLater.Click += new System.EventHandler(this.rdbLater_Click);
            // 
            // lblDeploymentTime
            // 
            this.lblDeploymentTime.AutoSize = true;
            this.lblDeploymentTime.Location = new System.Drawing.Point(25, 128);
            this.lblDeploymentTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDeploymentTime.Name = "lblDeploymentTime";
            this.lblDeploymentTime.Size = new System.Drawing.Size(122, 17);
            this.lblDeploymentTime.TabIndex = 40;
            this.lblDeploymentTime.Text = "Deployment Time:";
            // 
            // grpDeploymentInformation
            // 
            this.grpDeploymentInformation.Controls.Add(this.lblDeploymentTime);
            this.grpDeploymentInformation.Controls.Add(this.rdbLater);
            this.grpDeploymentInformation.Controls.Add(this.rdbNow);
            this.grpDeploymentInformation.Controls.Add(this.dtpDeploymentTime);
            this.grpDeploymentInformation.Controls.Add(this.dtpDeploymentDate);
            this.grpDeploymentInformation.Controls.Add(this.cmbDeploymentType);
            this.grpDeploymentInformation.Controls.Add(this.lblDeploymentType);
            this.grpDeploymentInformation.Controls.Add(this.cmbEnvironment);
            this.grpDeploymentInformation.Controls.Add(this.lblEnvironment);
            this.grpDeploymentInformation.Controls.Add(this.lblProjectName);
            this.grpDeploymentInformation.Controls.Add(this.txtProjectName);
            this.grpDeploymentInformation.Location = new System.Drawing.Point(16, 161);
            this.grpDeploymentInformation.Margin = new System.Windows.Forms.Padding(4);
            this.grpDeploymentInformation.Name = "grpDeploymentInformation";
            this.grpDeploymentInformation.Padding = new System.Windows.Forms.Padding(4);
            this.grpDeploymentInformation.Size = new System.Drawing.Size(623, 162);
            this.grpDeploymentInformation.TabIndex = 4;
            this.grpDeploymentInformation.TabStop = false;
            this.grpDeploymentInformation.Text = "Deployment Information:";
            // 
            // hlbDeploymentTypeText
            // 
            this.hlbDeploymentTypeText.AutoSize = false;
            this.hlbDeploymentTypeText.BackColor = System.Drawing.SystemColors.Control;
            this.hlbDeploymentTypeText.BaseStylesheet = null;
            this.hlbDeploymentTypeText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.hlbDeploymentTypeText.IsContextMenuEnabled = false;
            this.hlbDeploymentTypeText.Location = new System.Drawing.Point(16, 330);
            this.hlbDeploymentTypeText.Name = "hlbDeploymentTypeText";
            this.hlbDeploymentTypeText.Size = new System.Drawing.Size(623, 214);
            this.hlbDeploymentTypeText.TabIndex = 35;
            this.hlbDeploymentTypeText.Text = null;
            // 
            // frmOctopusDeploymentManagerPlus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 668);
            this.Controls.Add(this.hlbDeploymentTypeText);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDeploy);
            this.Controls.Add(this.lblInfoText);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.grpDeploymentInformation);
            this.Controls.Add(this.pctBanner);
            this.Controls.Add(this.shpContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(674, 715);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(674, 715);
            this.Name = "frmOctopusDeploymentManagerPlus";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Octopus Deployment Manager Plus";
            this.Shown += new System.EventHandler(this.frmOctopusDeploymentManager_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pctBanner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpDeploymentInformation.ResumeLayout(false);
            this.grpDeploymentInformation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctBanner;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInfoText;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDeploy;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shpContainer;
        private Microsoft.VisualBasic.PowerPacks.LineShape lnsTop;
        private Microsoft.VisualBasic.PowerPacks.LineShape lnsBottom;
        private System.Windows.Forms.Label lblDeploymentTime;
        private System.Windows.Forms.RadioButton rdbLater;
        private System.Windows.Forms.RadioButton rdbNow;
        private System.Windows.Forms.DateTimePicker dtpDeploymentTime;
        private System.Windows.Forms.DateTimePicker dtpDeploymentDate;
        private System.Windows.Forms.ComboBox cmbDeploymentType;
        private System.Windows.Forms.Label lblDeploymentType;
        private System.Windows.Forms.ComboBox cmbEnvironment;
        private System.Windows.Forms.Label lblEnvironment;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.TextBox txtProjectName;
        public System.Windows.Forms.ErrorProvider errorProvider;
        internal System.Windows.Forms.GroupBox grpDeploymentInformation;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel hlbDeploymentTypeText;
    }
}