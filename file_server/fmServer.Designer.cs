namespace file_server
{
    partial class fmServer
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
            this.txtCurrentClientIp = new System.Windows.Forms.TextBox();
            this.txtCurrentClientPort = new System.Windows.Forms.TextBox();
            this.txtCurrentFileRequest = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCurrentClientIp
            // 
            this.txtCurrentClientIp.Location = new System.Drawing.Point(90, 12);
            this.txtCurrentClientIp.Name = "txtCurrentClientIp";
            this.txtCurrentClientIp.Size = new System.Drawing.Size(100, 23);
            this.txtCurrentClientIp.TabIndex = 0;
            // 
            // txtCurrentClientPort
            // 
            this.txtCurrentClientPort.Location = new System.Drawing.Point(90, 41);
            this.txtCurrentClientPort.Name = "txtCurrentClientPort";
            this.txtCurrentClientPort.Size = new System.Drawing.Size(100, 23);
            this.txtCurrentClientPort.TabIndex = 1;
            // 
            // txtCurrentFileRequest
            // 
            this.txtCurrentFileRequest.Location = new System.Drawing.Point(90, 70);
            this.txtCurrentFileRequest.Name = "txtCurrentFileRequest";
            this.txtCurrentFileRequest.Size = new System.Drawing.Size(100, 23);
            this.txtCurrentFileRequest.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "client IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "client Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Filename";
            // 
            // fmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 110);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCurrentFileRequest);
            this.Controls.Add(this.txtCurrentClientPort);
            this.Controls.Add(this.txtCurrentClientIp);
            this.Name = "fmServer";
            this.Text = "server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fmServer_FormClosing);
            this.Load += new System.EventHandler(this.fmServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtCurrentClientIp;
        private TextBox txtCurrentClientPort;
        private TextBox txtCurrentFileRequest;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}