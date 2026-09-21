using dotnetbyexample.Marginalia;

namespace dotnetbyexample.Tests;

public class NoccoTests
{
    [Fact]
    public void ParseForTesting_SplitsDocsAndCodeSections()
    {
        var lines = new[]
        {
            "// intro",
            "var x = 1;",
            "// second",
            "var y = x + 1;"
        };

        var sections = Nocco.Nocco.ParseForTesting("sample.cs", lines);

        Assert.Equal(2, sections.Count);
        Assert.Contains("intro", sections[0].DocsHtml);
        Assert.Contains("var x = 1;", sections[0].CodeHtml);
        Assert.Contains("second", sections[1].DocsHtml);
    }

    [Theory]
    [InlineData("demo.cs", "csharp")]
    [InlineData("demo.csx", "csharp")]
    [InlineData("demo.fsx", "fsharp")]
    [InlineData("demo.vb", "vb.net")]
    [InlineData("demo.unknown", null)]
    public void GetLanguageName_ReturnsExpectedMapping(string source, string? expected)
    {
        var actual = Nocco.Nocco.GetLanguageName(source);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void FigureAttachments_GetFigures_ReturnsConfiguredFigure()
    {
        var figures = FigureAttachments.GetFigures("maps").ToList();

        Assert.NotEmpty(figures);
        Assert.Contains(figures, f => f.Caption.Contains("TryGetValue", StringComparison.OrdinalIgnoreCase));
        Assert.All(figures, figure => Assert.False(string.IsNullOrWhiteSpace(figure.Svg)));
    }

    [Fact]
    public void AnnotationAttachments_GetAnnotations_ReturnsConfiguredAnnotation()
    {
        var annotations = AnnotationAttachments.GetAnnotations("maps").ToList();

        Assert.NotEmpty(annotations);
        Assert.Contains(annotations, annotation => annotation.Text.Contains("TryGetValue", StringComparison.OrdinalIgnoreCase));
        Assert.All(annotations, annotation => Assert.Contains("ann", annotation.CssClass, StringComparison.Ordinal));
    }

    [Fact]
    public async Task GenerateAsync_GeneratesIndexAndExamplePage()
    {
        var tempRoot = Path.Combine(Path.GetTempPath(), "dotnetbyexample-tests", Guid.NewGuid().ToString("N"));
        var examplesRoot = Path.Combine(tempRoot, "examples");
        var siteRoot = Path.Combine(tempRoot, "site");
        var exampleDir = Path.Combine(examplesRoot, "sample");
        Directory.CreateDirectory(exampleDir);

        await File.WriteAllTextAsync(Path.Combine(exampleDir, "Sample.csx"), "// docs\nConsole.WriteLine(\"hi\");\n");
        await File.WriteAllTextAsync(Path.Combine(exampleDir, "Sample.bat"), ":: run\n");

        try
        {
            await Nocco.Nocco.GenerateAsync(examplesRoot, siteRoot);

            var indexPath = Path.Combine(siteRoot, "index.html");
            var samplePath = Path.Combine(siteRoot, "sample.html");

            Assert.True(File.Exists(indexPath), "index.html should be generated");
            Assert.True(File.Exists(samplePath), "sample.html should be generated");
            Assert.Contains("sample.html", await File.ReadAllTextAsync(indexPath), StringComparison.OrdinalIgnoreCase);
        }
        finally
        {
            if (Directory.Exists(tempRoot))
                Directory.Delete(tempRoot, true);
        }
    }
}
