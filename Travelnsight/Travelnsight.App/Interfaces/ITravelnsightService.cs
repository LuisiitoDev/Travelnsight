using Refit;
using Travelnsight.App.Dto;

namespace Travelnsight.App.Interfaces;

public interface ITravelnsightService
{
    [Post("/api/Vision/analyze")]
    Task<VisionResponseDto> Analyze(VisionRequestDto request);
}
