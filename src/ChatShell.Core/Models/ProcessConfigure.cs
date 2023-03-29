using System.Diagnostics;

namespace ChatShell.Core.Models;

[Serializable]
public class ProcessConfigure {
	public required string Name { get; init; } 

	public required string CommandLine { get; init; } 

	public required string Description { get; init; }

	public required string OperationSystem { get; init; }

	public required string AiName { get; init; }

	public string? WorkingDirectory { get; init; } 

	public ProcessStartInfo ToStartInfo(string arguments) {
		return new ProcessStartInfo {
			FileName = CommandLine,
			Arguments = arguments,
			WorkingDirectory = WorkingDirectory,
			CreateNoWindow = true,
			UseShellExecute = false,
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			RedirectStandardInput = true,
		};
	}
}