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
            ComboBoxToken = new ComboBox();
            ListViewResult = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            label1 = new Label();
            label2 = new Label();
            NumericUpDownCharCount = new NumericUpDown();
            ButtonGenerate = new Button();
            label3 = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            ButtonMarkDescription = new Button();
            ComboBoxMark = new ComboBox();
            RadioButtonGeneral = new RadioButton();
            RadioButtonMark = new RadioButton();
            NumericUpDownGenerateCount = new NumericUpDown();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownCharCount).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownGenerateCount).BeginInit();
            SuspendLayout();
            // 
            // ComboBoxToken
            // 
            ComboBoxToken.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxToken.FormattingEnabled = true;
            ComboBoxToken.Location = new Point(84, 22);
            ComboBoxToken.Name = "ComboBoxToken";
            ComboBoxToken.Size = new Size(250, 25);
            ComboBoxToken.TabIndex = 0;
            // 
            // ListViewResult
            // 
            ListViewResult.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            ListViewResult.FullRowSelect = true;
            ListViewResult.GridLines = true;
            ListViewResult.Location = new Point(12, 293);
            ListViewResult.Name = "ListViewResult";
            ListViewResult.Size = new Size(467, 210);
            ListViewResult.TabIndex = 1;
            ListViewResult.UseCompatibleStateImageBehavior = false;
            ListViewResult.View = View.Details;
            ListViewResult.ItemActivate += ListViewResult_ItemActivate;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "序号";
            columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "随机字符串 (双击复制)";
            columnHeader2.Width = 380;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 26);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 2;
            label1.Text = "字符范围";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(340, 26);
            label2.Name = "label2";
            label2.Size = new Size(56, 17);
            label2.TabIndex = 3;
            label2.Text = "字符个数";
            // 
            // NumericUpDownCharCount
            // 
            NumericUpDownCharCount.Location = new Point(402, 23);
            NumericUpDownCharCount.Name = "NumericUpDownCharCount";
            NumericUpDownCharCount.Size = new Size(59, 23);
            NumericUpDownCharCount.TabIndex = 4;
            NumericUpDownCharCount.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // ButtonGenerate
            // 
            ButtonGenerate.Location = new Point(193, 242);
            ButtonGenerate.Name = "ButtonGenerate";
            ButtonGenerate.Size = new Size(101, 45);
            ButtonGenerate.TabIndex = 5;
            ButtonGenerate.Text = "生 成";
            ButtonGenerate.UseVisualStyleBackColor = true;
            ButtonGenerate.Click += ButtonGenerate_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 64);
            label3.Name = "label3";
            label3.Size = new Size(32, 17);
            label3.TabIndex = 6;
            label3.Text = "掩码";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(NumericUpDownCharCount);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(ComboBoxToken);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 39);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(467, 66);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "设置";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ButtonMarkDescription);
            groupBox2.Controls.Add(ComboBoxMark);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new Point(12, 138);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(467, 98);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "设置";
            // 
            // ButtonMarkDescription
            // 
            ButtonMarkDescription.Location = new Point(6, 22);
            ButtonMarkDescription.Name = "ButtonMarkDescription";
            ButtonMarkDescription.Size = new Size(97, 32);
            ButtonMarkDescription.TabIndex = 8;
            ButtonMarkDescription.Text = "掩码说明";
            ButtonMarkDescription.UseVisualStyleBackColor = true;
            ButtonMarkDescription.Click += ButtonMarkDescription_Click;
            // 
            // ComboBoxMark
            // 
            ComboBoxMark.FormattingEnabled = true;
            ComboBoxMark.Location = new Point(60, 60);
            ComboBoxMark.Name = "ComboBoxMark";
            ComboBoxMark.Size = new Size(401, 25);
            ComboBoxMark.TabIndex = 7;
            // 
            // RadioButtonGeneral
            // 
            RadioButtonGeneral.AutoSize = true;
            RadioButtonGeneral.Checked = true;
            RadioButtonGeneral.Location = new Point(12, 12);
            RadioButtonGeneral.Name = "RadioButtonGeneral";
            RadioButtonGeneral.Size = new Size(50, 21);
            RadioButtonGeneral.TabIndex = 10;
            RadioButtonGeneral.TabStop = true;
            RadioButtonGeneral.Text = "常规";
            RadioButtonGeneral.UseVisualStyleBackColor = true;
            // 
            // RadioButtonMark
            // 
            RadioButtonMark.AutoSize = true;
            RadioButtonMark.Location = new Point(12, 111);
            RadioButtonMark.Name = "RadioButtonMark";
            RadioButtonMark.Size = new Size(50, 21);
            RadioButtonMark.TabIndex = 11;
            RadioButtonMark.Text = "掩码";
            RadioButtonMark.UseVisualStyleBackColor = true;
            // 
            // NumericUpDownGenerateCount
            // 
            NumericUpDownGenerateCount.Location = new Point(343, 253);
            NumericUpDownGenerateCount.Name = "NumericUpDownGenerateCount";
            NumericUpDownGenerateCount.Size = new Size(74, 23);
            NumericUpDownGenerateCount.TabIndex = 12;
            NumericUpDownGenerateCount.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(305, 256);
            label4.Name = "label4";
            label4.Size = new Size(32, 17);
            label4.TabIndex = 13;
            label4.Text = "数量";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(491, 515);
            Controls.Add(label4);
            Controls.Add(NumericUpDownGenerateCount);
            Controls.Add(RadioButtonMark);
            Controls.Add(RadioButtonGeneral);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(ButtonGenerate);
            Controls.Add(ListViewResult);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormMain";
            Text = "Honoo.Randoom DEMO";
            Load += FormMain_Load;
            ((System.ComponentModel.ISupportInitialize)NumericUpDownCharCount).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownGenerateCount).EndInit();
            ResumeLayout(false);
            PerformLayout();
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