using Azure.AI.Vision.ImageAnalysis;
using System.Text;

namespace Travelnsight.Application.Helper;

public static class ImageAnalysisFormatter
{
    public static string FormatForLLM(ImageAnalysisResult analysis)
    {
        ArgumentNullException.ThrowIfNull(analysis);

        var sb = new StringBuilder();

        sb.AppendLine("Describe the image as if you are a human observer. Do not mention that this is an analysis or that you are responding to a user.");
        sb.AppendLine("Focus on what is visible, the scene, the people, objects, and context. You may also ask interesting questions about details in the scene.");
        sb.AppendLine();

        AppendCaption(sb, analysis.Caption?.Text);
        AppendTags(sb, analysis.Tags?.Values);
        AppendObjects(sb, analysis.Objects?.Values);
        AppendOcrText(sb, analysis.Read?.Blocks);

        return sb.ToString();
    }

    private static void AppendCaption(StringBuilder sb, string? caption)
    {
        if (!string.IsNullOrWhiteSpace(caption))
        {
            sb.AppendLine($"It looks like: {caption}.");
        }
    }

    private static void AppendTags(StringBuilder sb, IEnumerable<DetectedTag>? tags)
    {
        var tagList = tags?.Select(t => t.Name).ToList() ?? [];
        if (tagList.Any())
        {
            sb.AppendLine($"I also notice elements such as {string.Join(", ", tagList)}.");
        }
    }

    private static void AppendObjects(StringBuilder sb, IEnumerable<DetectedObject>? objects)
    {
        if (objects == null || !objects.Any()) return;

        sb.AppendLine("There are some distinct objects in the scene:");
        foreach (var obj in objects)
        {
            var objTags = obj.Tags.Select(t => t.Name);
            sb.AppendLine($"- One object appears with characteristics like: {string.Join(", ", objTags)}.");
        }
    }

    private static void AppendOcrText(StringBuilder sb, IEnumerable<DetectedTextBlock>? blocks)
    {
        var lines = blocks?.Select(b => string.Join(" ", b.Lines.Select(l => l.Text))).ToList() ?? [];
        if (!lines.Any()) return;

        sb.AppendLine("Additionally, I can read some text in the image:");
        foreach (var line in lines)
        {
            sb.AppendLine($"\"{line}\"");
        }
    }
}
