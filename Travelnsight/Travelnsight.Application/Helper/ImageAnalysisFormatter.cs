using Azure.AI.Vision.ImageAnalysis;
using System.Text;

namespace Travelnsight.Application.Helper;

public static class ImageAnalysisFormatter
{
    public static string FormatForLLM(ImageAnalysisResult analysis)
    {
        ArgumentNullException.ThrowIfNull(analysis);

        var tags = FormatAnalysisSection(analysis.Tags?.Values, t => t.Name, "None detected");
        var objects = FormatAnalysisSection(analysis.Objects?.Values, o => string.Join(", ", o.Tags.Select(t => t.Name)), "None detected");
        var ocrText = FormatAnalysisSection(analysis.Read?.Blocks, b => string.Join(" ", b.Lines.Select(l => l.Text)), "None detected");

        return new StringBuilder()
            .AppendLine()
            .AppendLine("Detected Elements:")
            .AppendLine($"- Tags: {tags}")
            .AppendLine($"- Objects: {objects}")
            .AppendLine()
            .AppendLine("Recognized Text (OCR):")
            .AppendLine(ocrText)
            .ToString();
    }

    private static string FormatAnalysisSection<T>(IEnumerable<T>? items, Func<T, string> selector, string defaultValue)
    {
        return items?.Any() == true
            ? string.Join(", ", items.Select(selector))
            : defaultValue;
    }
}
