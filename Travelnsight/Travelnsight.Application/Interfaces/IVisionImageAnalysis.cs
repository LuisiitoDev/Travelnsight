using Azure.AI.Vision.ImageAnalysis;

namespace Travelnsight.Application.Interfaces;

public interface IVisionImageAnalysis
{
    Task<ImageAnalysisResult> Analyze(byte[] image, CancellationToken cancellationToken);
}
