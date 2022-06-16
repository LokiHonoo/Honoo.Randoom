namespace Demo
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ComboBoxToken = new System.Windows.Forms.ComboBox();
            this.ListViewResult = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.NumericUpDownCharCount = new System.Windows.Forms.NumericUpDown();
            this.ButtonGenerate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ButtonMarkDescription = new System.Windows.Forms.Button();
            this.ComboBoxMark = new System.Windows.Forms.ComboBox();
            this.RadioButtonGeneral = new System.Windows.Forms.RadioButton();
            this.RadioButtonMark = new System.Windows.Forms.RadioButton();
            this.NumericUpDownGenerateCount = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownCharCount)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownGenerateCount)).BeginInit();
            this.SuspendLayout();
            // 
            // ComboBoxToken
            // 
            this.ComboBoxToken.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxToken.FormattingEnabled = true;
            this.ComboBoxToken.Location = new System.Drawing.Point(84, 22);
            this.ComboBoxToken.Name = "ComboBoxToken";
            this.ComboBoxToken.Size = new System.Drawing.Size(250, 25);
            this.ComboBoxToken.TabIndex = 0;
            // 
            // ListViewResult
            // 
            this.ListViewResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.ListViewResult.FullRowSelect = true;
            this.ListViewResult.GridLines = true;
            this.ListViewResult.Location = new System.Drawing.Point(12, 293);
            this.ListViewResult.Name = "ListViewResult";
            this.ListViewResult.Size = new System.Drawing.Size(467, 210);
            this.ListViewResult.TabIndex = 1;
            this.ListViewResult.UseCompatibleStateImageBehavior = false;
            this.ListViewResult.View = System.Windows.Forms.View.Details;
            this.ListViewResult.ItemActivate += new System.EventHandler(this.ListViewResult_ItemActivate);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "随机字符串 (双击复制)";
            this.columnHeader2.Width = 380;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "字符范围";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(340, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "字符个数";
            // 
            // NumericUpDownCharCount
            // 
            this.NumericUpDownCharCount.Location = new System.Drawing.Point(402, 23);
            this.NumericUpDownCharCount.Name = "NumericUpDownCharCount";
            this.NumericUpDownCharCount.Size = new System.Drawing.Size(59, 23);
            this.NumericUpDownCharCount.TabIndex = 4;
            this.NumericUpDownCharCount.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // ButtonGenerate
            // 
            this.ButtonGenerate.Location = new System.Drawing.Point(193, 242);
            this.ButtonGenerate.Name = "ButtonGenerate";
            this.ButtonGenerate.Size = new System.Drawing.Size(101, 45);
            this.ButtonGenerate.TabIndex = 5;
            this.ButtonGenerate.Text = "生 成";
            this.ButtonGenerate.UseVisualStyleBackColor = true;
            this.ButtonGenerate.Click += new System.EventHandler(this.ButtonGenerate_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "掩码";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NumericUpDownCharCount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ComboBoxToken);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(467, 66);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ButtonMarkDescription);
            this.groupBox2.Controls.Add(this.ComboBoxMark);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(467, 98);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置";
            // 
            // ButtonMarkDescription
            // 
            this.ButtonMarkDescription.Location = new System.Drawing.Point(6, 22);
            this.ButtonMarkDescription.Name = "ButtonMarkDescription";
            this.ButtonMarkDescription.Size = new System.Drawing.Size(97, 32);
            this.ButtonMarkDescription.TabIndex = 8;
            this.ButtonMarkDescription.Text = "掩码说明";
            this.ButtonMarkDescription.UseVisualStyleBackColor = true;
            this.ButtonMarkDescription.Click += new System.EventHandler(this.ButtonMarkDescription_Click);
            // 
            // ComboBoxMark
            // 
            this.ComboBoxMark.FormattingEnabled = true;
            this.ComboBoxMark.Location = new System.Drawing.Point(60, 60);
            this.ComboBoxMark.Name = "ComboBoxMark";
            this.ComboBoxMark.Size = new System.Drawing.Size(401, 25);
            this.ComboBoxMark.TabIndex = 7;
            // 
            // RadioButtonGeneral
            // 
            this.RadioButtonGeneral.AutoSize = true;
            this.RadioButtonGeneral.Checked = true;
            this.RadioButtonGeneral.Location = new System.Drawing.Point(12, 12);
            this.RadioButtonGeneral.Name = "RadioButtonGeneral";
            this.RadioButtonGeneral.Size = new System.Drawing.Size(50, 21);
            this.RadioButtonGeneral.TabIndex = 10;
            this.RadioButtonGeneral.TabStop = true;
            this.RadioButtonGeneral.Text = "常规";
            this.RadioButtonGeneral.UseVisualStyleBackColor = true;
            // 
            // RadioButtonMark
            // 
            this.RadioButtonMark.AutoSize = true;
            this.RadioButtonMark.Location = new System.Drawing.Point(12, 111);
            this.RadioButtonMark.Name = "RadioButtonMark";
            this.RadioButtonMark.Size = new System.Drawing.Size(50, 21);
            this.RadioButtonMark.TabIndex = 11;
            this.RadioButtonMark.Text = "掩码";
            this.RadioButtonMark.UseVisualStyleBackColor = true;
            // 
            // NumericUpDownGenerateCount
            // 
            this.NumericUpDownGenerateCount.Location = new System.Drawing.Point(343, 253);
            this.NumericUpDownGenerateCount.Name = "NumericUpDownGenerateCount";
            this.NumericUpDownGenerateCount.Size = new System.Drawing.Size(74, 23);
            this.NumericUpDownGenerateCount.TabIndex = 12;
            this.NumericUpDownGenerateCount.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(305, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "数量";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 515);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.NumericUpDownGenerateCount);
            this.Controls.Add(this.RadioButtonMark);
            this.Controls.Add(this.RadioButtonGeneral);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ButtonGenerate);
            this.Controls.Add(this.ListViewResult);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "Honoo.Randoom DEMO";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownCharCount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownGenerateCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox ComboBoxToken;
        private ListView ListViewResult;
        private Label label1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Label label2;
        private NumericUpDown NumericUpDownCharCount;
        private Button ButtonGenerate;
        private Label label3;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private RadioButton RadioButtonGeneral;
        private RadioButton RadioButtonMark;
        private NumericUpDown NumericUpDownGenerateCount;
        private Label label4;
        private ComboBox ComboBoxMark;
        private Button ButtonMarkDescription;
    }
}