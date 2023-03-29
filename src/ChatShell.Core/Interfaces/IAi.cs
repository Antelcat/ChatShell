using System.Text.Json.Serialization;
using ChatShell.Core.Implements;
using ChatShell.Core.Models;

namespace ChatShell.Core.Interfaces;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "Type")]
[JsonDerivedType(typeof(ChatGptAi), nameof(ChatGptAi))]
public interface IAi {
	string Name { get; init; }

	void Initialize();

	/// <summary>
	/// 询问一个Command
	/// </summary>
	/// <param name="processConfigure"></param>
	/// <param name="commandInstruction"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<string> AskCommand(ProcessConfigure processConfigure, string commandInstruction, CancellationToken cancellationToken);

	/// <summary>
	/// 让AI解释Command
	/// </summary>
	/// <param name="processConfigure"></param>
	/// <param name="command"></param>
	/// <param name="commandInstruction"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<string> ExplainCommand(ProcessConfigure processConfigure, string command, string commandInstruction, CancellationToken cancellationToken);
}