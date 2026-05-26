// Canvas is a C# port of the Python marginalia_grammar.py from
// pythonbyexample (https://github.com/adewale/pythonbyexample). It provides
// a locked visual vocabulary for generating SVG diagram figures that accompany
// example pages.
//
// The grammar enforces a single visual language. Figure paint functions compose
// diagrams from the words and phrases defined here; metrics, palette, stroke
// weights, and typography are locked at class level. There is no escape hatch
// for raw SVG.

using System.Text;

namespace dotnetbyexample.Marginalia;

/// <summary>
/// Stateful SVG canvas. Call drawing methods to build up a diagram, then call
/// <see cref="ToSvg"/> to render the accumulated parts as an SVG element.
/// </summary>
public class Canvas
{
    // ── Palette ────────────────────────────────────────────────────────────
    // Aligned with nocco.css design tokens. These are the only colours that
    // figures may use; paint functions never pick a colour directly.
    //   Ink        warm dark brown
    //   InkSoft    Ink at 70%
    //   Emphasis   brand orange (used sparingly — at most one mark per figure)
    //   SoftFill   Ink at 5% (quiet container tint)
    private const string Ink = "#521000";
    private const string InkSoft = "rgba(82, 16, 0, 0.7)";
    private const string Emphasis = "#FF4801";
    private const string SoftFill = "rgba(82, 16, 0, 0.05)";

    // ── Stroke weights ─────────────────────────────────────────────────────
    private const double WHairline = 0.6;
    private const double WStroke = 1.0;
    private const double WEmphasis = 1.4;
    private const double WGhost = 0.5;
    private const double GhostOpacity = 0.4;
    private const string Dash = "2 2";

    // ── Locked geometry — never override per figure ────────────────────────
    private const double DotR = 2.5;
    private const double TickLen = 6;
    private const int NodeR = 14;
    private const double ArrowOpen = 5;
    private const double ArrowClosed = 7;

    /// <summary>Standard name-box width in viewBox units.</summary>
    public const int NameW = 60;
    /// <summary>Standard name-box height in viewBox units.</summary>
    public const int NameH = 24;
    /// <summary>Standard object-box width in viewBox units.</summary>
    public const int ObjectW = 80;
    /// <summary>Standard object-box height in viewBox units.</summary>
    public const int ObjectH = 32;
    /// <summary>Standard cell size in viewBox units.</summary>
    public const int Cell = 24;
    /// <summary>Standard word width in viewBox units.</summary>
    public const int WordW = 44;

    // ── Typography ─────────────────────────────────────────────────────────
    private const string FontSerif = "'Iowan Old Style', Charter, Georgia, serif";
    private const string FontMono = "'JetBrains Mono', 'IBM Plex Mono', Menlo, monospace";
    private const string FontSans = "-apple-system, 'Source Sans Pro', sans-serif";
    private const int SizeBody = 11;
    private const int SizeMono = 10;
    private const int SizeSmall = 9;
    private const int SizeTag = 8;
    private const int Baseline = 4;

    // ── Render scale ───────────────────────────────────────────────────────
    // Figures render at IntrinsicScale × their viewBox dimensions so they
    // appear larger on desktop while still scaling down for narrow viewports
    // via CSS max-width: 100%.
    private const double IntrinsicScale = 1.6;

    /// <summary>Canvas width in viewBox units.</summary>
    public int W { get; }
    /// <summary>Canvas height in viewBox units.</summary>
    public int H { get; }

    private readonly List<string> _parts = new();

    /// <summary>Create a canvas of the given viewBox dimensions.</summary>
    public Canvas(int w = 320, int h = 110)
    {
        W = w;
        H = h;
    }

    private void Add(string s) => _parts.Add(s);

    // ── Private line helper ────────────────────────────────────────────────
    private void DrawLine(double x1, double y1, double x2, double y2,
        string color = Ink, double weight = WStroke, string? dash = null, double opacity = 1.0)
    {
        var sb = new StringBuilder();
        sb.Append($"<line x1=\"{F(x1)}\" y1=\"{F(y1)}\" x2=\"{F(x2)}\" y2=\"{F(y2)}\"");
        sb.Append($" stroke=\"{color}\" stroke-width=\"{F(weight)}\"");
        if (dash != null) sb.Append($" stroke-dasharray=\"{dash}\"");
        if (opacity < 1.0) sb.Append($" opacity=\"{F(opacity)}\"");
        sb.Append("/>");
        Add(sb.ToString());
    }

