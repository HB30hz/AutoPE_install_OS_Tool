
namespace MyDeployTool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblRecommendSystem;
        private System.Windows.Forms.Label lblImagePath;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Timer timerAutoDeploy;
        private System.Windows.Forms.Label lblCPU;
        private System.Windows.Forms.Label lblMotherboard;
        private System.Windows.Forms.Label lblMemory;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        //<param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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

            lblCPU = new Label();
            lblMotherboard = new Label();
            lblMemory = new Label();
            SuspendLayout();
            // 
            // CPU
            // 
            lblCPU.AutoSize = true;
            lblCPU.Location = new Point(50, 50);
            lblCPU.Name = "lblCPU";
            lblCPU.Size = new Size(39, 17);
            lblCPU.TabIndex = 0;
            lblCPU.Text = "CPU: ";
            // 
            // 主板
            // 
            lblMotherboard.AutoSize = true;
            lblMotherboard.Location = new Point(50, 100);
            lblMotherboard.Name = "lblMotherboard";
            lblMotherboard.Size = new Size(39, 17);
            lblMotherboard.TabIndex = 1;
            lblMotherboard.Text = "主板: ";
            // 
            // 内存
            // 
            lblMemory.AutoSize = true;
            lblMemory.Location = new Point(50, 150);
            lblMemory.Name = "lblMemory";
            lblMemory.Size = new Size(39, 17);
            lblMemory.TabIndex = 2;
            lblMemory.Text = "内存: ";

            //
            // 推荐系统
            //
            this.lblRecommendSystem = new System.Windows.Forms.Label();
            this.lblRecommendSystem.Location = new System.Drawing.Point(50, 200);
            this.lblRecommendSystem.Size = new System.Drawing.Size(400,20);

            //
            // 镜像路径
            //
            this.lblImagePath = new System.Windows.Forms.Label();
            this.lblImagePath.Location = new System.Drawing.Point(50, 230);
            this.lblImagePath.Size = new System.Drawing.Size(600, 20);

            //
            // 部署进度
            //
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.progressBar.Location = new System.Drawing.Point(50, 260);
            this.progressBar.Size = new System.Drawing.Size(600, 30);
            this.Controls.Add(this.progressBar);

            //
            // 部署状态
            //
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStatus.Location = new System.Drawing.Point(50, 300);
            this.lblStatus.Size = new System.Drawing.Size(600, 20);
            this.Controls.Add(this.lblStatus);

            //
            // 取消部署按钮
            //
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCancel.Location = new System.Drawing.Point(50, 330);
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.Text = "取消部署";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            //定时器
            this.timerAutoDeploy = new System.Windows.Forms.Timer(this.components);
            this.timerAutoDeploy.Interval = 3000; //3秒
            this.timerAutoDeploy.Tick += new System.EventHandler(this.timerAutoDeploy_Tick);

            //添加控件
            this.Controls.Add(this.lblRecommendSystem);
            this.Controls.Add(this.lblImagePath);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCancel);
            

            // 
            // MainForm
            // 
            ClientSize = new Size(800, 450);
            Controls.Add(lblMemory);
            Controls.Add(lblMotherboard);
            Controls.Add(lblCPU);
            Name = "MainForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}