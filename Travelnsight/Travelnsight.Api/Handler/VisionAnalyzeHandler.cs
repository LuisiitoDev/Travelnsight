using Microsoft.AspNetCore.Mvc;
using Travelnsight.Api.Dtos;
using Travelnsight.Application.Interfaces;

namespace Travelnsight.Api.Handler;

public static class VisionAnalyzeHandler
{
    public static async Task<IResult> ExecuteAsync([FromBody] byte[] image, [FromServices] IImageAnalysisUseCase imageAnalysis, CancellationToken cancellationToken)
    {
        var response = await imageAnalysis.Analyze(image, cancellationToken);
        return Results.Ok(new VisionResponseDto { Response = response });
    }
}
