namespace Travelnsight.Application.Interfaces;

public interface IImageAnalysisUseCase
{
    Task<string> Analyze(byte[] image, CancellationToken cancellationToken);
}
