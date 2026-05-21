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
            ["arrays"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "array-index",
                    "An array is a fixed-length sequence of elements; each element is stored at a zero-based index and accessed in constant time."),
            },
            ["switch"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "switch-cases",
                    "A switch tests one value against every case label in turn; exactly one matching arm executes, and unmatched arms are skipped entirely."),
            },
            ["functions"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "function-call",
                    "Calling a function passes arguments in and receives a return value out; the caller resumes with that value once the function returns."),
            },
            ["multiple-return-values"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "multi-return",
                    "A tuple return lets a single function call hand back several values at once; the caller deconstructs them with var (a, b) = …"),
            },
            ["hello-world"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "console-writeline",
                    "Console.WriteLine writes its argument to standard output followed by a newline; the text appears immediately in the terminal."),
            },
            ["constants"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "const-value",
                    "A const value is resolved at compile time and inlined wherever it is used; no memory slot is allocated for it at runtime."),
            },
            ["enums"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "enum-values",
                    "An enum assigns a name to each underlying integer value; the compiler enforces that only valid members are used."),
            },
            ["methods"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "method-slots",
                    "A struct bundles named fields and methods into a single type; methods receive the struct value and can compute derived results from its fields."),
            },
            ["interfaces"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "interface-contract",
                    "An interface declares a contract — any type that provides the required methods satisfies it, enabling polymorphism without inheritance."),
            },
            ["errors"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "exception-flow",
                    "When no exception is thrown the try block completes normally and execution continues; if an exception is thrown it is caught by the matching catch block."),
            },
            ["generics"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "generic-box",
                    "A generic type is parameterised by a type variable T; each concrete instantiation substitutes a real type for T, sharing the same implementation."),
            },
            ["sorting"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "sort-before-after",
                    "List.Sort rearranges elements in ascending order in place; the original positions are discarded and replaced by the sorted sequence."),
            },
            ["variadic-functions"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "params-array",
                    "A params parameter collects any number of call-site arguments into a single array; the caller passes them as a comma-separated list with no extra syntax."),
            },
            ["defer"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "lifo-defer",
                    "Deferred cleanup actions are registered in order and executed in reverse (LIFO) when the enclosing scope exits, whether normally or by exception."),
            },
            ["goroutines"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "goroutine-lanes",
                    "Two goroutines (or Tasks) run concurrently on separate execution lanes; the runtime interleaves them with no guaranteed ordering between the lanes."),
            },
            ["channels"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "channel-buffer",
                    "A buffered channel decouples producer and consumer; the producer can send up to the buffer capacity without blocking, and the consumer reads whenever it is ready."),
            },
            ["timers"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "timer-tick",
                    "A timer fires a single tick event on its channel after the specified duration; reading from the channel blocks until the deadline is reached."),
            },
            ["url-parsing"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "url-parts",
                    "Uri.Parse splits a URL into its components — scheme, host, path, and query string — each of which can be read or rebuilt independently."),
            },
            ["json"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "json-serialize",
                    "JsonSerializer.Serialize converts a C# object to a JSON string; JsonSerializer.Deserialize reconstructs an equivalent object from that string."),
            },
            ["string-formatting"] = new[]
            {
                new FigureAttachment(
                    "after-last",
                    "format-placeholders",
                    "An interpolated string embeds expression placeholders directly in the string literal; at runtime each placeholder is replaced by the value of its expression."),
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
