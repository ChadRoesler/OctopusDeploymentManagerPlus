using System.Drawing;

namespace OctopusDeploymentManagerPlus.Forms
{
    partial class frmOctopusDeploymentManagerPlusStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOctopusDeploymentManagerPlusStatus));
            this.pctBanner = new System.Windows.Forms.PictureBox();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.shpContainer = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lnsTop = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lnsBottom = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.rtbOutputConsole = new System.Windows.Forms.RichTextBox();
            this.lblProgress = new System.Windows.Forms.Label();
            this.btnViewDeployment = new System.Windows.Forms.Button();
            this.btnCancelDeployment = new System.Windows.Forms.Button();
            this.progressDeployment = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pctBanner)).BeginInit();
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
            this.lblSubtitle.Size = new System.Drawing.Size(519, 17);
            this.lblSubtitle.TabIndex = 11;
            this.lblSubtitle.Text = "This tool will deploy specific types to the chosen environment and company code.";
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
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(24, 639);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(79, 30);
            this.btnBack.TabIndex = 31;
            this.btnBack.Text = "&Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
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
            // rtbOutputConsole
            // 
            this.rtbOutputConsole.DetectUrls = false;
            this.rtbOutputConsole.Location = new System.Drawing.Point(24, 175);
            this.rtbOutputConsole.Margin = new System.Windows.Forms.Padding(4);
            this.rtbOutputConsole.Name = "rtbOutputConsole";
            this.rtbOutputConsole.ReadOnly = true;
            this.rtbOutputConsole.Size = new System.Drawing.Size(617, 421);
            this.rtbOutputConsole.TabIndex = 36;
            this.rtbOutputConsole.Text = "";
            this.rtbOutputConsole.WordWrap = false;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(16, 80);
            this.lblProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(84, 17);
            this.lblProgress.TabIndex = 35;
            this.lblProgress.Text = "Applying {0}";
            // 
            // btnViewDeployment
            // 
            this.btnViewDeployment.Location = new System.Drawing.Point(391, 639);
            this.btnViewDeployment.Margin = new System.Windows.Forms.Padding(4);
            this.btnViewDeployment.Name = "btnViewDeployment";
            this.btnViewDeployment.Size = new System.Drawing.Size(165, 30);
            this.btnViewDeployment.TabIndex = 37;
            this.btnViewDeployment.Text = "&View Deployment";
            this.btnViewDeployment.UseVisualStyleBackColor = true;
            this.btnViewDeployment.Click += new System.EventHandler(this.btnViewDeployment_Click);
            // 
            // btnCancelDeployment
            // 
            this.btnCancelDeployment.Location = new System.Drawing.Point(111, 639);
            this.btnCancelDeployment.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelDeployment.Name = "btnCancelDeployment";
            this.btnCancelDeployment.Size = new System.Drawing.Size(165, 30);
            this.btnCancelDeployment.TabIndex = 39;
            this.btnCancelDeployment.Text = "Cancel Deployment";
            this.btnCancelDeployment.UseVisualStyleBackColor = true;
            this.btnCancelDeployment.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // progressDeployment
            // 
            this.progressDeployment.Location = new System.Drawing.Point(24, 119);
            this.progressDeployment.Margin = new System.Windows.Forms.Padding(4);
            this.progressDeployment.Name = "progressDeployment";
            this.progressDeployment.Size = new System.Drawing.Size(619, 28);
            this.progressDeployment.TabIndex = 40;
            // 
            // frmOctopusDeploymentManagerPlusStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 668);
            this.Controls.Add(this.progressDeployment);
            this.Controls.Add(this.btnCancelDeployment);
            this.Controls.Add(this.btnViewDeployment);
            this.Controls.Add(this.rtbOutputConsole);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pctBanner);
            this.Controls.Add(this.shpContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(674, 715);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(674, 715);
            this.Name = "frmOctopusDeploymentManagerPlusStatus";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Octopus Deployment Manager Plus";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOctopusDeploymentManagerStatus_FormClosing);
            this.Shown += new System.EventHandler(this.frmOctopusDeploymentManagerStatus_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pctBanner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctBanner;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnBack;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shpContainer;
        private Microsoft.VisualBasic.PowerPacks.LineShape lnsTop;
        private Microsoft.VisualBasic.PowerPacks.LineShape lnsBottom;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Button btnViewDeployment;
        private System.Windows.Forms.RichTextBox rtbOutputConsole;
        private System.Windows.Forms.Button btnCancelDeployment;
        private System.Windows.Forms.ProgressBar progressDeployment;
    }
}
