namespace _2312647_LeAnhKhoa_Lab1_Bai3
{
    partial class Form1
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
            this.lblInput = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.btnResolve = new System.Windows.Forms.Button();
            this.lstResult = new System.Windows.Forms.ListBox();
            this.lblIPInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Location = new System.Drawing.Point(20, 20);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(97, 17);
            this.lblInput.TabIndex = 0;
            this.lblInput.Text = "Nhập tên miền:";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(130, 11);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(200, 25);
            this.txtHost.TabIndex = 1;
            // 
            // btnResolve
            // 
            this.btnResolve.Location = new System.Drawing.Point(340, 9);
            this.btnResolve.Name = "btnResolve";
            this.btnResolve.Size = new System.Drawing.Size(100, 30);
            this.btnResolve.TabIndex = 2;
            this.btnResolve.Text = "Phân giải";
            this.btnResolve.UseVisualStyleBackColor = true;
            this.btnResolve.Click += new System.EventHandler(this.btnResolve_Click);
            // 
            // lstResult
            // 
            this.lstResult.FormattingEnabled = true;
            this.lstResult.ItemHeight = 17;
            this.lstResult.Location = new System.Drawing.Point(20, 60);
            this.lstResult.Name = "lstResult";
            this.lstResult.Size = new System.Drawing.Size(420, 140);
            this.lstResult.TabIndex = 3;
            // 
            // lblIPInfo
            // 
            this.lblIPInfo.AutoSize = true;
            this.lblIPInfo.Location = new System.Drawing.Point(20, 220);
            this.lblIPInfo.Name = "lblIPInfo";
            this.lblIPInfo.Size = new System.Drawing.Size(84, 17);
            this.lblIPInfo.TabIndex = 4;
            this.lblIPInfo.Text = "Thông tin IP:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 252);
            this.Controls.Add(this.lblIPInfo);
            this.Controls.Add(this.lstResult);
            this.Controls.Add(this.btnResolve);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.lblInput);
            this.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Button btnResolve;
        private System.Windows.Forms.ListBox lstResult;
        private System.Windows.Forms.Label lblIPInfo;
    }
}

