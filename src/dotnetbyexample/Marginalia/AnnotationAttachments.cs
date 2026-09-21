using System.Collections.Generic;

namespace dotnetbyexample.Marginalia;

/// <summary>
/// Describes a text annotation attached to an example page.
/// </summary>
public readonly record struct AnnotationAttachment(
    string Text,
    string Note,
    string DirectionClass,
    string ColorClass,
    string? Context = null);

/// <summary>
/// Maps example directory slugs to optional annotation callouts.
/// </summary>
public static class AnnotationAttachments
{
    private static readonly Dictionary<string, IReadOnlyList<AnnotationAttachment>> Registry =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["hello-world"] = new[]
            {
                new AnnotationAttachment(
                    "Console.WriteLine",
                    "writes to standard output",
                    "ann-s",
                    "ann-amber",
                    "Core API")
            },
            ["maps"] = new[]
            {
                new AnnotationAttachment(
                    "TryGetValue",
                    "read safely without exceptions",
                    "ann-s",
                    "ann-blue",
                    "Lookup"),
                new AnnotationAttachment(
                    "delete",
                    "remove a key/value entry",
                    "ann-se",
                    "ann-green",
                    "Mutation")
            },
            ["goroutines"] = new[]
            {
                new AnnotationAttachment(
                    "go",
                    "starts concurrent work",
                    "ann-ne",
                    "ann-purple",
                    "Concurrency")
            }
        };

    public static IEnumerable<(string Text, string Note, string CssClass, string? Context)> GetAnnotations(string slug)
    {
        if (!Registry.TryGetValue(slug, out var annotations))
            return [];

        return annotations.Select(annotation => (
            annotation.Text,
            annotation.Note,
            $"ann {annotation.DirectionClass} {annotation.ColorClass}",
            annotation.Context));
    }
}
