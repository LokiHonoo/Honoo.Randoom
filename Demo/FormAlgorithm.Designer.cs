namespace Demo
{
    partial class FormAlgorithm
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
            PanelInput = new System.Windows.Forms.Panel();
            ButtonOK = new System.Windows.Forms.Button();
            ProgressBarStep = new System.Windows.Forms.ProgressBar();
            ComboBoxHashAlgorithm = new System.Windows.Forms.ComboBox();
            LabelSeedTip = new System.Windows.Forms.Label();
            PanelInput.SuspendLayout();
            SuspendLayout();
            // 
            // PanelInput
            // 
            PanelInput.BackColor = System.Drawing.Color.LightSteelBlue;
            PanelInput.Controls.Add(LabelSeedTip);
            PanelInput.Location = new System.Drawing.Point(12, 12);
            PanelInput.Name = "PanelInput";
            PanelInput.Size = new System.Drawing.Size(406, 343);
            PanelInput.TabIndex = 0;
            PanelInput.MouseMove += PanelInput_MouseMove;
            // 
            // ButtonOK
            // 
            ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            ButtonOK.Location = new System.Drawing.Point(343, 390);
            ButtonOK.Name = "ButtonOK";
            ButtonOK.Size = new System.Drawing.Size(75, 32);
            ButtonOK.TabIndex = 3;
            ButtonOK.Text = "确定";
            ButtonOK.UseVisualStyleBackColor = true;
            // 
            // ProgressBarStep
            // 
            ProgressBarStep.Location = new System.Drawing.Point(12, 361);
            ProgressBarStep.Name = "ProgressBarStep";
            ProgressBarStep.Size = new System.Drawing.Size(406, 23);
            ProgressBarStep.Step = 1;
            ProgressBarStep.TabIndex = 1;
            // 
            // ComboBoxHashAlgorithm
            // 
            ComboBoxHashAlgorithm.FormattingEnabled = true;
            ComboBoxHashAlgorithm.Items.AddRange(new object[] { "SHA1", "SHA256", "SHA384", "SHA512" });
            ComboBoxHashAlgorithm.Location = new System.Drawing.Point(12, 395);
            ComboBoxHashAlgorithm.Name = "ComboBoxHashAlgorithm";
            ComboBoxHashAlgorithm.Size = new System.Drawing.Size(121, 25);
            ComboBoxHashAlgorithm.TabIndex = 2;
            ComboBoxHashAlgorithm.SelectedIndexChanged += ComboBoxHashAlgorithm_SelectedIndexChanged;
            // 
            // LabelSeedTip
            // 
            LabelSeedTip.AutoSize = true;
            LabelSeedTip.Location = new System.Drawing.Point(172, 159);
            LabelSeedTip.Name = "LabelSeedTip";
            LabelSeedTip.Size = new System.Drawing.Size(56, 17);
            LabelSeedTip.TabIndex = 0;
            LabelSeedTip.Text = "移动鼠标";
            // 
            // FormAlgorithm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(430, 434);
            Controls.Add(ComboBoxHashAlgorithm);
            Controls.Add(ProgressBarStep);
            Controls.Add(ButtonOK);
            Controls.Add(PanelInput);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormAlgorithm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "算法设置";
            Load += FormAlgorithm_Load;
            PanelInput.ResumeLayout(false);
            PanelInput.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel PanelInput;
        private System.Windows.Forms.Button ButtonOK;
        private System.Windows.Forms.ProgressBar ProgressBarStep;
        private System.Windows.Forms.ComboBox ComboBoxHashAlgorithm;
        private System.Windows.Forms.Label LabelSeedTip;
    }
}