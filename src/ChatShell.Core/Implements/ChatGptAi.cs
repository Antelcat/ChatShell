using ChatShell.Core.Interfaces;
using ChatShell.Core.Models;
using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;

namespace ChatShell.Core.Implements; 

internal class ChatGptAi : IAi {
	private const string AskCommandPrompt = """
		Provide only {0} commands for {1} without any description.
		If there is a lack of details, provide most logical solution.
		Ensure the output is a valid shell command.
		If multiple steps required try to combine them together.
		Instruction: {2}
		""";

	private const string ExplainCommandPrompt = """
		Given a {0} command and it's instruction for {1}.
		Explain how the command works and why use this command.
		Please use {2} to reply. 
		Command: {3}
		Instruction: {4}
		""";

	public required string Name { get; init; }

	public required OpenAiOptions Options { get; init; }

	public required string ModelId { get; init; }

	public string Language { get; init; } = "English";

	public float? Temperature { get; set; }

	public int? MaxTokens { get; set; }


	private IOpenAIService? openAIService;

	public void Initialize() {
		openAIService = new OpenAIService(Options);
	}

	public Task<string> AskCommand(ProcessConfigure processConfigure, string commandInstruction, CancellationToken cancellationToken) {
		var prompt = string.Format(AskCommandPrompt, processConfigure.Description, processConfigure.OperationSystem, commandInstruction);
		return CreateChatCompletion(prompt, cancellationToken);
	}

	public Task<string> ExplainCommand(ProcessConfigure processConfigure, string command, string commandInstruction, CancellationToken cancellationToken) {
		var prompt = string.Format(ExplainCommandPrompt, processConfigure.Description, processConfigure.OperationSystem, Language, command, commandInstruction);
		return CreateChatCompletion(prompt, cancellationToken);
	}

	private async Task<string> CreateChatCompletion(string prompt, CancellationToken cancellationToken) {
		if (openAIService == null) {
			throw new ArgumentNullException(nameof(openAIService), "Not initialized");
		}

		var response = await openAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest {
			Messages = new ChatMessage[] {
				new("system", prompt)
			}
		}, ModelId, cancellationToken);

		if (response.Error != null) {
			throw new Exception(response.Error.Message);
		}

		return response.Choices[0].Message.Content;
	}
}