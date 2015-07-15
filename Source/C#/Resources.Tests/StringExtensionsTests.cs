using FluentAssertions;

using NUnit.Framework;

namespace Resources.Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void IdenticalStringsHaveLevenshteinDistanceOfZero()
        {
            // Arrange
            const string A = "aStringAStringA";
            const string B = "aStringAStringA";

            // Act
            var distance = A.LevenshteinDistance(B);

            // Assert
            distance.Should().Be(0);
        }

        [Test]
        public void StringsWithDifferentFirstLetterShouldHaveLevenshteinDistanceOfOne()
        {
            // Arrange
            const string A = "aStringAStringA";
            const string B = "bStringAStringA";

            // Act
            var distance = A.LevenshteinDistance(B);

            // Assert
            distance.Should().Be(1);
        }

        [Test]
        public void StringsWithDifferentLastLetterShouldHaveLevenshteinDistanceOfOne()
        {
            // Arrange
            const string A = "aStringAStringA";
            const string B = "aStringAStringB";

            // Act
            var distance = A.LevenshteinDistance(B);

            // Assert
            distance.Should().Be(1);
        }

        [Test]
        public void StringsWithDifferntLetterInTheMiddleShouldHaveLevenshteinDistanceOfOne()
        {
            // Arrange
            const string A = "aStringAStringA";
            const string B = "aStringBStringA";

            // Act
            var distance = A.LevenshteinDistance(B);

            // Assert
            distance.Should().Be(1);
        }

        [Test]
        public void StringsWithTransposedLettersShouldHaveLevenshteinDistanceOfOne()
        {
            // Arrange
            const string A = "StringABString";
            const string B = "StringBAString";

            // Act
            var distance = A.LevenshteinDistance(B);

            // Assert
            distance.Should().Be(2);
        }

        [Test]
        public void StringsWithAdditionalCharacterAtTheEndShouldHaveLevenshteinDistanceOfOne()
        {
            // Arrange
            const string A = "aStringAStringA";
            const string B = "aStringAStringAA";

            // Act
            var distance = A.LevenshteinDistance(B);

            // Assert
            distance.Should().Be(1);
        }

        [Test]
        public void StringsWithVariousDifferenceShouldHaveCorrectLevenshteinDistance()
        {
            // Arrange
            const string A = "aStringAStringA";
            const string B = "bStringBStringB";

            // Act
            var distance = A.LevenshteinDistance(B);

            // Assert
            distance.Should().Be(3);
        }

        [Test]
        public void IdenticalStringsHaveDamerauLevenshteinDistanceOfZero()
        {
            // Arrange
            const string A = "aStringAStringA";
            const string B = "aStringAStringA";

            // Act
            var distance = A.DamerauLevenshteinDistance(B);

            // Assert
            distance.Should().Be(0);
        }

        [Test]
        public void StringsWithDifferentFirstLetterShouldHaveDamerauLevenshteinDistanceOfOne()
        {
            // Arrange
            const string A = "aStringAStringA";
            const string B = "bStringAStringA";

            // Act
            var distance = A.DamerauLevenshteinDistance(B);

            // Assert
            distance.Should().Be(1);
        }

        [Test]
        public void StringsWithDifferentLastLetterShouldHaveDamerauLevenshteinDistanceOfOne()
        {
            // Arrange
            const string A = "aStringAStringA";
            const string B = "aStringAStringB";

            // Act
            var distance = A.DamerauLevenshteinDistance(B);

            // Assert
            distance.Should().Be(1);
        }

        [Test]
        public void StringsWithDifferntLetterInTheMiddleShouldHaveDamerauLevenshteinDistanceOfOne()
        {
            // Arrange
            const string A = "aStringAStringA";
            const string B = "aStringBStringA";

            // Act
            var distance = A.DamerauLevenshteinDistance(B);

            // Assert
            distance.Should().Be(1);
        }

        [Test]
        public void StringsWithTransposedLettersShouldHaveDamerauLevenshteinDistanceOfOne()
        {
            // Arrange
            const string A = "StringABString";
            const string B = "StringBAString";

            // Act
            var distance = A.DamerauLevenshteinDistance(B);

            // Assert
            distance.Should().Be(1);
        }

        [Test]
        public void StringsWithAdditionalCharacterAtTheEndShouldHaveDamerauLevenshteinDistanceOfOne()
        {
            // Arrange
            const string A = "aStringAStringA";
            const string B = "aStringAStringAA";

            // Act
            var distance = A.DamerauLevenshteinDistance(B);

            // Assert
            distance.Should().Be(1);
        }

        [Test]
        public void StringsWithVariousDifferenceShouldHaveCorrectDamerauLevenshteinDistance()
        {
            // Arrange
            const string A = "aStringAStringA";
            const string B = "bStringBStringB";

            // Act
            var distance = A.DamerauLevenshteinDistance(B);

            // Assert
            distance.Should().Be(3);
        }
    }
}