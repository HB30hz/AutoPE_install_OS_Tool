using MyDeployTool.Deployment;
using MyDeployTool.Hardware;
using MyDeployTool.Rules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Formats;
using MyDeployTool;

namespace MyDeployTool
{
    //主窗体
    public partial class MainForm : Form
    {
        
        private readonly HardwareScanner _hardware;
        private readonly RuleEngine _ruleEngine;
        private readonly BackupManager _backupManager;
        private string _imagePath;

        public MainForm()
        {
            InitializeComponent();

            // 初始化依赖对象
            _hardware = new HardwareScanner();
            _ruleEngine = new RuleEngine(); // 默认加载 rules.json
            _backupManager = new BackupManager();

            // 自动加载硬件信息并显示推荐系统
            LoadHardwareInfo();
            ShowRecommendedSystem();
            
            
        }

        // 释放托管资源
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _hardware?.Dispose();
        //      //_backupManager?.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}


        //加载硬件信息并显示
        private void LoadHardwareInfo()
        {
            /*
             //调试
             string cpuInfo = hardware.GetCPUInfo();
             string motherboardInfo = hardware.GetMotherboardInfo();
             int memoryGB = hardware.GetTotalMemoryGB();

             // 输出到控制台
             Console.WriteLine($"CPU: {cpuInfo}");
             Console.WriteLine($"主板: {motherboardInfo}");
             Console.WriteLine($"内存: {memoryGB} GB");

             // 显示到界面
             lblCPU.Text = $"CPU: {cpuInfo}";
             lblMotherboard.Text = $"主板: {motherboardInfo}";
             lblMemory.Text = $"内存: {memoryGB} GB";
             */
            lblCPU.Text = $"CPU: {_hardware.GetCPUInfo()}";
            lblMotherboard.Text = $"主板: {_hardware.GetMotherboardInfo()}";
            lblMemory.Text = $"内存: {_hardware.GetTotalMemoryGB()} GB";

        }

        //显示推荐系统
        private void ShowRecommendedSystem()
        {
            try
            {
                //根据硬件信息获取推荐系统镜像路径
                _imagePath = _ruleEngine.GetImagePath(_hardware);
                lblRecommendSystem.Text = $"推荐系统: {Path.GetFileName(_imagePath)}";
                lblImagePath.Text = $"镜像路径：{_imagePath}";
            }
            catch (Exception ex)
            {
                lblRecommendSystem.Text = $"规则匹配失败：{ex.Message}";
                timerAutoDeploy.Stop();//停止自动部署
            }   
        }

        //定时器触发部署逻辑
        private void timerAutoDeploy_Tick(object sender, EventArgs e)   
        {
            StartDeployment();
            
        }

        //开始部署
        private async void StartDeployment()
        {
            try
            {
                timerAutoDeploy.Stop();//停止定时器

                /*
                  //检查镜像文件是否存在
                if (!File.Exists(_imagePath))
                {
                    MessageBox.Show($"镜像文件未找到：{_imagePath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;    
                }
                */

                //更新界面状态
                progressBar.Style = ProgressBarStyle.Marquee;
                lblStatus.Text = "正在部署系统...";

                //异步部署系统
                await Task.Run(() =>
                {
                    //调用部署管理器部署系统
                    _backupManager.DeploySystem(_imagePath);
                });

                //部署完成，重启计算机
                lblStatus.Text = "部署完成，计算机将在 10 秒后重启...";
                await Task.Delay(10000); //延时10秒
                RebootCompter();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"镜像文件未找到：{ex.FileName}");
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"权限不足，请以管理员身份运行！");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"未知错误：{ex.Message}");
            }

        }

        //重启计算机
        private void RebootCompter()
        {
            try 
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "shutdown",
                    Arguments = "/r /t 0",
                    CreateNoWindow = true,
                    UseShellExecute = false
                });
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"重启计算机失败：{ex.Message}");
            }
        }

        //取消按钮点击事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       

    }
}
