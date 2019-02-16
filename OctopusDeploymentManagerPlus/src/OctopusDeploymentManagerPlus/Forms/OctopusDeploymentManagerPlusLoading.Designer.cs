using System.Drawing;

namespace OctopusDeploymentManagerPlus.Forms
{
    partial class frmOctopusDeploymentManagerPlusLoading
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOctopusDeploymentManagerPlusLoading));
            this.pctBanner = new System.Windows.Forms.PictureBox();
            this.lblLoading = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pctBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // pctBanner
            // 
            this.pctBanner.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pctBanner.Image = global::OctopusDeploymentManagerPlus.Properties.Resources.banner;
            this.pctBanner.Location = new System.Drawing.Point(0, 0);
            this.pctBanner.Margin = new System.Windows.Forms.Padding(4);
            this.pctBanner.Name = "pctBanner";
            this.pctBanner.Size = new System.Drawing.Size(656, 70);
            this.pctBanner.TabIndex = 1;
            this.pctBanner.TabStop = false;
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.BackColor = System.Drawing.Color.White;
            this.lblLoading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoading.Location = new System.Drawing.Point(108, 27);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(392, 20);
            this.lblLoading.TabIndex = 2;
            this.lblLoading.Text = "Loading Octopus Deployment Manager Plus...";
            // 
            // frmOctopusDeploymentManagerPlusLoading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 71);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.pctBanner);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmOctopusDeploymentManagerPlusLoading";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Octopus Deployment Manager Plus";
            ((System.ComponentModel.ISupportInitialize)(this.pctBanner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctBanner;
        private System.Windows.Forms.Label lblLoading;
    }
}