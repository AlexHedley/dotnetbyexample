// **Nocco** is a quick-and-dirty, literate-programming-style documentation
// generator. It is a C# port of [Docco](http://jashkenas.github.com/docco/),
// which was written by [Jeremy Ashkenas](https://github.com/jashkenas) in
// Coffescript and runs on node.js.
//
// Nocco produces HTML that displays your comments alongside your code.
// Comments are passed through
// [Markdown](http://daringfireball.net/projects/markdown/syntax), and code is
// highlighted using [google-code-prettify](http://code.google.com/p/google-code-prettify/)
// syntax highlighting. This page is the result of running Nocco against its
// own source files.
//
// Currently, to build Nocco, you'll have to have Visual Studio 2010. The project
// depends on [MarkdownSharp](http://code.google.com/p/markdownsharp/) and you'll
// have to install [.NET MVC 3](http://www.asp.net/mvc/mvc3) to get the
// System.Web.Razor assembly. The MarkdownSharp is a NuGet package that will be
// installed automatically when you build the project.
//
// To use Nocco, run it from the command-line:
//
//     nocco *.cs
//
// ...will generate linked HTML documentation for the named source files, saving
// it into a `docs` folder.
//
// The [source for Nocco](http://github.com/dontangg/nocco) is available on GitHub,
// and released under the MIT license.
//
// If **.NET** doesn't run on your platform, or you'd prefer a more convenient
// package, get [Rocco](http://rtomayko.github.com/rocco/), the Ruby port that's
// available as a gem. If you're writing shell scripts, try
// [Shocco](http://rtomayko.github.com/shocco/), a port for the **POSIX shell**.
// Both are by [Ryan Tomayko](http://github.com/rtomayko). If Python's more
// your speed, take a look at [Nick Fitzgerald](http://github.com/fitzgen)'s
// [Pycco](http://fitzgen.github.com/pycco/).

// Import namespaces to allow us to type shorter type names.

using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using dotnetbyexample.Resources;
using Index = dotnetbyexample.Resources.Index;
using dotnetbyexample;

namespace Nocco;

public class Nocco
{
    private static string _executingDirectory = String.Empty;
    private static Dictionary<string, List<string>> _examples = new();

    //### Main Documentation Generation Functions

    // Generate the documentation for a source file by reading it in, splitting it
    // up into comment/code sections, highlighting them for the appropriate language,
    // and merging them into an HTML template.
    private static void GenerateDocumentation(KeyValuePair<string, List<string>> example)
    {
        Dictionary<string, List<Section>> files = new();
        
        foreach (var source in example.Value)
        {
            Console.WriteLine($"Generating documentation for {source}");
            var lines = File.ReadAllLines(source);
            var sections = Parse(source, lines);
            Highlight(sections);
            files.Add(source, sections);
        }

        GenerateHtml(example.Key, files);
    }

    // Given a string of source code, parse out each comment and the code that
    // follows it, and create an individual `Section` for it.
    private static List<Section> Parse(string source, string[] lines) {
        var sections = new List<Section>();
        var language = GetLanguage(source);
        var hasCode = false;
        var docsText = new StringBuilder();
        var codeText = new StringBuilder();

        Action<string, string> save = (docs, code) => sections.Add(new Section { DocsHtml = docs, CodeHtml = code });
        Func<string, string> mapToMarkdown = docs => {
            if (language.MarkdownMaps != null)
                docs = language.MarkdownMaps.Aggregate(docs, (currentDocs, map) => Regex.Replace(currentDocs, map.Key, map.Value, RegexOptions.Multiline));
            return docs;
        };

        foreach (var line in lines) {
            if (language.CommentMatcher.IsMatch(line) && !language.CommentFilter.IsMatch(line)) {
                if (hasCode) {
                    save(mapToMarkdown(docsText.ToString()), codeText.ToString());
                    hasCode = false;
                    docsText = new StringBuilder();
                    codeText = new StringBuilder();
                }
                docsText.AppendLine(language.CommentMatcher.Replace(line, ""));
            }
            else {
                hasCode = true;
                codeText.AppendLine(line);
            }
        }
        save(mapToMarkdown(docsText.ToString()), codeText.ToString());

        return sections;
    }

    
    // Prepares a single chunk of code for HTML output and runs the text of its
    // corresponding comment through **Markdown**, using a C# implementation
    // called [MarkdownSharp](http://code.google.com/p/markdownsharp/).
    private static void Highlight(List<Section> sections) {
        var markdown = new MarkdownSharp.Markdown();

        foreach (var section in sections) {
            section.DocsHtml = markdown.Transform(section.DocsHtml);
            section.CodeHtml = section.CodeHtml;
        }
    }
    
