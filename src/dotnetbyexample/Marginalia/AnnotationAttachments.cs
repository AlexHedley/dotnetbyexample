namespace dotnetbyexample.Marginalia;

/// <summary>
/// Loads text annotations for an example page from a file in the example
/// directory.
/// </summary>
public static class AnnotationAttachments
{
    private const string AnnotationFileName = "Annotations.txt";

    public static IEnumerable<(string Text, string Note, string CssClass, string? Context)> GetAnnotations(string exampleDirectory)
    {
        var annotationFile = Path.Combine(exampleDirectory, AnnotationFileName);
        if (!File.Exists(annotationFile))
            return [];

        return File.ReadLines(annotationFile)
            .Select(ParseLine)
            .Where(parsed => parsed.HasValue)
            .Select(parsed => parsed!.Value);
    }

    private static (string Text, string Note, string CssClass, string? Context)? ParseLine(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            return null;

        var trimmed = line.Trim();
        if (trimmed.StartsWith("#", StringComparison.Ordinal))
            return null;

        var parts = trimmed.Split('|', StringSplitOptions.TrimEntries);
        if (parts.Length is not (4 or 5))
            return null;

        var offset = parts.Length == 5 ? 1 : 0;
        var context = parts.Length == 5 ? EmptyAsNull(parts[0]) : null;
        var text = parts[offset];
        var note = parts[offset + 1];
        var directionClass = parts[offset + 2];
        var colorClass = parts[offset + 3];

        if (string.IsNullOrWhiteSpace(text) ||
            string.IsNullOrWhiteSpace(note) ||
            string.IsNullOrWhiteSpace(directionClass) ||
            string.IsNullOrWhiteSpace(colorClass))
        {
            return null;
        }

        return (
            text,
            note,
            $"ann {directionClass} {colorClass}",
            context
        );
    }

    private static string? EmptyAsNull(string value) =>
        string.IsNullOrWhiteSpace(value) ? null : value;
}
