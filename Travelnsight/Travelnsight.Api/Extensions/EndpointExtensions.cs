using Travelnsight.Api.Handler;

namespace Travelnsight.Api.Extensions;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapVision(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/Vision")
                           .WithOpenApi();

        group.MapPost("/analyze", VisionAnalyzeHandler.ExecuteAsync)
             .Produces(StatusCodes.Status401Unauthorized)
             .Produces(StatusCodes.Status400BadRequest)
             .Produces(StatusCodes.Status200OK);

        return builder;
    }
}
