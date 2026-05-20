// Figure paint functions and registry for .NET by Example.
//
// Each function receives a Canvas and draws one diagram using only
// the canvas primitives — no raw SVG, no colour choices outside the palette.
//
// The Registry dictionary maps a figure name to its paint function and the
// (width, height) of the canvas it should receive.

namespace dotnetbyexample.Marginalia;

/// <summary>
/// Named figure paint functions and their canvas-size metadata.
/// </summary>
public static class Figures
{
    // ── Example figures ────────────────────────────────────────────────────

    /// <summary>
    /// Numbers · int is a 32-bit fixed-range type; double spacing widens at extremes.
    /// </summary>
    public static void NumberLines(Canvas c)
    {
        c.Tag(0, 8, "int · 32-bit");
        c.Register(0, 22, 260, divisions: 8);
        c.Tag(0, 50, "double · representable spacing widens");
        c.Register(0, 64, 260);
        foreach (int x in new[] { 10, 30, 60, 100, 130, 140, 148, 160, 188, 230, 260 })
            c.Tick(x, 64);
        c.Dot(140, 64, emphasis: true);
    }

    /// <summary>
    /// Values · every literal is a typed object: int, string, list, dict each carry
    /// their own behaviour.
    /// </summary>
    public static void ValueTypes(Canvas c)
    {
        var rows = new[] { ("int", "42"), ("string", "\"hi\""), ("bool", "true"), ("double", "3.14") };
        for (int i = 0; i < rows.Length; i++)
        {
            var (t, v) = rows[i];
            c.ObjectBox(0, i * 30, t, v, w: 160, h: 26, tagPosition: "inside");
        }
    }

    /// <summary>
    /// Recursion · stacked frames of the same function with decreasing arguments.
    /// </summary>
    public static void CallStack(Canvas c)
    {
        var chain = new[] { 3, 2, 1, 0 };
        for (int i = 0; i < chain.Length; i++)
        {
            var suffix = chain[i] == 0 ? " ← base" : "";
            c.CellBox(0, i * 22, $"Factorial({chain[i]}){suffix}", w: 180, h: 20);
        }
        c.Dashed(192, 90, 192, 18);
        c.ClosedArrow(192, 30, 192, 18, emphasis: true);
    }

    /// <summary>
    /// Closures · the inner function keeps a reference to the outer scope's captured variable.
    /// </summary>
    public static void ClosureCell(Canvas c)
    {
        c.Frame(0, 16, 240, 96, label: "MakeMultiplier");
        c.Tag(16, 32, "captured");
        c.CellBox(16, 38, "factor=2", w: 84, h: 22);
        c.Frame(112, 38, 122, 60, label: "Multiply");
        c.Label(173, 76, "uses capture", anchor: "middle");
        c.ClosedArrow(128, 76, 102, 56, emphasis: true);
    }

    /// <summary>
    /// Variables · a name is a reference that points at an object.
    /// </summary>
    public static void VariablesBind(Canvas c)
    {
        c.Bind(0, 6, "x", "int", "42", objectW: 70, gap: 20);
    }

    /// <summary>
    /// For loops · a counter advances one cell at a time; the loop stops
    /// when the range is exhausted.
    /// </summary>
    public static void ForLoop(Canvas c)
    {
        c.Tag(0, 6, "for i in range(4)");
        c.Cells(0, 14, new[] { "0", "1", "2", "3" }, w: 28, h: 24);
        // Orange dot above cell index 2 (x = 2*28 + 14 = 70) marks the current position
        c.Dot(70, 10, emphasis: true);
        c.Label(118, 26, "← stop here");
    }

    // ── Registry ───────────────────────────────────────────────────────────

    /// <summary>
    /// Maps a figure name to its paint action and canvas dimensions
    /// (width, height) in viewBox units.
    /// </summary>
    public static readonly IReadOnlyDictionary<string, (Action<Canvas> Paint, int Width, int Height)> Registry =
        new Dictionary<string, (Action<Canvas>, int, int)>
        {
            ["number-lines"]  = (NumberLines,  260, 78),
            ["value-types"]   = (ValueTypes,   160, 120),
            ["call-stack"]    = (CallStack,     200, 100),
            ["closure-cell"]  = (ClosureCell,   240, 120),
            ["variables-bind"]= (VariablesBind, 180, 44),
            ["for-loop"]      = (ForLoop,       210, 46),
        };

    /// <summary>
    /// Render a named figure to an SVG string. Returns an empty string if the
    /// figure name is not registered.
    /// </summary>
    public static string RenderSvg(string figureName)
    {
        if (!Registry.TryGetValue(figureName, out var entry))
            return string.Empty;
        var canvas = new Canvas(entry.Width, entry.Height);
        entry.Paint(canvas);
        return canvas.ToSvg();
    }
}