    // ── Tokens (atomic marks) ──────────────────────────────────────────────

    /// <summary>Thin hairline between two points.</summary>
    public void Hairline(double x1, double y1, double x2, double y2)
        => DrawLine(x1, y1, x2, y2, weight: WHairline);

    /// <summary>Standard-weight stroke between two points.</summary>
    public void Stroke(double x1, double y1, double x2, double y2)
        => DrawLine(x1, y1, x2, y2);

    /// <summary>Ghost (faded) stroke between two points.</summary>
    public void Ghost(double x1, double y1, double x2, double y2)
        => DrawLine(x1, y1, x2, y2, weight: WGhost, opacity: GhostOpacity);

    /// <summary>Dashed hairline between two points.</summary>
    public void Dashed(double x1, double y1, double x2, double y2)
        => DrawLine(x1, y1, x2, y2, weight: WHairline, dash: Dash);

    /// <summary>Small filled circle. Use <paramref name="emphasis"/> to paint it in the accent colour.</summary>
    public void Dot(double x, double y, bool emphasis = false)
        => Add($"<circle cx=\"{F(x)}\" cy=\"{F(y)}\" r=\"{F(DotR)}\" fill=\"{(emphasis ? Emphasis : Ink)}\"/>");

    /// <summary>Short vertical tick centred on (x, y).</summary>
    public void Tick(double x, double y, double? length = null)
    {
        var len = length ?? TickLen;
        Hairline(x, y - len / 2, x, y + len / 2);
    }

    // ── Text helpers ───────────────────────────────────────────────────────
    private void DrawText(double x, double y, string s,
        string family, int size, string anchor, string color,
        bool italic = false, string? tracking = null)
    {
        var sb = new StringBuilder();
        sb.Append($"<text x=\"{F(x)}\" y=\"{F(y)}\"");
        sb.Append($" font-family=\"{family}\" font-size=\"{size}\"");
        sb.Append($" fill=\"{color}\" text-anchor=\"{anchor}\"");
        if (italic) sb.Append(" font-style=\"italic\"");
        if (tracking != null) sb.Append($" letter-spacing=\"{tracking}\"");
        sb.Append($">{HtmlEncode(s)}</text>");
        Add(sb.ToString());
    }

    /// <summary>Monospaced text centred at (x, y).</summary>
    public void Mono(double x, double y, string s, string anchor = "middle", int? size = null, string? color = null)
        => DrawText(x, y, s, FontMono, size ?? SizeMono, anchor, color ?? Ink);

    /// <summary>Serif italic identifier centred at (x, y).</summary>
    public void Ident(double x, double y, string s, string anchor = "middle", string? color = null)
        => DrawText(x, y, s, FontSerif, SizeBody, anchor, color ?? Ink, italic: true);

    /// <summary>Small sans-serif label in muted colour.</summary>
    public void Label(double x, double y, string s, string anchor = "start")
        => DrawText(x, y, s, FontSans, SizeSmall, anchor, InkSoft);

    /// <summary>Extra-small uppercase sans-serif tag in muted colour.</summary>
    public void Tag(double x, double y, string s, string anchor = "start")
        => DrawText(x, y, s.ToUpperInvariant(), FontSans, SizeTag, anchor, InkSoft, tracking: "0.5");

    // ── Words (composable shapes) ──────────────────────────────────────────

    /// <summary>Open rect with italic identifier. Returns the right-edge midpoint.</summary>
    public (double Rx, double Ry) NameBox(double x, double y, string name)
    {
        Add($"<rect x=\"{F(x)}\" y=\"{F(y)}\" width=\"{NameW}\" height=\"{NameH}\" fill=\"none\" stroke=\"{Ink}\" stroke-width=\"{F(WStroke)}\"/>");
        Ident(x + NameW / 2.0, y + NameH / 2.0 + Baseline, name);
        return (x + NameW, y + NameH / 2.0);
    }

