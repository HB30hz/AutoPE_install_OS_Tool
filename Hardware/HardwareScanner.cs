using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace MyDeployTool.Hardware
{
    //硬件信息扫描器
    public class HardwareScanner : IDisposable
    {
        private ManagementObjectSearcher _cpuSearcher;
        private ManagementObjectSearcher _motherboardSearcher;
        private ManagementObjectSearcher _memorySearcher;

        //获取CPU信息
        public string GetCPUInfo()
        {
            try
            {
               _cpuSearcher = new ManagementObjectSearcher("SELECT Name FROM Win32_Processor");
                foreach (ManagementObject obj in _cpuSearcher.Get())
                {
                    return obj["Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取 CPU 信息失败: " + ex.Message);
            }
            return "Unknown";
        }


        //获取主板信息
        public string GetMotherboardInfo()
        {
            try
            {
                _motherboardSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
                foreach (ManagementObject obj in _motherboardSearcher.Get())
                {
                    return $"{obj["Manufacturer"]} {obj["Product"]}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取主板信息失败: " + ex.Message);
            }
            return "Unknown";
        }

        //获取总内存大小（单位：GB）
        public int GetTotalMemoryGB()
        {
            try
            {
                _memorySearcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
                ulong total = 0;
                foreach (ManagementObject obj in _memorySearcher.Get())
                {
                    total += Convert.ToUInt64(obj["Capacity"]);
                }
                return (int)(total / 1024 / 1024 / 1024);//转换为GB
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取内存信息失败: " + ex.Message);
            }
            return 0;
        }

        public void Dispose()
        {
            _cpuSearcher?.Dispose();
            _motherboardSearcher?.Dispose();
            _memorySearcher?.Dispose();
        }
    }
}
