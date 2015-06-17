using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pipelines
{
    internal class Examples
    {
        public void RunPipeline(int seed)
        {
            var readBuffer = new BlockingCollection<string>(BUFFER_SIZE);
            var correctCaseBuffer = new BlockingCollection<string>(BUFFER_SIZE);
            var createSentanceBuffer = new BlockingCollection<string>(BUFFER_SIZE);

            var taskFactory = new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.None);

            // Stage 1: Read strings and merge into sentences
            var readStrings = taskFactory.StartNew(() => ReadStrings(readBuffer, seed));

            // Stage 2: Correct case
            var correctCases = taskFactory.StartNew(() => CorrectCase(readBuffer, correctCaseBuffer));

            // Stage 3: Merge into sentences
            var createSentances = taskFactory.StartNew(() => CreateSentences(correctCaseBuffer, createSentanceBuffer));

            // Stage 4: Write output
            var stage4 = taskFactory.StartNew(() => WriteSentences(createSentanceBuffer));

            Task.WaitAll(readStrings, correctCases, createSentances, stage4);
            Console.WriteLine("End");
        }

        private static void CorrectCase(BlockingCollection<string> input, BlockingCollection<string> output)
        {
            try
            {
                var isFirstPhrase = true;
                foreach (var phrase in input.GetConsumingEnumerable())
                {
                    if (isFirstPhrase)
                    {
                        var capitalized = phrase.Substring(0, 1).ToUpper() + phrase.Substring(1);
                        isFirstPhrase = false;
                        output.Add(capitalized);
                    }
                    else
                    {
                        output.Add(phrase);
                        if (phrase == ".")
                        {
                            isFirstPhrase = true;
                        }
                    }
                }
            }
            finally
            {
                output.CompleteAdding();
            }
        }

        private static void CreateSentences(BlockingCollection<string> input, BlockingCollection<string> output)
        {
            try
            {
                var sentenceBuilder = new StringBuilder();
                var isFirstPhrase = true;
                foreach (var phrase in input.GetConsumingEnumerable())
                {
                    if (!isFirstPhrase && phrase != ".")
                    {
                        sentenceBuilder.Append(" ");
                    }

                    sentenceBuilder.Append(phrase);
                    isFirstPhrase = false;

                    if (phrase == ".")
                    {
                        var sentence = sentenceBuilder.ToString();
                        sentenceBuilder.Clear();
                        output.Add(sentence);
                        isFirstPhrase = true;
                    }
                }
            }
            finally
            {
                output.CompleteAdding();
            }
        }

        private static IEnumerable<string> PhraseSource(int seed)
        {
            var random = new Random(seed);
            for (var i = 0; i < NUMBER_OF_SENTENCES; i++)
            {
                foreach (var line in _phrases)
                {
                    if (line == "<Adjective>")
                    {
                        yield return _adjectives[random.Next(0, _adjectives.Length)];
                    }
                    else if (line == "<Noun>")
                    {
                        yield return _nouns[random.Next(0, _nouns.Length)];
                    }
                    else
                    {
                        yield return line;
                    }
                }
            }
        }

        private static void ReadStrings(BlockingCollection<string> output, int seed)
        {
            try
            {
                foreach (var phrase in PhraseSource(seed))
                {
                    output.Add(phrase);
                }
            }
            finally
            {
                output.CompleteAdding();
            }
        }

        private static void WriteSentences(BlockingCollection<string> input)
        {
            foreach (var sentence in input.GetConsumingEnumerable())
            {
                Console.WriteLine(sentence);
            }
        }

        private const int NUMBER_OF_SENTENCES = 1000;

        private const int BUFFER_SIZE = 32;

        private static readonly string[] _phrases =
            {
                "the", "<Adjective>", "<Adjective>", "<Noun>", "jumped over the",
                "<Adjective>", "<Noun>", "."
            };

        private static readonly string[] _adjectives = { "quick", "brown", "lazy" };

        private static readonly string[] _nouns = { "fox", "dog" };
    }
}