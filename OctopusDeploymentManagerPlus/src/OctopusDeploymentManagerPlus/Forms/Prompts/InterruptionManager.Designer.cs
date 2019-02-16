using System.Drawing;

namespace OctopusDeploymentManagerPlus.Forms.Prompts
{
    partial class frmInterruptionManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInterruptionManager));
            this.pctBanner = new System.Windows.Forms.PictureBox();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnFail = new System.Windows.Forms.Button();
            this.btnRetry = new System.Windows.Forms.Button();
            this.shpContainer = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lnsTop = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lnsBottom = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtNote = new System.Windows.Forms.TextBox();
            this.rtbError = new System.Windows.Forms.RichTextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.btnViewDeployment = new System.Windows.Forms.Button();
            this.btnCopyToClipboard = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pctBanner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
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
            this.lblSubtitle.Size = new System.Drawing.Size(472, 17);
            this.lblSubtitle.TabIndex = 11;
            this.lblSubtitle.Text = "Please enter a note and determine how to handle the current interruption.";
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
            // btnFail
            // 
            this.btnFail.Location = new System.Drawing.Point(564, 639);
            this.btnFail.Margin = new System.Windows.Forms.Padding(4);
            this.btnFail.Name = "btnFail";
            this.btnFail.Size = new System.Drawing.Size(79, 30);
            this.btnFail.TabIndex = 32;
            this.btnFail.Text = "&Fail";
            this.btnFail.UseVisualStyleBackColor = true;
            this.btnFail.Click += new System.EventHandler(this.btnFail_Click);
            // 
            // btnRetry
            // 
            this.btnRetry.Location = new System.Drawing.Point(477, 639);
            this.btnRetry.Margin = new System.Windows.Forms.Padding(4);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(79, 30);
            this.btnRetry.TabIndex = 31;
            this.btnRetry.Text = "&Retry";
            this.btnRetry.UseVisualStyleBackColor = true;
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
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
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(16, 353);
            this.txtNote.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(621, 239);
            this.txtNote.TabIndex = 34;
            // 
            // rtbError
            // 
            this.rtbError.BackColor = System.Drawing.SystemColors.Control;
            this.rtbError.DetectUrls = false;
            this.rtbError.Location = new System.Drawing.Point(16, 92);
            this.rtbError.Margin = new System.Windows.Forms.Padding(4);
            this.rtbError.Name = "rtbError";
            this.rtbError.ReadOnly = true;
            this.rtbError.Size = new System.Drawing.Size(621, 224);
            this.rtbError.TabIndex = 35;
            this.rtbError.Text = "";
            this.rtbError.WordWrap = false;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(11, 331);
            this.lblNote.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(42, 17);
            this.lblNote.TabIndex = 37;
            this.lblNote.Text = "Note:";
            // 
            // btnViewDeployment
            // 
            this.btnViewDeployment.Location = new System.Drawing.Point(304, 639);
            this.btnViewDeployment.Margin = new System.Windows.Forms.Padding(4);
            this.btnViewDeployment.Name = "btnViewDeployment";
            this.btnViewDeployment.Size = new System.Drawing.Size(165, 30);
            this.btnViewDeployment.TabIndex = 38;
            this.btnViewDeployment.Text = "&View Deployment";
            this.btnViewDeployment.UseVisualStyleBackColor = true;
            this.btnViewDeployment.Click += new System.EventHandler(this.btnViewDeployment_Click);
            // 
            // btnCopyToClipboard
            // 
            this.btnCopyToClipboard.Location = new System.Drawing.Point(16, 640);
            this.btnCopyToClipboard.Margin = new System.Windows.Forms.Padding(4);
            this.btnCopyToClipboard.Name = "btnCopyToClipboard";
            this.btnCopyToClipboard.Size = new System.Drawing.Size(164, 28);
            this.btnCopyToClipboard.TabIndex = 39;
            this.btnCopyToClipboard.Text = "&Copy to Clipboard";
            this.btnCopyToClipboard.UseVisualStyleBackColor = true;
            this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
            // 
            // frmInterruptionManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 668);
            this.ControlBox = false;
            this.Controls.Add(this.btnCopyToClipboard);
            this.Controls.Add(this.btnViewDeployment);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.rtbError);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.btnFail);
            this.Controls.Add(this.btnRetry);
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
            this.Name = "frmInterruptionManager";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Octopus Deployment Manager Plus Interruption";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pctBanner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctBanner;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnFail;
        private System.Windows.Forms.Button btnRetry;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shpContainer;
        private Microsoft.VisualBasic.PowerPacks.LineShape lnsTop;
        private Microsoft.VisualBasic.PowerPacks.LineShape lnsBottom;
        public System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.RichTextBox rtbError;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Button btnViewDeployment;
        private System.Windows.Forms.Button btnCopyToClipboard;
    }
}