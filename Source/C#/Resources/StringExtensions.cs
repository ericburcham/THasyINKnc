using System;
using System.Linq;

namespace Resources
{
    public static class StringExtensions
    {
        public static int DamerauLevenshteinDistance(this string s, string t)
        {
            var sLength = s.Length;
            var tLength = t.Length;
            var matrix = new int[sLength + 1, tLength + 1];

            for (var i = 0; i <= sLength; i++)
            {
                matrix[i, 0] = i;
            }

            for (var i = 0; i <= tLength; i++)
            {
                matrix[0, i] = i;
            }

            for (var i = 1; i <= sLength; i++)
            {
                for (var j = 1; j <= tLength; j++)
                {
                    var cost = t[j - 1] == s[i - 1] ? 0 : 1;
                    var vals = new[] { matrix[i - 1, j] + 1, matrix[i, j - 1] + 1, matrix[i - 1, j - 1] + cost };

                    matrix[i, j] = vals.Min();

                    if (i > 1 && j > 1 && s[i - 1] == t[j - 2] && s[i - 2] == t[j - 1])
                    {
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + cost);
                    }
                }
            }

            return matrix[sLength, tLength];
        }

        public static int LevenshteinDistance(this string s, string t)
        {
            var sLength = s.Length;
            var tLength = t.Length;
            var matrix = new int[sLength + 1, tLength + 1];

            if (sLength == 0)
            {
                return tLength;
            }

            if (tLength == 0)
            {
                return sLength;
            }

            for (var i = 1; i <= sLength; i++)
            {
                for (var j = 1; j <= tLength; j++)
                {
                    var cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }

            return matrix[sLength, tLength];
        }
    }
}