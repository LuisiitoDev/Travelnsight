using Azure;
using Azure.AI.ContentSafety;
using Microsoft.Extensions.Options;
using Travelnsight.Application.Interfaces;
using Travelnsight.Infraestructure.Configuration;

namespace Travelnsight.Infraestructure.Services;

public class ImageContentModerator(IOptionsMonitor<AzureImageModeratorOptions> options) : IImageContentModerator
{
    public async Task<bool> IsImageSafe(byte[] image, CancellationToken cancellationToken)
    {
        var client = new ContentSafetyClient(new Uri(options.CurrentValue.Endpoint), new AzureKeyCredential(options.CurrentValue.Key));
        var contentSafe = new ContentSafetyImageData(BinaryData.FromBytes(image));
        var request = new AnalyzeImageOptions(contentSafe);
        var response = await client.AnalyzeImageAsync(request, cancellationToken);

        return response.Value.CategoriesAnalysis.Any(c => c.Severity >= 1);
    }
}
