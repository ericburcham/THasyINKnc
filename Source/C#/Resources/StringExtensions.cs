using System;

namespace Resources
{
    public static class StringExtensions
    {
        public static int LevenshteinDistance(this string s, string t)
        {
            var sLength = s.Length;
            var tLength = t.Length;
            var distance = new int[sLength + 1, tLength + 1];

            // Step 1
            if (sLength == 0)
            {
                return tLength;
            }

            if (tLength == 0)
            {
                return sLength;
            }

            // Step 2
            for (var i = 0; i <= sLength; distance[i, 0] = i++)
            {
            }

            for (var j = 0; j <= tLength; distance[0, j] = j++)
            {
            }

            // Step 3
            for (var i = 1; i <= sLength; i++)
            {
                //Step 4
                for (var j = 1; j <= tLength; j++)
                {
                    // Step 5
                    var cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    distance[i, j] = Math.Min(
                        Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                        distance[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return distance[sLength, tLength];
        }
    }
}