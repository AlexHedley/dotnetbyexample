// Figure attachments for .NET by Example.
//
// Each example directory may contain a `Figures.txt` file alongside its
// source files. Adding a figure to an example needs no code change.
//
// Format: one attachment per line, pipe-delimited:
//
//     anchor|figure-name|caption
//
// Blank lines and lines starting with '#' are ignored. The anchor
// "after-last" renders a banner below the final code section.

namespace dotnetbyexample.Marginalia;

/// <summary>
/// Describes a figure attachment: the position, the figure name, and the
/// caption that accompanies it.
/// </summary>
public sealed record FigureAttachment(string Anchor, string FigureName, string Caption);

/// <summary>
/// Reads figure attachments from <c>Figures.txt</c> in an example directory.
/// </summary>
public static class FigureAttachments
{
    public const string FileName = "Figures.txt";

    /// <summary>
    /// Parses the attachments for an example directory. Returns an empty list
    /// if the directory has no <c>Figures.txt</c>.
    /// </summary>
    public static IReadOnlyList<FigureAttachment> Load(string exampleDirectory)
    {
        var path = Path.Combine(exampleDirectory, FileName);
        if (!File.Exists(path))
            return Array.Empty<FigureAttachment>();

        var result = new List<FigureAttachment>();
        foreach (var raw in File.ReadAllLines(path))
        {
            var line = raw.Trim();
            if (line.Length == 0 || line.StartsWith('#'))
                continue;

            var parts = line.Split('|', 3);
            if (parts.Length != 3)
            {
                Console.WriteLine($"Skipping malformed line in {path}: {line}");
                continue;
            }

            result.Add(new FigureAttachment(parts[0].Trim(), parts[1].Trim(), parts[2].Trim()));
        }

        return result;
    }

    /// <summary>
    /// Returns all (svg, caption) pairs for the given example directory.
    /// Returns an empty sequence if no figures are attached.
    /// </summary>
    public static IEnumerable<(string Svg, string Caption)> GetFigures(string exampleDirectory)
    {
        foreach (var att in Load(exampleDirectory))
        {
            var svg = Figures.RenderSvg(att.FigureName);
            if (!string.IsNullOrEmpty(svg))
                yield return (svg, att.Caption);
            else
                Console.WriteLine($"Unknown figure '{att.FigureName}' in {Path.Combine(exampleDirectory, FileName)}");
        }
    }
}
