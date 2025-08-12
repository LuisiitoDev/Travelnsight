using Azure;
using Azure.AI.Vision.ImageAnalysis;
using Microsoft.Extensions.Options;
using Travelnsight.Application.Interfaces;
using Travelnsight.Infraestructure.Configuration;

namespace Travelnsight.Infraestructure.Services;

public class VisionImageAnalysis(IOptionsMonitor<AzureCustomVisionOptions> options) : IVisionImageAnalysis
{
    public async Task<ImageAnalysisResult> Analyze(byte[] image, CancellationToken cancellationToken)
    {
        var client = new ImageAnalysisClient(
            new Uri(options.CurrentValue.Endpoint),
            new AzureKeyCredential(options.CurrentValue.Key));

        return await client.AnalyzeAsync(BinaryData.FromBytes(image),
            visualFeatures: VisualFeatures.Tags |
                            VisualFeatures.Objects |
                            VisualFeatures.Read |
                            VisualFeatures.People |
                            VisualFeatures.DenseCaptions,
            new ImageAnalysisOptions() { GenderNeutralCaption = true },
            cancellationToken);
    }
}
