# ✨ ChatShell
<div align="center">
    <p>ChatShell是一个跨平台的命令行工具。基于ChatGPT，直接输入要实现的功能，即可生成对应的命令。</p>
    <p>支持cmd、PowerShell、bash、conda等等，无需运行时，开箱即用。</p>
</div>

<div align="center">
    <img src="https://github.com/DearVa/ChatShell/blob/master/img/Demo.gif?raw=true" width="800" alt="Demo"/>
</div>

<div align="center">
    <img src="https://github.com/DearVa/ChatShell/blob/master/img/MultipleShell.gif?raw=true" width="800" alt="MultipleShell"/>
</div>

# ♻️ Installation & Usage
ChatShell被发布成了单个可执行文件，你可以在 [这里](https://github.com/DearVa/ChatShell/releases) 下载到对应你当前电脑操作系统的版本。

下载完成后，你需要一个配置文件才能够运行程序。配置文件命名为`AppConfigure.json`，将它和可执行文件放在同一个文件夹，下面是一个适用于Windows的配置文件模板。

这个配置文件包含了对cmd和PowerShell两种shell的调用，其中的ApiKey可以从 [OpenAI官网](https://platform.openai.com/account/api-keys) 获取。

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
                "ApiKey": "输入你的API KEY"
            },
            "ModelId": "gpt-3.5-turbo",
            "MaxTokens": 1024,
            "Language": "Chinese"
        }
    ]
}
```

之后在命令行中执行`ChatShell cmd`，你会看到cmd的输出，但此时ChatShell已经接管了输入。你可以直接输入对命令的描述，ChatShell会自动生成对应的命令。

```cmd
Microsoft Windows [版本 10.0.22621.1413]
(c) Microsoft Corporation。保留所有权利。

G:\Source\C#\ChatShell\src>将MultipleShell.mp4用ffmpeg转换成MultipleShell.gif，保留原始的分辨率，30帧每秒
AI is thinking...
ffmpeg -i MultipleShell.mp4 -vf fps=30,scale=1920:-1 MultipleShell.gif
Execute command? [y(es) / e(xplain) / n(o)]:
```

如果确认命令无误，输入 `y` 即可直接运行。如果有任何问题，可以输入 `e` 让ChatShell为你解释命令。

```cmd
G:\Source\C#\ChatShell\src>将MultipleShell.mp4用ffmpeg转换成MultipleShell.gif，保留原始的分辨率，30帧每秒
AI is thinking...
ffmpeg -i MultipleShell.mp4 -vf fps=30,scale=iw:-1 MultipleShell.gif
Execute command? [y(es) / e(xplain) / n(o)]: e
AI is thinking...
此命令是使用 FFmpeg 工具将视频文件 MultipleShell.mp4 转换为动画 GIF 文件的命令。-i 参数指定输入文件，-vf 参数指定在视频
转换过程中需要进行的操作。这里使用了两个过滤器。 fps=30 表示将视频的帧率调整为 30 帧每秒，scale=iw:-1 表示将视频按照原始比例输出。最后的 MultipleShell.gif 表示输出的 GIF 文件名。

使用此命令的原因是可以将视频文件转换为更为方便使用的动画 GIF 文件，便于在网页、社交媒体等平台进行分享和传播。同时，通过
调整帧率和缩放比例，还可以进一步优化 GIF 文件的大小和清晰度。
Execute command? [y(es) / n(o)]:
```
