自动化系统部署工具项目文档
1. 项目概述
本工具用于在Windows PE环境中根据硬件信息自动选择并安装对应的操作系统镜像，支持自定义规则配置和日志记录。
________________________________________
2. 功能特性
•	硬件信息检测：自动获取CPU型号、主板信息、内存容量。
•	规则匹配：通过JSON配置文件动态匹配系统镜像。
•	自动化部署：启动后自动执行部署逻辑，无需用户交互。
•	日志记录：记录部署过程中的关键操作和错误信息。
•	自动重启：部署完成后自动重启计算机。
3. 项目结构
MyDeployTool/
├── SystemImages/                # 系统镜像目录
│   ├── Intel_Win11Pro_24H2.wim
│   ├── AMD_Win11Pro_24H2.wim
│   └── ...
├── rules.json                   # 规则配置文件
├── deploy.log                   # 日志文件
├── Hardware/
│   └── HardwareScanner.cs       # 硬件信息检测模块
├── Deployment/
│   └── BackupManager.cs         # 系统部署和备份模块
├── Rules/
│   └── RuleEngine.cs            # 规则解析引擎
└── MainForm.cs                  # 主界面逻辑
