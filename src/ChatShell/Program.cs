using System.Diagnostics;
// using System.Text;
using System.Text.Json;
using ChatShell.Core;
using ChatShell.Core.Models;

namespace ChatShell;

public static class Program {
	private static readonly CancellationTokenSource CancellationTokenSource = new();

	public static async Task Main(string[] args) {
		var fs = File.OpenRead(Path.Combine(Path.GetDirectoryName(Environment.ProcessPath.NotNull()).NotNull(), "AppConfigure.json"));
		var configure = (await JsonSerializer.DeserializeAsync<AppConfigure>(fs)).NotNull("Invalid AppConfigure.json");
		await fs.DisposeAsync();

		ProcessConfigure processConfigure;
		if (args.Length > 0) {
			processConfigure = configure.ProcessConfigures.FirstOrDefault(p => p.Name == args[0]) ??
							   throw new ArgumentOutOfRangeException($"No such a ProcessConfigure: {args[0]}");
		} else {
			processConfigure = configure.ProcessConfigures[0];
		}

		var ai = configure.AiConfigures.FirstOrDefault(a => a.Name == processConfigure.AiName) ??
				  throw new ArgumentOutOfRangeException($"No such a AiConfigure: {processConfigure.AiName}");
		ai.Initialize();

		var startInfo = processConfigure.ToStartInfo(args.Length > 1 ? string.Join(' ', args[1..]) : string.Empty);
		var process = Process.Start(startInfo);
		if (process == null) {
			throw new ApplicationException($"Cannot start process, command line: {startInfo.FileName} {startInfo.Arguments}");
		}
		
		// process.ErrorDataReceived += Process_OnErrorDataReceived;
		process.Exited += Process_OnExited;

		var cancellationToken = CancellationTokenSource.Token;
		Task.Run(() => RedirectTask(process.StandardOutput, Console.Out, cancellationToken), cancellationToken).Detach();
		Task.Run(() => RedirectTask(process.StandardError, Console.Error, cancellationToken), cancellationToken).Detach();

		try {
			// await ReadOutputAsync(process, cancellationToken);

			while (true) {
				var input = (await Console.In.ReadLineAsync(cancellationToken))?.Trim();
				if (string.IsNullOrEmpty(input)) {
					break;
				}

				await Console.Out.WriteLineAsync("AI is thinking...");
				var command = await ai.AskCommand(processConfigure, input, cancellationToken);
				await Console.Out.WriteLineAsync(command);
				await WriteWithColor("Execute command? [y(es) / e(xplain) / n(o)]: ", ConsoleColor.Green);

				input = (await Console.In.ReadLineAsync(cancellationToken))?.Trim().ToLower();
				switch (input) {
					case "":
					case "y": {
						await ExecuteAsync(process, command);
						break;
					}
					case "e": {
						await Console.Out.WriteLineAsync("AI is thinking...");
						var explain = await ai.ExplainCommand(processConfigure, command, input, cancellationToken);
						await Console.Out.WriteLineAsync(explain);
						await WriteWithColor("Execute command? [y(es) / n(o)]: ", ConsoleColor.Green);

						input = (await Console.In.ReadLineAsync(cancellationToken))?.Trim().ToLower();
						if (input is "" or "y") {
							await ExecuteAsync(process, command);
						} else {
							await process.StandardInput.WriteAsync(Environment.NewLine);
						}

						break;
					}
					default: {
						await process.StandardInput.WriteAsync(Environment.NewLine);
						break;
					}
				}
			}
		} catch (OperationCanceledException) {
			// Ignore
		}
	}

	// private static void Process_OnErrorDataReceived(object sender, DataReceivedEventArgs e) {
	// 	Console.Error.WriteLine(e.Data);
	// }

	private static void Process_OnExited(object? sender, EventArgs e) {
		CancellationTokenSource.Cancel();
		Console.Out.WriteLine($"\nProcess exited, code: {((Process)sender!).ExitCode}");
	}

	private static async Task WriteWithColor(string value, ConsoleColor color) {
		var originalColor = Console.ForegroundColor;
		Console.ForegroundColor = color;
		await Console.Out.WriteAsync(value);
		Console.ForegroundColor = originalColor;
	}

	private static async Task ExecuteAsync(Process process, string command) {
		foreach (var commandLine in command.Split('\n')) {
			await process.StandardInput.WriteLineAsync(commandLine);
			// await ReadOutputAsync(process, cancellationToken);
		}
	}

	private static async Task RedirectTask(StreamReader reader, TextWriter writer, CancellationToken cancellationToken) {
		var buffer = new char[1];
		while (!cancellationToken.IsCancellationRequested) {
			await reader.ReadAsync(buffer, cancellationToken);
			await writer.WriteAsync(buffer, cancellationToken);
		}
	}

	// private static async Task ReadOutputAsync(Process process, CancellationToken cancellationToken) {
	// 	while (true) {
	// 		var line = await ReadLineOrEndNoBlock(process.StandardOutput);
	// 		if (string.IsNullOrEmpty(line)) {  // 如果程序没有输出，那就输出ChatShell> 
	// 			await Console.Out.WriteAsync($"{nameof(ChatShell)}> ");
	// 			break;
	// 		}
	// 		
	// 		if (line[^1] != '\n') {  // 如果程序有输出，那么就在前面加(ChatShell)
	// 			await Console.Out.WriteAsync($"({nameof(ChatShell)}) {line}");
	// 			break;
	// 		}
	// 
	// 		await Console.Out.WriteAsync(line);
	// 	}
	// }

	// private static async Task<string?> ReadLineOrEndNoBlock(StreamReader reader) {
	// 	if (reader.Peek() == -1) {
	// 		return null;
	// 	}
	// 
	// 	var sb = new StringBuilder();
	// 	var buffer = new char[1];
	// 	while (reader.Peek() != -1) {
	// 		await reader.ReadAsync(buffer, 0, 1);
	// 		var c = buffer[0];
	// 		switch (c) {
	// 			case '\n': {
	// 				return sb.Append('\n').ToString();
	// 			}
	// 			case '\r': {  // Mac: \r  UNIX: \n
	// 				var next = reader.Peek();
	// 				if (next == '\n') {
	// 					await reader.ReadAsync(buffer, 0, 1);
	// 				}
	// 				return sb.Append('\n').ToString();
	// 			}
	// 			default: {
	// 				sb.Append(c);
	// 				break;
	// 			}
	// 		}
	// 	}
	// 
	// 	return sb.ToString();
	// }
}