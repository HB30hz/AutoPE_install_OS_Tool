using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDeployTool.Deployment
{
    //部署管理器
    public class BackupManager
    {
        public void DeploySystem(string imagePath)
        {
            // 记录开始时间
            File.AppendAllText("deploy.log", $"[{DateTime.Now}] 开始部署系统...\n");

            // 执行部署命令
            ExecuteCommand("diskpart.exe", $"/Apply-Image /ImageFile:{imagePath} /Index:1 /ApplyDir:D:\\");

            // 记录完成时间
            File.AppendAllText("deploy.log", $"[{DateTime.Now}] 部署完成！\n");
            
            // 示例：使用 DISM 应用镜像（需根据实际分区调整路径）
            string dismArgs = $"/Apply-Image /ImageFile:{imagePath} /Index:1 /ApplyDir:D:\\";
            ExecuteCommand("dism.exe", dismArgs);

            // 修复引导
            ExecuteCommand("bcdboot.exe", "D:\\Windows /s S:");
        }

        private void ExecuteCommand(string command, string args)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = args,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            // 记录日志
            File.AppendAllText("deploy.log", $"[{DateTime.Now}] 执行命令: {command} {args}\n输出:\n{output}\n");
        }
    }
}
