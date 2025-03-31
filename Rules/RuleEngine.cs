using System;
using System.IO;
using Newtonsoft.Json;
using MyDeployTool.Hardware;

namespace MyDeployTool.Rules
{
    public class RuleEngine
    {
        private class RuleConfig
        {
            public Rule[] Rules { get; set; }
        }

        private class Rule
        {
            public string Condition { get; set; }
            public string ImagePath { get; set; }
        }

        private readonly Rule[] _rules;

        public RuleEngine()
        {
            // 动态获取当前程序运行目录
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string configPath = Path.Combine(currentDir, "rules.json");

            Console.WriteLine($"配置文件路径: {configPath}");

            if (!File.Exists(configPath))
                throw new FileNotFoundException($"规则配置文件未找到: {configPath}");

            string json = File.ReadAllText(configPath);
            var config = JsonConvert.DeserializeObject<RuleConfig>(json);
            _rules = config.Rules;
        }

        // 根据硬件信息获取镜像路径
        public string GetImagePath(HardwareScanner hardware)
        {
            string cpu = hardware.GetCPUInfo();
            int memoryGB = hardware.GetTotalMemoryGB();

            foreach (var rule in _rules)
            {
                bool isMatch = EvaluateCondition(rule.Condition, cpu, memoryGB);
                if (isMatch)
                {
                    return rule.ImagePath;
                }
            }

            throw new InvalidOperationException("没有匹配的规则！");
        }

        private bool EvaluateCondition(string condition, string cpu, int memoryGB)
        {
            // 替换变量为实际值
            string expr = condition
                .Replace("CPU", $"\"{cpu}\"")
                .Replace("MemoryGB", memoryGB.ToString());

            try
            {
                return new System.Data.DataTable().Compute(expr, null) as bool? ?? false;
            }
            catch
            {
                return false;
            }
        }
    }
}