using System;
using System.IO;

namespace StringComparisons
{
    static class Program
    {
        static void Main()
        {
            var directory = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(directory, "TestData.txt");
            var separator = new[] { Environment.NewLine };
            var lastNames = File.ReadAllText(filePath).Split(separator, StringSplitOptions.RemoveEmptyEntries);

            foreach (var lastName in lastNames)
            {
                Console.WriteLine(lastName);
            }
        }
    }
}