    /// <summary>
    /// Filled rect with type tag and value centred. Returns the left-edge midpoint.
    /// </summary>
    public (double Lx, double Ly) ObjectBox(double x, double y, string typeTag, string value,
        int? w = null, int? h = null, bool soft = true, string tagPosition = "above")
    {
        var bw = w ?? ObjectW;
        var bh = h ?? ObjectH;
        var fill = soft ? SoftFill : "none";
        Add($"<rect x=\"{F(x)}\" y=\"{F(y)}\" width=\"{bw}\" height=\"{bh}\" fill=\"{fill}\" stroke=\"{Ink}\" stroke-width=\"{F(WStroke)}\"/>");
        if (!string.IsNullOrEmpty(typeTag))
        {
            var tagY = tagPosition == "inside" ? y + SizeTag + 2 : y - 5;
            Tag(x + 4, tagY, typeTag);
        }
        if (!string.IsNullOrEmpty(value))
            Mono(x + bw / 2.0, y + bh / 2.0 + Baseline, value);
        return (x, y + bh / 2.0);
    }

    /// <summary>Single cell (box with optional text).</summary>
    public void CellBox(double x, double y, string content = "",
        int? w = null, int? h = null, bool ghost = false, bool soft = false)
    {
        var cw = w ?? Cell;
        var ch = h ?? Cell;
        var weight = ghost ? WGhost : WStroke;
        var opacityAttr = ghost ? $" opacity=\"{F(GhostOpacity)}\"" : "";
        var fill = soft ? SoftFill : "none";
        Add($"<rect x=\"{F(x)}\" y=\"{F(y)}\" width=\"{cw}\" height=\"{ch}\" fill=\"{fill}\" stroke=\"{Ink}\" stroke-width=\"{F(weight)}\"{opacityAttr}/>");
        if (!string.IsNullOrEmpty(content))
            Mono(x + cw / 2.0, y + ch / 2.0 + Baseline, content);
    }

    /// <summary>Row of cells. <paramref name="items"/> provides each cell's text (use "" for empty).</summary>
    public void Cells(double x, double y, IEnumerable<string> items, int? w = null, int? h = null)
    {
        var cw = w ?? Cell;
        var ch = h ?? Cell;
        int i = 0;
        foreach (var item in items)
        {
            CellBox(x + i * cw, y, item, w: cw, h: ch);
            i++;
        }
    }

    /// <summary>
    /// Horizontal hairline with optional regular ticks (a "number line").
    /// When <paramref name="divisions"/> is specified, evenly-spaced ticks are drawn.
    /// </summary>
    public void Register(double x, double y, double w, int? divisions = null, bool between = false)
    {
        Hairline(x, y, x + w, y);
        if (divisions is null) return;
        var step = w / divisions.Value;
        for (int i = 0; i <= divisions.Value; i++)
            Tick(x + i * step, y);
        if (between)
        {
            for (int i = 0; i < divisions.Value; i++)
                Hairline(x + i * step + step / 2, y - TickLen / 3.0,
                         x + i * step + step / 2, y + TickLen / 3.0);
        }
    }

    /// <summary>Circle node with a centred label.</summary>
    public void Node(double x, double y, string label, int? r = null, bool ghost = false)
    {
        var nr = r ?? NodeR;
        var weight = ghost ? WGhost : WStroke;
        var opacityAttr = ghost ? $" opacity=\"{F(GhostOpacity)}\"" : "";
        Add($"<circle cx=\"{F(x)}\" cy=\"{F(y)}\" r=\"{nr}\" fill=\"none\" stroke=\"{Ink}\" stroke-width=\"{F(weight)}\"{opacityAttr}/>");
        Mono(x, y + Baseline, label, size: SizeSmall);
    }

    /// <summary>Open rectangle with optional type-tag label.</summary>
    public void Frame(double x, double y, double w, double h, string? label = null, bool ghost = false)
    {
        var weight = ghost ? WGhost : WStroke;
        var opacityAttr = ghost ? $" opacity=\"{F(GhostOpacity)}\"" : "";
        Add($"<rect x=\"{F(x)}\" y=\"{F(y)}\" width=\"{F(w)}\" height=\"{F(h)}\" fill=\"none\" stroke=\"{Ink}\" stroke-width=\"{F(weight)}\"{opacityAttr}/>");
        if (label != null) Tag(x + 6, y - 3, label);
    }

