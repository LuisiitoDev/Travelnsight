using Travelnsight.Application.Helper;
using Travelnsight.Application.Interfaces;

namespace Travelnsight.Application.UsesCases;

public class ImageAnalysisUseCase(IImageContentModerator moderator, IVisionImageAnalysis vision, IAIInferenceService inference) : IImageAnalysisUseCase
{
    public async Task<string> Analyze(byte[] image, CancellationToken cancellationToken)
    {
        if (!await moderator.IsImageSafe(image, cancellationToken)) return string.Empty;

        var analysisResult = await vision.Analyze(image, cancellationToken);
        var prompt = ImageAnalysisFormatter.FormatForLLM(analysisResult);

        return await inference.AskToGpt4o(prompt, cancellationToken);
    }
}