    // Once all of the code is finished highlighting, we can generate the HTML file
    // and write out the documentation. Pass the completed sections into the template
    // found in `Resources/Webpage.cshtml`
    private static async void GenerateHtml(string source, Dictionary<string, List<Section>> files)
    {
        string destination = new DirectoryInfo(source).Name;
        destination = Path.Combine(Constants.SITE_FOLDER, destination + ".html").ToLower();

        string pathToRoot = "";

        IServiceCollection services = new ServiceCollection();
        services.AddLogging();
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        await using var htmlRenderer = new HtmlRenderer(serviceProvider, loggerFactory);

        // Configure Data
        // Get the .bat file seperately
        var runner = files.Where(f => @Path.GetExtension(f.Key) == ".bat").Select(f => f.Value).FirstOrDefault();
        // Get the rest of the files
        files = files.Where(f => @Path.GetExtension(f.Key) != ".bat").ToDictionary();

        var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            Func<string, string> getSourcePath = s =>
                Path.Combine(pathToRoot, Path.ChangeExtension(s.ToLower(), ".html").Substring(2)).Replace('\\', '/');
            var dictionary = new Dictionary<string, object?>
            {
                { "Title", Path.GetFileName(source) },
                { "PathToCss", Path.Combine(pathToRoot, "nocco.css").Replace('\\', '/') },
                { "PathToJs", Path.Combine(pathToRoot, "prettify.js").Replace('\\', '/') },
                { "GetSourcePath", getSourcePath },
                { "Files", files },
                { "Runner", runner },
            };

            var parameters = ParameterView.FromDictionary(dictionary);
            var output = await htmlRenderer.RenderComponentAsync<Webpage>(parameters);

            return output.ToHtmlString();
        });
        
        File.WriteAllText(destination, html);
    }

    // Build an index.html file that links to all of the generated HTML files.
    private static async void GenerateIndex()
    {
        _executingDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        File.Copy(Path.Combine(_executingDirectory, "Resources", "favicon.ico"), Path.Combine(Constants.SITE_FOLDER, "favicon.ico"), true);
        File.Copy(Path.Combine(_executingDirectory, "Resources", "reset.css"), Path.Combine(Constants.SITE_FOLDER, "reset.css"), true);
        File.Copy(Path.Combine(_executingDirectory, "Resources", "style.css"), Path.Combine(Constants.SITE_FOLDER, "style.css"), true);

        // Just get the the folder name of the examples (should this be more readable?)
        var sources = _examples.Keys.Select(e => new DirectoryInfo(e).Name).ToList();

        IServiceCollection services = new ServiceCollection();
        services.AddLogging();
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        await using var htmlRenderer = new HtmlRenderer(serviceProvider, loggerFactory);

        var indexHtml = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            var dictionary = new Dictionary<string, object?>
            {
                { "Title", ".NET by Example" },
                { "Sources", sources },
            };

            var parameters = ParameterView.FromDictionary(dictionary);
            var output = await htmlRenderer.RenderComponentAsync<Index>(parameters);

            return output.ToHtmlString();
        });

        var destination = Path.Combine(Constants.SITE_FOLDER, "index.html");
        File.WriteAllText(destination, indexHtml);
    }

    // A list of the languages that Nocco supports, mapping the file extension to
    // the symbol that indicates a comment. To add another language to Nocco's
    // repertoire, add it here.
    //
    // You can also specify a list of regular expression patterns and replacements. This
    // translates things like
    // [XML documentation comments](http://msdn.microsoft.com/en-us/library/b2s063f7.aspx) into Markdown.
    private static Dictionary<string, Language> Languages = new Dictionary<string, Language> {
        { ".sql", new Language {
            Name = "sql",
            Symbol = "--",
        }},
        { ".js", new Language {
            Name = "javascript",
            Symbol = "//",
            Ignores = new List<string> {
                "min.js"
            }
        }},
        { ".cs", new Language {
            Name = "csharp",
            Symbol = "///?",
            Ignores = new List<string> {
                "Designer.cs"
            },
            MarkdownMaps = new Dictionary<string, string> {
                { @"<c>([^<]*)</c>", "`$1`" },
                { @"<param[^\>]*name=""([^""]*)""[^\>]*>([^<]*)</param>", "**argument** *$1*: $2" + Environment.NewLine },
                { @"<returns>([^<]*)</returns>", "**returns**: $1" + Environment.NewLine },
                { @"<see\s*cref=""([^""]*)""\s*/>", "see `$1`"},
                { @"(</?example>|</?summary>|</?remarks>)", "" },
            }
        }},
        { ".csx", new Language {
            Name = "csharp",
            Symbol = "///?",
            Ignores = new List<string> {
                "Designer.cs"
            },
            MarkdownMaps = new Dictionary<string, string> {
                { @"<c>([^<]*)</c>", "`$1`" },
                { @"<param[^\>]*name=""([^""]*)""[^\>]*>([^<]*)</param>", "**argument** *$1*: $2" + Environment.NewLine },
                { @"<returns>([^<]*)</returns>", "**returns**: $1" + Environment.NewLine },
                { @"<see\s*cref=""([^""]*)""\s*/>", "see `$1`"},
                { @"(</?example>|</?summary>|</?remarks>)", "" },
            }
        }},
        { ".vb", new Language {
            Name = "vb.net",
            Symbol = "'+",
            Ignores = new List<string> {
                "Designer.vb"
            },
            MarkdownMaps = new Dictionary<string, string> {
                { @"<c>([^<]*)</c>", "`$1`" },
                { @"<param[^\>]*>([^<]*)</param>", "" },
                { @"<returns>([^<]*)</returns>", "" },
                { @"<see\s*cref=""([^""]*)""\s*/>", "see `$1`"},
                { @"(</?example>|</?summary>|</?remarks>)", "" },
            }
        }},
        //{ ".fs", new Language {
        //    Name = "fsharp",
        //    Symbol = "//",
        //    Ignores = new List<string> {
        //        "AssemblyInfo.fs"
        //    },
        //    MarkdownMaps = new Dictionary<string, string> {
        //        { @"<c>([^<]*)</c>", "`$1`" },
        //        { @"<param[^\>]*name=""([^""]*)""[^\>]*>([^<]*)</param>", "**argument** *$1*: $2" + Environment.NewLine },
        //        { @"<returns>([^<]*)</returns>", "**returns**: $1" + Environment.NewLine },
        //        { @"<see\s*cref=""([^""]*)""\s*/>", "see `$1`"},
        //        { @"(</?example>|</?summary>|</?remarks>)", "" },
        //    }
        //}},
        { ".fsx", new Language {
            Name = "fsharp",
            Symbol = "///?", //(* *)
            Ignores = new List<string> {
                "AssemblyInfo.fs"
            },
            MarkdownMaps = new Dictionary<string, string> {
                { @"<c>([^<]*)</c>", "`$1`" },
                { @"<param[^\>]*name=""([^""]*)""[^\>]*>([^<]*)</param>", "**argument** *$1*: $2" + Environment.NewLine },
                { @"<returns>([^<]*)</returns>", "**returns**: $1" + Environment.NewLine },
                { @"<see\s*cref=""([^""]*)""\s*/>", "see `$1`"},
                { @"(</?example>|</?summary>|</?remarks>)", "" },
            }
        }},
        { ".bat", new Language {
            Name = "batch",
            Symbol = ":+",
        }},
    };
    
    // Get the current language we're documenting, based on the extension.
    private static Language GetLanguage(string source) {
        var extension = Path.GetExtension(source);
        return Languages.ContainsKey(extension) ? Languages[extension] : null;
    }
    
    // Find all the files that match the pattern(s) passed in as arguments and
    // generate documentation for each one.
    public static void Generate()
    {
        string[] targets = new [] { "*.cs", "*.vb", "*.csx", "*.fsx", "*.bat" };

        if (targets.Length > 0)
        {
            Directory.CreateDirectory(Constants.SITE_FOLDER);

            _executingDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            File.Copy(Path.Combine(_executingDirectory, "Resources", "nocco.css"), Path.Combine(Constants.SITE_FOLDER, "nocco.css"), true);
            File.Copy(Path.Combine(_executingDirectory, "Resources", "nocco.js"), Path.Combine(Constants.SITE_FOLDER, "nocco.js"), true);
            File.Copy(Path.Combine(_executingDirectory, "Resources", "prettify.js"), Path.Combine(Constants.SITE_FOLDER, "prettify.js"), true);

            var directories = Directory.GetDirectories(@"examples", "*", SearchOption.TopDirectoryOnly);
            Console.WriteLine($"{directories.Length} directories found.");

            foreach (var directory in directories)
            {
                List<string> _files = new();

                foreach (var target in targets)
                {
                    _files.AddRange(Directory.GetFiles(directory, target, SearchOption.TopDirectoryOnly).Where(filename =>
                    {
                        var language = GetLanguage(Path.GetFileName(filename));

                        if (language == null)
                            return false;

                        // Check if the file extension should be ignored
                        if (language.Ignores != null && language.Ignores.Any(ignore => filename.EndsWith(ignore)))
                            return false;

                        // Don't include certain directories
                        var foldersToExclude = new string[] { @"\docs", @"\bin", @"\obj" };
                        if (foldersToExclude.Any(folder => Path.GetDirectoryName(filename).Contains(folder)))
                            return false;

                        return true;
                    }));
                }

                _examples.Add(directory, _files);
            }

            Console.WriteLine($"{_examples.Count} examples found.");

            foreach (var example in _examples)
            {
                Console.WriteLine($"Generating documentation for {example.Key}");
                GenerateDocumentation(example);
            }

            Console.WriteLine($"Generating home page with list of examples.");
            GenerateIndex();
        }
    }

}