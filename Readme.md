# ChatShell
ChatShell is a cross-platform, command-line tool based on ChatGPT, supporting multiple shells and customizable configurations.

<div align="center">
    <img src="https://github.com/DearVa/ChatShell/blob/master/img/MultipleShell.gif" width="800" alt="MultipleShell"/>
</div>

# Installation
ChatShell被发布成了单个可执行文件，你可以在 [这里](https://github.com/DearVa/ChatShell/releases) 下载到对应你当前电脑操作系统的版本。

下载完成后，你需要一个配置文件才能够运行程序。配置文件命名为`AppConfigure.json`，将它和可执行文件放在同一个文件夹，下面是一个配置文件的模板。

其中的ApiKey可以从 [OpenAI官网](https://platform.openai.com/account/api-keys) 获取，其中的Organization不是必要的，具体可参考官网指南。

```json
{
    "ProcessConfigures": [
        {
            "Name": "cmd",
            "CommandLine": "cmd",
            "Description": "cmd",
            "OperationSystem": "Windows 11",
            "AiName": "gpt3.5"
        },
        {
            "Name": "ps",
            "CommandLine": "powershell",
            "Description": "powershell",
            "OperationSystem": "Windows 11",
            "AiName": "gpt3.5"
        }
    ],
    "AiConfigures": [
        {
            "Type": "ChatGptAi",
            "Name": "gpt3.5",
            "Options": {
                "ApiKey": "YOUR_API_KEY",
                "Organization": "YOUR_ORIGANIZATION"
            },
            "ModelId": "gpt-3.5-turbo",
            "MaxTokens": 1024,
            "Language": "Chinese"
        }
    ]
}
```