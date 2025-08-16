using Azure;
using Azure.AI.Inference;
using Microsoft.Extensions.Options;
using Travelnsight.Application.Interfaces;
using Travelnsight.Infraestructure.Configuration;

namespace Travelnsight.Infraestructure.Services;

public class AIInferenceService(IOptionsMonitor<AzureAIInferenceOptions> options) : IAIInferenceService
{
    private const string GPT_4O = "gpt-4o";

    public async Task<string> AskToGpt4o(string prompt, CancellationToken cancellationToken)
    {
        var client = new ChatCompletionsClient(
            new Uri(options.CurrentValue.Endpoint),
            new AzureKeyCredential(options.CurrentValue.Key),
            new AzureAIInferenceClientOptions());

        var request = new ChatCompletionsOptions
        {
            Messages = { new ChatRequestUserMessage(prompt) },
            Model = GPT_4O,
        };

        var response = await client.CompleteAsync(request, cancellationToken);

        return response.Value.Content;
    }
}
