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

    /// <summary>
    /// Arrays · elements are stored in a fixed-length sequence accessed by zero-based index.
    /// </summary>
    public static void ArrayIndex(Canvas c)
    {
        c.Tag(0, 6, "int[] b = {1,2,3,4,5}");
        c.Cells(0, 14, new[] { "1", "2", "3", "4", "5" }, w: 28, h: 24);
        // Orange dot above cell index 4 — the element being accessed
        c.Dot(4 * 28 + 14, 10, emphasis: true);
        c.Label(4 * 28 + 4, 46, "b[4] = 5");
    }

    /// <summary>
    /// Switch · a switch value is tested against each case; the matching arm runs.
    /// </summary>
    public static void SwitchCases(Canvas c)
    {
        // Switch-value box centered at top
        c.CellBox(70, 2, "i = 2", w: 60, h: 22);

        // Three outcome boxes
        c.CellBox(0, 54, "one", w: 56, h: 22);
        c.CellBox(72, 54, "two", w: 56, h: 22);
        c.CellBox(144, 54, "three", w: 56, h: 22);

        // Arrows from condition bottom-center to each case
        c.ClosedArrow(100, 24, 28, 54);
        c.ClosedArrow(100, 24, 100, 54, emphasis: true);  // i == 2 matches
        c.ClosedArrow(100, 24, 172, 54);

        // Case labels above the outcome boxes
        c.Tag(4, 50, "case 1");
        c.Tag(76, 50, "case 2");
        c.Tag(148, 50, "case 3");
    }

    /// <summary>
    /// Functions · calling a function passes arguments and receives a return value.
    /// </summary>
    public static void FunctionCall(Canvas c)
    {
        // Function call box
        c.CellBox(0, 10, "Plus(1, 2)", w: 100, h: 26);
        // Return value box
        c.ObjectBox(138, 2, "int", "3", w: 60, h: 42, tagPosition: "inside");
        // Arrow showing the return — emphasis marks the result as the key concept
        c.ClosedArrow(102, 23, 136, 23, emphasis: true);
        c.Label(106, 56, "returns int");
    }

    /// <summary>
    /// Multiple return values · a tuple return lets one call yield several results.
    /// </summary>
    public static void MultiReturn(Canvas c)
    {
        // Function box left-center
        c.CellBox(0, 20, "Vals()", w: 70, h: 26);

        // Two return-value boxes stacked on the right
        c.ObjectBox(112, 4, "int", "3", w: 60, h: 28, tagPosition: "inside");
        c.ObjectBox(112, 38, "int", "7", w: 60, h: 28, tagPosition: "inside");

        // Two arrows fanning out from the right edge of Vals()
        c.ClosedArrow(72, 28, 110, 18);
        c.ClosedArrow(72, 36, 110, 52, emphasis: true);
    }

    /// <summary>
    /// Hello world · Console.WriteLine emits text to standard output.
    /// </summary>
    public static void HelloWorldPrint(Canvas c)
    {
        c.CellBox(0, 10, "Console.WriteLine", w: 130, h: 24);
        c.ObjectBox(158, 2, "stdout", "\"Hello, World!\"", w: 110, h: 40, tagPosition: "inside");
        c.ClosedArrow(132, 22, 156, 22, emphasis: true);
    }

    /// <summary>
    /// Constants · a const is baked in at compile time; no memory slot is
    /// allocated at runtime.
    /// </summary>
    public static void ConstValue(Canvas c)
    {
        c.Tag(0, 6, "const · compile-time");
        c.CellBox(0, 12, "MaxScore = 100", w: 140, h: 24, soft: true);
        c.Label(148, 18, "no memory");
        c.Label(148, 30, "slot at runtime");
    }

    /// <summary>
    /// Enums · each named member maps to a fixed underlying integer value.
    /// </summary>
    public static void EnumValues(Canvas c)
    {
        c.Tag(0, 6, "ServerState");
        c.ObjectBox(0,  12, "0", "Idle",   w: 60, h: 28, tagPosition: "inside");
        c.ObjectBox(64, 12, "1", "Normal", w: 60, h: 28, tagPosition: "inside");
        c.ObjectBox(128,12, "2", "Error",  w: 60, h: 28, tagPosition: "inside");
    }

    /// <summary>
    /// Methods · a struct bundles fields and methods into one named type.
    /// </summary>
    public static void MethodSlots(Canvas c)
    {
        c.Frame(0, 0, 180, 90, label: "Rect");
        c.Tag(10, 18, "fields");
        c.CellBox(10, 24, "w=3", w: 70, h: 20);
        c.CellBox(94, 24, "h=4", w: 70, h: 20);
        c.Tag(10, 56, "methods");
        c.CellBox(10, 62, "Area()", w: 70, h: 22, soft: true);
        c.CellBox(94, 62, "Perim()", w: 70, h: 22, soft: true);
    }

    /// <summary>
    /// Interfaces · multiple concrete types satisfy one shared interface contract.
    /// </summary>
    public static void InterfaceContract(Canvas c)
    {
        c.Frame(50, 0, 120, 24, label: "IShape");
        c.CellBox(52, 2, "Area() float64", w: 116, h: 20);
        c.CellBox(0,  62, "Rect",   w: 90, h: 24);
        c.CellBox(130,62, "Circle", w: 90, h: 24);
        c.ClosedArrow(45, 62, 95, 26);
        c.ClosedArrow(175, 62, 125, 26, emphasis: true);
        c.Label(2,   56, "implements");
        c.Label(132, 56, "implements");
    }

    /// <summary>
    /// Errors · the normal path returns a value; an exception routes to catch.
    /// </summary>
    public static void ExceptionFlow(Canvas c)
    {
        c.CellBox(60, 0, "try { … }", w: 90, h: 22);
        c.CellBox(0,  62, "return ok",  w: 80, h: 22);
        c.CellBox(130,62, "catch { … }", w: 80, h: 22);
        c.ClosedArrow(80,  22, 40,  62);
        c.ClosedArrow(130, 22, 170, 62, emphasis: true);
        c.Label(4,   56, "no throw");
        c.Label(132, 56, "exception");
    }

    /// <summary>
    /// Generics · a single generic type is instantiated with different type
    /// arguments at each use site.
    /// </summary>
    public static void GenericBox(Canvas c)
    {
        c.Frame(55, 0, 100, 24, label: "Stack<T>");
        c.CellBox(57, 2, "T", w: 96, h: 20);
        c.CellBox(0,   58, "Stack<int>",    w: 96, h: 24);
        c.CellBox(114, 58, "Stack<string>", w: 96, h: 24);
        c.ClosedArrow(48,  58, 90,  26);
        c.ClosedArrow(162, 58, 120, 26, emphasis: true);
    }

    /// <summary>
    /// Sorting · a list of unsorted elements becomes ordered after the sort call.
    /// </summary>
    public static void SortBeforeAfter(Canvas c)
    {
        c.Tag(0, 6, "before");
        c.Cells(0, 12, new[] { "3", "1", "4", "2" }, w: 28, h: 24);
        c.ClosedArrow(56, 42, 56, 54, emphasis: true);
        c.Tag(0, 58, "after · sorted");
        c.Cells(0, 64, new[] { "1", "2", "3", "4" }, w: 28, h: 24);
    }

    /// <summary>
    /// Variadic functions · individual arguments collapse into a single params
    /// array inside the callee.
    /// </summary>
    public static void ParamsArray(Canvas c)
    {
        c.Tag(0, 6, "arguments");
        c.CellBox(0, 12, "1", w: 30, h: 24);
        c.CellBox(36, 12, "2", w: 30, h: 24);
        c.CellBox(72, 12, "3", w: 30, h: 24);
        c.ClosedArrow(106, 24, 126, 24, emphasis: true);
        c.Tag(134, 6, "params int[]");
        c.Cells(134, 12, new[] { "1", "2", "3" }, w: 26, h: 24);
    }

    /// <summary>
    /// Defer · deferred calls are pushed onto a stack and run LIFO when the
    /// enclosing function returns.
    /// </summary>
    public static void LifoDefer(Canvas c)
    {
        c.Tag(16, 6, "registered (LIFO)");
        c.CellBox(16, 12, "Open(file)",      w: 140, h: 20);
        c.CellBox(16, 36, "Start(timer)",    w: 140, h: 20);
        c.CellBox(16, 60, "Lock(mutex) · first", w: 140, h: 20, soft: true);
        c.Dashed(4, 80, 4, 30);
        c.ClosedArrow(4, 30, 4, 20, emphasis: true);
    }

    /// <summary>
    /// Goroutines · two goroutines run concurrently on separate execution lanes
    /// with no guaranteed ordering between them.
    /// </summary>
    public static void GoroutineLanes(Canvas c)
    {
        c.Tag(0, 6, "goroutine A");
        c.Stroke(0, 20, 210, 20);
        c.CellBox(20, 8, "f()", w: 50, h: 24);
        c.Tag(0, 42, "goroutine B");
        c.Stroke(0, 56, 210, 56);
        c.CellBox(80, 44, "f()", w: 50, h: 24, soft: true);
        c.Label(0, 72, "concurrent — no fixed order");
    }

    /// <summary>
    /// Channels · a buffered channel decouples producer and consumer; the buffer
    /// absorbs bursts up to its capacity.
    /// </summary>
    public static void ChannelBuffer(Canvas c)
    {
        c.CellBox(0, 14, "send", w: 50, h: 26);
        c.Tag(64, 4, "channel buf=3");
        c.Cells(64, 10, new[] { "A", "B", "_" }, w: 28, h: 26);
        c.CellBox(162, 14, "recv", w: 50, h: 26);
        c.ClosedArrow(52, 27, 62, 27);
        c.ClosedArrow(150, 27, 160, 27, emphasis: true);
    }

    /// <summary>
    /// Timers · a timer fires a single tick event when the deadline is reached.
    /// </summary>
    public static void TimerTick(Canvas c)
    {
        c.Tag(0, 8, "time");
        c.Register(0, 18, 200);
        c.Dot(160, 18, emphasis: true);
        c.Hairline(160, 18, 160, 36);
        c.Tag(138, 40, "tick event");
    }

    /// <summary>
    /// URL parsing · a URL is decomposed into scheme, host, path, and query.
    /// </summary>
    public static void UrlParts(Canvas c)
    {
        c.ObjectBox(0,   6, "scheme", "https",       w: 60, h: 28, tagPosition: "inside");
        c.ObjectBox(64,  6, "host",   "example.com", w: 90, h: 28, tagPosition: "inside");
        c.ObjectBox(158, 6, "path",   "/foo",        w: 52, h: 28, tagPosition: "inside");
        c.ObjectBox(214, 6, "query",  "q=1",         w: 52, h: 28, tagPosition: "inside");
    }

    /// <summary>
    /// JSON · a C# object is serialised to a JSON string and can be deserialised
    /// back to an equivalent object.
    /// </summary>
    public static void JsonSerialize(Canvas c)
    {
        c.Frame(0, 4, 96, 46, label: "C# object");
        c.CellBox(8, 20, "Name=Bob", w: 80, h: 20);
        c.ClosedArrow(98, 27, 118, 27, emphasis: true);
        c.Frame(120, 4, 112, 46, label: "JSON");
        c.CellBox(128, 20, "{\"Name\":\"Bob\"}", w: 96, h: 20);
    }

    /// <summary>
    /// String formatting · placeholders in a format string are replaced with
    /// the corresponding argument values at runtime.
    /// </summary>
    public static void FormatPlaceholders(Canvas c)
    {
        c.CellBox(0, 0, "$\"Hello, {name}!\"", w: 160, h: 24, soft: true);
        c.ObjectBox(90, 40, "string", "\"World\"", w: 80, h: 28, tagPosition: "inside");
        c.ClosedArrow(118, 24, 128, 38, emphasis: true);
        c.Label(0, 82, "→ \"Hello, World!\"");
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
            ["array-index"]   = (ArrayIndex,    155, 52),
            ["switch-cases"]  = (SwitchCases,   205, 84),
            ["function-call"] = (FunctionCall,  200, 62),
            ["multi-return"]        = (MultiReturn,        175,  72),
            ["console-writeline"]   = (HelloWorldPrint,    272,  46),
            ["const-value"]         = (ConstValue,         240,  42),
            ["enum-values"]         = (EnumValues,         195,  42),
            ["method-slots"]        = (MethodSlots,        180,  90),
            ["interface-contract"]  = (InterfaceContract,  220,  90),
            ["exception-flow"]      = (ExceptionFlow,      212,  88),
            ["generic-box"]         = (GenericBox,         212,  84),
            ["sort-before-after"]   = (SortBeforeAfter,    115,  92),
            ["params-array"]        = (ParamsArray,        215,  42),
            ["lifo-defer"]          = (LifoDefer,          168,  88),
            ["goroutine-lanes"]     = (GoroutineLanes,     215,  76),
            ["channel-buffer"]      = (ChannelBuffer,      215,  42),
            ["timer-tick"]          = (TimerTick,          205,  46),
            ["url-parts"]           = (UrlParts,           270,  36),
            ["json-serialize"]      = (JsonSerialize,      236,  52),
            ["format-placeholders"] = (FormatPlaceholders, 200,  86),
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