    // ── Arrows ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Axis-style open arrow: hairline ending in a tiny open V.
    /// </summary>
    public void OpenArrow(double x1, double y1, double x2, double y2)
    {
        Hairline(x1, y1, x2, y2);
        var (dx, dy) = (x2 - x1, y2 - y1);
        var L = Math.Sqrt(dx * dx + dy * dy);
        if (L == 0) return;
        var (ux, uy) = (dx / L, dy / L);
        var (bx, by) = (x2 - ArrowOpen * ux, y2 - ArrowOpen * uy);
        var (px, py) = (-uy * (ArrowOpen / 2), ux * (ArrowOpen / 2));
        Add($"<path d=\"M{F(x2)},{F(y2)} L{F(bx + px)},{F(by + py)} M{F(x2)},{F(y2)} L{F(bx - px)},{F(by - py)}\" stroke=\"{Ink}\" stroke-width=\"{F(WHairline)}\" fill=\"none\"/>");
    }

    /// <summary>
    /// Filled-wedge arrow. Use <paramref name="emphasis"/> only for the single live
    /// arrow per figure to keep the scarcity rule.
    /// </summary>
    public void ClosedArrow(double x1, double y1, double x2, double y2, bool emphasis = false)
    {
        var color = emphasis ? Emphasis : Ink;
        var weight = emphasis ? WEmphasis : WStroke;
        var (dx, dy) = (x2 - x1, y2 - y1);
        var L = Math.Sqrt(dx * dx + dy * dy);
        if (L == 0) return;
        var (ux, uy) = (dx / L, dy / L);
        var (endX, endY) = (x2 - ArrowClosed * ux, y2 - ArrowClosed * uy);
        DrawLine(x1, y1, endX, endY, color, weight);
        var (bx, by) = (x2 - ArrowClosed * ux, y2 - ArrowClosed * uy);
        var (px, py) = (-uy * (ArrowClosed / 2.5), ux * (ArrowClosed / 2.5));
        Add($"<polygon points=\"{F(x2)},{F(y2)} {F(bx + px)},{F(by + py)} {F(bx - px)},{F(by - py)}\" fill=\"{color}\"/>");
    }

    // ── Phrases (recurring multi-word constructions) ───────────────────────

    /// <summary>The foundational "name → object" picture.</summary>
    public void Bind(double x, double y, string name, string typeTag, string value,
        int objectW = ObjectW, int gap = 40)
    {
        var (nx, ny) = NameBox(x, y + (ObjectH - NameH) / 2.0, name);
        var (ox, oy) = ObjectBox(nx + gap, y, typeTag, value, w: objectW);
        ClosedArrow(nx + 2, ny, ox - 2, oy);
    }

    // ── Render ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Render all accumulated drawing commands as a self-contained SVG element.
    ///
    /// The SVG element renders at <c>IntrinsicScale</c> × its viewBox dimensions.
    /// CSS <c>max-width: 100%</c> on the container is required so the figure
    /// scales down on narrow viewports without being stretched.
    /// </summary>
    public string ToSvg()
    {
        const int padTop = 14, padX = 14, padBottom = 14;
        var vbW = W + 2 * padX;
        var vbH = H + padTop + padBottom;
        var outW = (int)Math.Round(vbW * IntrinsicScale);
        var outH = (int)Math.Round(vbH * IntrinsicScale);
        return $"<svg viewBox=\"-{padX} -{padTop} {vbW} {vbH}\" width=\"{outW}\" height=\"{outH}\" xmlns=\"http://www.w3.org/2000/svg\">"
            + string.Concat(_parts)
            + "</svg>";
    }

    // ── Helpers ────────────────────────────────────────────────────────────

    /// <summary>Format a floating-point value with up to two decimal places, no trailing zeros.</summary>
    private static string F(double v) => v == Math.Floor(v) ? ((int)v).ToString() : v.ToString("0.##");

    private static string HtmlEncode(string s) =>
        s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
}
