﻿namespace TestPlatform.Views.SpacialRecognitionPages
{
    partial class SpacialRecognitionExposition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpacialRecognitionExposition));
            this.expositionBW = new System.ComponentModel.BackgroundWorker();
            this.expositionControllerBW = new System.ComponentModel.BackgroundWorker();
            this.instructionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // expositionBW
            // 
            this.expositionBW.WorkerReportsProgress = true;
            this.expositionBW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.expositionBW_DoWork);
            this.expositionBW.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.expositionBW_ProgressChanged);
            this.expositionBW.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.expositionBW_RunWorkerCompleted);
            // 
            // expositionControllerBW
            // 
            this.expositionControllerBW.WorkerReportsProgress = true;
            this.expositionControllerBW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.expositionControllerBW_DoWork);
            this.expositionControllerBW.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.expositionControllerBW_ProgressChanged);
            this.expositionControllerBW.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.expositionControllerBW_RunWorkerCompleted);
            // 
            // instructionLabel
            // 
            this.instructionLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.instructionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.instructionLabel.Name = "instructionLabel";
            this.instructionLabel.Size = new System.Drawing.Size(695, 524);
            this.instructionLabel.TabIndex = 12;
            this.instructionLabel.Text = "instruction";
            this.instructionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.instructionLabel.Visible = false;
            // 
            // SpacialRecognitionExposition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1194, 741);
            this.Controls.Add(this.instructionLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "SpacialRecognitionExposition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SpacialRecognitionExposition";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MatchingExposition_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label instructionLabel;
        private System.ComponentModel.BackgroundWorker expositionBW;
        private System.ComponentModel.BackgroundWorker expositionControllerBW;
    }
}