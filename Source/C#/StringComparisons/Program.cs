using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using Resources;

namespace StringComparisons
{
    internal static class Program
    {
        private static void Main()
        {
            var sw = new Stopwatch();

            const string Target = "ALEX SMITH";

            var directory = Directory.GetCurrentDirectory();
            var separator = new[] { Environment.NewLine };

            sw.Start();
            Console.WriteLine("Reading test data");

            var lastNamesFilePath = Path.Combine(directory, "LastNames.txt");
            var lastNames =
                File.ReadAllText(lastNamesFilePath).Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();

            var maleNamesFilePath = Path.Combine(directory, "MaleFirstNames.txt");
            var maleNames =
                File.ReadAllText(maleNamesFilePath).Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();

            Console.WriteLine("\tDone.  Took: {0} milliseconds", sw.ElapsedMilliseconds);
            Console.WriteLine();

            sw.Restart();
            Console.WriteLine("Combining data files");

            var fullNames = new List<string>();

            maleNames.ForEach(
                firstName =>
                    {
                        lastNames.ForEach(
                            lastName =>
                                {
                                    fullNames.Add(firstName + " " + lastName);
                                });
                    });

            Console.WriteLine("\tDone.  Took: {0} milliseconds", sw.ElapsedMilliseconds);
            Console.WriteLine();

            sw.Restart();
            Console.WriteLine("Computing results");

            var results = fullNames
                .AsParallel()
                .Select(
                    fullName =>
                    new StringDistance
                        {
                            Distance = Target.DamerauLevenshteinDistance(fullName),
                            Target = Target,
                            Test = fullName
                        })
                .Where(result => result.Distance <= 2)
                .OrderByDescending(result => result.Distance)
                .ThenBy(result => result.Test)
                .ToList();


            Console.WriteLine("\tDone.  Took: {0} milliseconds", sw.ElapsedMilliseconds);
            Console.WriteLine();
            sw.Reset();

            Console.WriteLine("Displaying results:");
            results
                .ForEach(
                    result =>
                        {
                            Console.WriteLine(
                                "Distance from {0} to {1}: {2}",
                                result.Target,
                                result.Test,
                                result.Distance);
                        });
        }
    }

    internal class StringDistance
    {
        public string Target { get; set; }

        public string Test { get; set; }

        public int Distance { get; set; }
    }
}