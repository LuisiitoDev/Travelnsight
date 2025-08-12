namespace Travelnsight.Application.Interfaces;

public interface IImageContentModerator
{
    Task<bool> IsImageSafe(byte[] image, CancellationToken cancellationToken);
}
