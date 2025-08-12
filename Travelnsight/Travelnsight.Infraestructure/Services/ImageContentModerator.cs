using Azure.AI.ContentSafety;
using Travelnsight.Application.Interfaces;

namespace Travelnsight.Infraestructure.Services;

public class ImageContentModerator(ContentSafetyClient client) : IImageContentModerator
{
    public async Task<bool> IsImageSafe(byte[] image, CancellationToken cancellationToken)
    {
        var contentSafe = new ContentSafetyImageData(BinaryData.FromBytes(image));
        var request = new AnalyzeImageOptions(contentSafe);
        var response = await client.AnalyzeImageAsync(request, cancellationToken);

        return response.Value.CategoriesAnalysis.Any(c => c.Severity >= 1);
    }
}
