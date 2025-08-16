using Microsoft.AspNetCore.Mvc;
using Travelnsight.Api.Dtos;
using Travelnsight.Application.Interfaces;

namespace Travelnsight.Api.Handler;

public static class VisionAnalyzeHandler
{
    public static async Task<IResult> ExecuteAsync(VisionRequestDto request, IImageAnalysisUseCase imageAnalysis, CancellationToken cancellationToken)
    {
        var response = await imageAnalysis.Analyze(request.Image, cancellationToken);
        return Results.Ok(new VisionResponseDto { Response = response });
    }
}
