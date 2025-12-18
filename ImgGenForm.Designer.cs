using PowerLib.Winform.Controls;
using System.Windows.Forms;

namespace ImageGenerator
{
    partial class ImgGenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImgGenForm));
            this.lblPath = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblFormat = new System.Windows.Forms.Label();
            this.cmbFormat = new System.Windows.Forms.ComboBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.nudSizeValue = new System.Windows.Forms.NumericUpDown();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.picBox = new PowerLib.Winform.Controls.RipplePictureBox();
            this.ripplePictureBox1 = new PowerLib.Winform.Controls.RipplePictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudSizeValue)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPath
            // 
            this.lblPath.Location = new System.Drawing.Point(7, 12);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(47, 14);
            this.lblPath.TabIndex = 0;
            this.lblPath.Text = "保存路径:";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(60, 11);
            this.txtPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(228, 25);
            this.txtPath.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(315, 12);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(140, 25);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "浏览...";
            this.btnBrowse.Click += new System.EventHandler(this.OnBrowseClick);
            // 
            // lblFormat
            // 
            this.lblFormat.Location = new System.Drawing.Point(7, 51);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(47, 19);
            this.lblFormat.TabIndex = 3;
            this.lblFormat.Text = "图片格式:";
            // 
            // cmbFormat
            // 
            this.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormat.Items.AddRange(new object[] {
            "BMP",
            "JPG",
            "PNG"});
            this.cmbFormat.Location = new System.Drawing.Point(60, 49);
            this.cmbFormat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbFormat.Name = "cmbFormat";
            this.cmbFormat.Size = new System.Drawing.Size(81, 23);
            this.cmbFormat.TabIndex = 4;
            // 
            // lblUnit
            // 
            this.lblUnit.Location = new System.Drawing.Point(180, 50);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(47, 19);
            this.lblUnit.TabIndex = 5;
            this.lblUnit.Text = "尺寸单位:";
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.Items.AddRange(new object[] {
            "KB",
            "MB"});
            this.cmbUnit.Location = new System.Drawing.Point(245, 49);
            this.cmbUnit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(55, 23);
            this.cmbUnit.TabIndex = 6;
            // 
            // lblValue
            // 
            this.lblValue.Location = new System.Drawing.Point(325, 51);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(47, 19);
            this.lblValue.TabIndex = 7;
            this.lblValue.Text = "尺寸数值:";
            // 
            // nudSizeValue
            // 
            this.nudSizeValue.Location = new System.Drawing.Point(375, 51);
            this.nudSizeValue.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nudSizeValue.Maximum = new decimal(new int[] {
            102400,
            0,
            0,
            0});
            this.nudSizeValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSizeValue.Name = "nudSizeValue";
            this.nudSizeValue.Size = new System.Drawing.Size(80, 25);
            this.nudSizeValue.TabIndex = 8;
            this.nudSizeValue.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(337, 94);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(117, 26);
            this.btnGenerate.TabIndex = 9;
            this.btnGenerate.Text = "生成图片";
            this.btnGenerate.Click += new System.EventHandler(this.OnGenerateClick);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(57, 82);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(267, 38);
            this.lblStatus.TabIndex = 10;
            // 
            // picBox
            // 
            this.picBox.AnimationEnabled = false;
            this.picBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBox.DragSplashRadius = 10;
            this.picBox.Location = new System.Drawing.Point(0, 0);
            this.picBox.MinimumSize = new System.Drawing.Size(256, 256);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(800, 450);
            this.picBox.TabIndex = 0;
            this.picBox.Text = "ripplePictureBox1";
            // 
            // ripplePictureBox1
            // 
            this.ripplePictureBox1.AnimationEnabled = false;
            this.ripplePictureBox1.DragSplashRadius = 10;
            this.ripplePictureBox1.HoverSplash = true;
            this.ripplePictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("ripplePictureBox1.Image")));
            this.ripplePictureBox1.Location = new System.Drawing.Point(39, 145);
            this.ripplePictureBox1.MinimumSize = new System.Drawing.Size(256, 256);
            this.ripplePictureBox1.Name = "ripplePictureBox1";
            this.ripplePictureBox1.Size = new System.Drawing.Size(412, 256);
            this.ripplePictureBox1.TabIndex = 11;
            this.ripplePictureBox1.Text = "ripplePictureBox1";
            // 
            // ImgGenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 429);
            this.Controls.Add(this.ripplePictureBox1);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lblFormat);
            this.Controls.Add(this.cmbFormat);
            this.Controls.Add(this.lblUnit);
            this.Controls.Add(this.cmbUnit);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.nudSizeValue);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.lblStatus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimizeBox = false;
            this.Name = "ImgGenForm";
            this.Text = "脏宝宝图片生成器";
            ((System.ComponentModel.ISupportInitialize)(this.nudSizeValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RipplePictureBox picBox;
        private TextBox txtPath; // 保存路径输入
        private Button btnBrowse; // 浏览按钮
        private ComboBox cmbFormat; // 图片格式选择
        private ComboBox cmbUnit; // 尺寸单位选择
        private NumericUpDown nudSizeValue; // 尺寸数值输入
        private Button btnGenerate; // 生成按钮
        private Label lblStatus; // 状态显示
        private Label lblPath;
        private Label lblFormat;
        private Label lblUnit;
        private Label lblValue;
        private RipplePictureBox ripplePictureBox1;
    }
}