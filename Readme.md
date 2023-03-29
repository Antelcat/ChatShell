# ✨ ChatShell
<div align="center">
    <p>ChatShell is a cross-platform command-line tool. Based on ChatGPT, it can generate corresponding commands by directly inputting the desired function.</p>
    <p>Supporting cmd, PowerShell, bash, conda, and more, ChatShell is ready to use out of the box without the need for runtime installation.</p>
</div>

<div align="center">
    <img src="https://github.com/DearVa/ChatShell/blob/master/img/Demo.gif?raw=true" width="800" alt="Demo"/>
</div>

<div align="center">
    <img src="https://github.com/DearVa/ChatShell/blob/master/img/MultipleShell.gif?raw=true" width="800" alt="MultipleShell"/>
</div>

<div align="center">
    <a href="https://github.com/DearVa/ChatShell/blob/master/Readme_zh_CN.md"><strong>🇨🇳中文版</strong></a>
</div>

# ♻️ Installation & Usage
ChatShell has been released as a single executable file, which you can download for your current operating system [here](https://github.com/DearVa/ChatShell/releases).

After downloading the file, you need a configuration file in order to run the program. The configuration file is named AppConfigure.json and should be placed in the same folder as the executable file. Below is a template configuration file for Windows.

This configuration file includes calls to both cmd and PowerShell shells, and the `ApiKey` value can be obtained from the [OpenAI website](https://platform.openai.com/account/api-keys).

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
                "ApiKey": "YOUR_API_KEY"
            },
            "ModelId": "gpt-3.5-turbo",
            "MaxTokens": 1024,
            "Language": "English"
        }
    ]
}
```

After downloading and placing the configuration file and executable in the same folder, execute `ChatShell cmd` in the command line. You will see the output of the cmd shell, but by doing so ChatShell has taken control of the input. You can now directly input a command description and ChatShell will automatically generate the corresponding command for you.

```cmd
Microsoft Windows [Version 10.0.22621.1413]
(c) Microsoft Corporation. All rights reserved.

G:\Source\C#\ChatShell\src>Convert MultipleShell.mp4 to MultipleShell.gif using ffmpeg while preserving the original resolution and using 30 frames per second
AI is thinking...
ffmpeg -i MultipleShell.mp4 -vf fps=30,scale=iw:-1 MultipleShell.gif
Execute command? [y(es) / e(xplain) / n(o)]:
```

If you are sure the command is correct, you can simply enter `y` to run it. If you have any questions, you can enter `e` to have ChatShell explain the command for you.

```cmd
Microsoft Windows [Version 10.0.22621.1413]
(c) Microsoft Corporation. All rights reserved.

G:\Source\C#\ChatShell\src>Convert MultipleShell.mp4 to MultipleShell.gif using ffmpeg while preserving the original resolution and using 30 frames per second
AI is thinking...
ffmpeg -i MultipleShell.mp4 -vf fps=30,scale=iw:-1 MultipleShell.gif
Execute command? [y(es) / e(xplain) / n(o)]: e
AI is thinking...
This command uses the FFmpeg tool to convert the video file "MultipleShell.mp4" into an animated GIF file. The "-i" parameter specifies the input file, and the "-vf" parameter specifies the operations to be performed during the video conversion process. Two filters are used here: "fps=30" adjusts the video frame rate to 30 frames per second, and "scale=iw:-1" outputs the video in its original aspect ratio. Finally, "MultipleShell.gif" specifies the output GIF file name.

This command is used to convert a video file into a more convenient and shareable animated GIF file, suitable for use on web pages, social media platforms, and other media. By adjusting the frame rate and scaling ratio, you can further optimize the size and clarity of the GIF file.
Execute command? [y(es) / n(o)]:
```
