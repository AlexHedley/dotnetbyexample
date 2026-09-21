using System.Text.RegularExpressions;

namespace Nocco;

internal static partial class HtmlSafety
{
    [GeneratedRegex(@"<\s*(script|iframe|object|embed|link|meta)[^>]*>[\s\S]*?<\s*/\s*\1\s*>", RegexOptions.IgnoreCase)]
    private static partial Regex DangerousElementRegex();

    [GeneratedRegex(@"\s+on[a-z]+\s*=\s*(['""]).*?\1", RegexOptions.IgnoreCase)]
    private static partial Regex EventHandlerRegex();

    [GeneratedRegex(@"\s+(href|src)\s*=\s*(['""])\s*javascript:[^'""]*\2", RegexOptions.IgnoreCase)]
    private static partial Regex JsProtocolRegex();

    [GeneratedRegex(@"<\s*(script|iframe|object|embed|link|meta)[^>]*/\s*>", RegexOptions.IgnoreCase)]
    private static partial Regex DangerousSelfClosingRegex();

    public static string SanitizeHtml(string html)
    {
        var safe = DangerousElementRegex().Replace(html, string.Empty);
        safe = DangerousSelfClosingRegex().Replace(safe, string.Empty);
        safe = EventHandlerRegex().Replace(safe, string.Empty);
        safe = JsProtocolRegex().Replace(safe, string.Empty);
        return safe;
    }
}
