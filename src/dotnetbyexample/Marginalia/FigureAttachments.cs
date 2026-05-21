// Figure attachment map for .NET by Example.
//
// This file records which figures appear on which example pages.
// Example markdown and source files are never modified; figures are
// curated here by the project owner.
//
// The anchor "after-last" renders a banner below the final code section.
// Future anchors like "after-cell-0" can be wired up as the template evolves.

namespace dotnetbyexample.Marginalia;

/// <summary>
/// Describes a figure attachment: the position, the figure name, and the
/// caption that accompanies it.
/// </summary>
public sealed record FigureAttachment(string Anchor, string FigureName, string Caption);

/// <summary>
/// Maps example directory slugs to their figure attachments.
///
/// Slugs match the lowercase directory name under <c>examples/</c>.
/// The convention matches the Python By Example attachment model:
/// one or more figures per example page, each with an anchor and caption.
/// </summary>
public static class FigureAttachments
{
    /// <summary>
    /// slug → ordered list of attachments (anchor, figure name, caption).
    /// </summary>
    public static readonly IReadOnlyDictionary<string, IReadOnlyList<FigureAttachment>> Attachments =
        new Dictionary<string, IReadOnlyList<FigureAttachment>>(StringComparer.OrdinalIgnoreCase)
        {
            ["values"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "number-lines",
                    "C# int is a 32-bit signed integer with a fixed range; double is an IEEE 64-bit value whose representable precision thins out near the extremes."),
            },
            ["variables"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "variables-bind",
                    "A variable is a named slot that refers to an object. Assignment binds the slot; the object lives independently."),
            },
            ["recursion"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "call-stack",
                    "Each recursive call pushes a new frame with the same method name and a smaller argument; the base case unwinds back up the stack."),
            },
            ["closures"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "closure-cell",
                    "The inner function keeps a reference to the captured variable from the outer scope, so the captured value survives the outer call returning."),
            },
            ["for"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "for-loop",
                    "A for loop advances a counter through each value in the range one step at a time; the loop stops automatically when the range is exhausted."),
            },
            ["structs"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "struct-fields",
                    "A struct groups named, typed fields into a single record; each field holds its own value independently."),
            },
            ["if-else"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "if-else-branch",
                    "An if/else evaluates a condition and routes execution to exactly one branch; the false branch runs when the condition is not satisfied."),
            },
            ["maps"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "map-entries",
                    "A Dictionary maps each key to one value; looking up a missing key returns a zero value by default, or false from TryGetValue."),
            },
            ["slices"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "slice-window",
                    "GetRange returns a new list containing the elements from the given start index for the given count, leaving the original list unchanged."),
            },
            ["pointers"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "ref-alias",
                    "A ref parameter is an alias — both the caller's variable and the parameter name point at the same storage location, so writes inside the method are visible outside."),
            },
        };

    /// <summary>
    /// Returns all (svg, caption) pairs for the given example slug.
    /// Returns an empty sequence if no figures are attached.
    /// </summary>
    public static IEnumerable<(string Svg, string Caption)> GetFigures(string slug)
    {
        if (!Attachments.TryGetValue(slug, out var attachments))
            yield break;

        foreach (var att in attachments)
        {
            var svg = Figures.RenderSvg(att.FigureName);
            if (!string.IsNullOrEmpty(svg))
                yield return (svg, att.Caption);
        }
    }
}
