namespace Travelnsight.Application.Interfaces;

public interface IAIInferenceService
{
    Task<string> AskToGpt4o(string prompt, CancellationToken cancellationToken);
}
