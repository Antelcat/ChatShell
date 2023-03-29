using System.Text.Json.Serialization;
using ChatShell.Core.Interfaces;

namespace ChatShell.Core.Models;

[Serializable]
public class AppConfigure {
	public required List<ProcessConfigure> ProcessConfigures { get; init; }

	public required List<IAi> AiConfigures { get; init; }

	[JsonConstructor]
	public AppConfigure() { }
}