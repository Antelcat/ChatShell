using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace ChatShell.Core.Models;

[Serializable]
public partial class ProcessConfigure {
	public required string Name { get; init; } 

	public required string CommandLine { get; init; } 

	public required string Description { get; init; }

	public required string OperationSystem { get; init; }

	public required string AiName { get; init; }

	public string? WorkingDirectory { get; init; } 

	public ProcessStartInfo ToStartInfo(string arguments) {
		var commands = SplitBySpace().Split(CommandLine);
		var argumentsBuilder = new StringBuilder();
		foreach (var command in commands.Skip(1)) {
			argumentsBuilder.Append(command).Append(' ');
		}

		return new ProcessStartInfo {
			FileName = commands[0],
			Arguments = argumentsBuilder.Append(arguments).ToString(),
			WorkingDirectory = WorkingDirectory,
			CreateNoWindow = true,
			UseShellExecute = false,
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			RedirectStandardInput = true,
		};
	}

	[GeneratedRegex(@"[ ](?=(?:[^""]*""[^""]*"")*[^""]*$)")]
	private static partial Regex SplitBySpace();
}