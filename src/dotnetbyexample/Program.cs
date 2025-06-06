// The entrance point for the program.  Just run Nocco!

using dotnetbyexample;

namespace Nocco
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("  ___         ___         ___        ___        ___   ");
            Console.WriteLine("/' _ `\\      / __`\\      /'___\\     /'___\\     / __`\\ ");
            Console.WriteLine("/\\ \\/\\ \\    /\\ \\_\\ \\    /\\ \\__/    /\\ \\__/    /\\ \\_\\ \\");
            Console.WriteLine("\\ \\_\\ \\_\\   \\ \\____/    \\ \\____\\   \\ \\____\\   \\ \\____/");
            Console.WriteLine("\\/_/\\/_/    \\/___/      \\/____/    \\/____/    \\/___/ ");
            Console.WriteLine("");
            
            Nocco.Generate();

            Console.WriteLine();
            Console.WriteLine($"Open {Constants.SITE_FOLDER}/ to see generated output.");
            Console.WriteLine();
        }
    }
}
