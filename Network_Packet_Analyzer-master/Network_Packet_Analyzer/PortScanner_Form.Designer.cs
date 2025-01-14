﻿namespace Network_Packet_Analyzer
{
    partial class PortScanner_Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortScanner_Form));
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.txtStartPort = new System.Windows.Forms.TextBox();
            this.txtEndPort = new System.Windows.Forms.TextBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.listResults = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(23, 32);
            this.txtIPAddress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(423, 22);
            this.txtIPAddress.TabIndex = 0;
            // 
            // txtStartPort
            // 
            this.txtStartPort.Location = new System.Drawing.Point(23, 85);
            this.txtStartPort.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtStartPort.Name = "txtStartPort";
            this.txtStartPort.Size = new System.Drawing.Size(193, 22);
            this.txtStartPort.TabIndex = 1;
            this.txtStartPort.Text = "0";
            // 
            // txtEndPort
            // 
            this.txtEndPort.Location = new System.Drawing.Point(252, 85);
            this.txtEndPort.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEndPort.Name = "txtEndPort";
            this.txtEndPort.Size = new System.Drawing.Size(193, 22);
            this.txtEndPort.TabIndex = 2;
            this.txtEndPort.Text = "255";
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(23, 121);
            this.btnScan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(423, 32);
            this.btnScan.TabIndex = 3;
            this.btnScan.Text = "Scan Ports";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // listResults
            // 
            this.listResults.FormattingEnabled = true;
            this.listResults.IntegralHeight = false;
            this.listResults.ItemHeight = 16;
            this.listResults.Location = new System.Drawing.Point(23, 162);
            this.listResults.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listResults.Name = "listResults";
            this.listResults.Size = new System.Drawing.Size(423, 110);
            this.listResults.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "IP Address:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port Range: Start";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(252, 64);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "to";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 285);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(480, 26);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 8;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(50, 20);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // PortScanner_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 311);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listResults);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.txtEndPort);
            this.Controls.Add(this.txtStartPort);
            this.Controls.Add(this.txtIPAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "PortScanner_Form";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Port Scanner";
            this.Load += new System.EventHandler(this.PortScanner_Form_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.TextBox txtStartPort;
        private System.Windows.Forms.TextBox txtEndPort;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.ListBox listResults;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}
