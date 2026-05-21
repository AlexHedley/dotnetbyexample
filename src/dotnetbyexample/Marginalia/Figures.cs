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

    /// <summary>
    /// Structs · a struct groups named, typed fields into a single record.
    /// </summary>
    public static void StructFields(Canvas c)
    {
        // Outer frame labelled with the type name
        c.Frame(0, 0, 206, 96, label: "Person");

        // Row 1 — Name field
        var (nx1, ny1) = c.NameBox(10, 19, "Name");
        var (ox1, oy1) = c.ObjectBox(nx1 + 14, 14, "string", "\"Bob\"", w: 80, h: 34, tagPosition: "inside");
        c.ClosedArrow(nx1 + 2, ny1, ox1 - 2, oy1);

        // Row 2 — Age field (emphasis arrow)
        var (nx2, ny2) = c.NameBox(10, 59, "Age");
        var (ox2, oy2) = c.ObjectBox(nx2 + 14, 54, "int", "20", w: 80, h: 34, tagPosition: "inside");
        c.ClosedArrow(nx2 + 2, ny2, ox2 - 2, oy2, emphasis: true);
    }

    /// <summary>
    /// If/else · a condition routes execution to one of two branches.
    /// </summary>
    public static void IfElseBranch(Canvas c)
    {
        // Condition box across the top
        c.CellBox(60, 2, "num < 10?", w: 100, h: 22);

        // Outcome boxes
        c.CellBox(10, 56, "1 digit", w: 80, h: 22);
        c.CellBox(130, 56, "multiple", w: 80, h: 22);

        // Branch arrows from bottom of condition to top of each outcome
        c.ClosedArrow(95, 24, 50, 56);
        c.ClosedArrow(125, 24, 170, 56, emphasis: true);

        // Branch labels
        c.Label(32, 50, "true");
        c.Label(152, 50, "false");
    }

    /// <summary>
    /// Maps · a dictionary maps each key to a value; TryGetValue is the safe lookup.
    /// </summary>
    public static void MapEntries(Canvas c)
    {
        c.Tag(24, -2, "key");
        c.Tag(138, -2, "value");

        // Row 1
        c.ObjectBox(0, 0, "string", "\"k1\"", w: 90, h: 28, tagPosition: "inside");
        c.ObjectBox(120, 0, "int", "7", w: 60, h: 28, tagPosition: "inside");
        c.ClosedArrow(92, 14, 118, 14);

        // Row 2 (emphasis — the lookup value is the key concept)
        c.ObjectBox(0, 38, "string", "\"k2\"", w: 90, h: 28, tagPosition: "inside");
        c.ObjectBox(120, 38, "int", "13", w: 60, h: 28, tagPosition: "inside");
        c.ClosedArrow(92, 52, 118, 52, emphasis: true);
    }

    /// <summary>
    /// Slices · GetRange returns a contiguous sub-view starting at a given index.
    /// </summary>
    public static void SliceWindow(Canvas c)
    {
        c.Tag(0, 6, "GetRange(1, 2)");
        c.Cells(0, 14, new[] { "a", "b", "c", "d", "e" }, w: 28, h: 24);
        // Orange dot above cell index 1 (x = 1*28 + 14 = 42) — start of sub-view
        c.Dot(42, 10, emphasis: true);
        c.Label(28, 46, "→ [b, c]");
    }

    /// <summary>
    /// Pointers · a ref parameter creates an alias — both names share one object.
    /// </summary>
    public static void RefAlias(Canvas c)
    {
        var (nx1, ny1) = c.NameBox(0, 4, "i");
        var (nx2, ny2) = c.NameBox(0, 38, "iptr");
        var (ox, oy) = c.ObjectBox(90, 16, "int", "1", w: 70, h: 28, tagPosition: "inside");
        c.ClosedArrow(nx1 + 2, ny1, ox - 2, oy);
        c.ClosedArrow(nx2 + 2, ny2, ox - 2, oy, emphasis: true);
        c.Label(ox + 4, 12, "shared");
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
            ["struct-fields"] = (StructFields,  206, 96),
            ["if-else-branch"]= (IfElseBranch,  220, 82),
            ["map-entries"]   = (MapEntries,    180, 70),
            ["slice-window"]  = (SliceWindow,   150, 54),
            ["ref-alias"]     = (RefAlias,      165, 66),
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
