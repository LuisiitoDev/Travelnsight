using Azure.AI.Vision.ImageAnalysis;
using System.Text;
using Travelnsight.Application.Interfaces;

namespace Travelnsight.Infraestructure.Services;

public class VisionImageAnalysis(ImageAnalysisClient client) : IVisionImageAnalysis
{
    public async Task<ImageAnalysisResult> Analyze(byte[] image, CancellationToken cancellationToken)
    {
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
