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
            var s = "aStringAStringA";
            var t = "aStringAStringA";

            // Act
            var distance = s.LevenshteinDistance(t);

            // Assert
            distance.Should().Be(0);
        }

        [Test]
        public void StringsWithDifferentFirstLetterShouldHaveLevenshteinDistanceOfOne()
        {
            // Arrange
            var s = "aStringAStringA";
            var t = "bStringAStringA";

            // Act
            var distance = s.LevenshteinDistance(t);

            // Assert
            distance.Should().Be(1);
        }

        [Test]
        public void StringsWithDifferentLastLetterShouldHaveLevenshteinDistanceOfOne()
        {
            // Arrange
            var s = "aStringAStringA";
            var t = "aStringAStringB";

            // Act
            var distance = s.LevenshteinDistance(t);

            // Assert
            distance.Should().Be(1);
        }

        [Test]
        public void StringsWithDifferntLetterInTheMiddleShouldHaveLevenshteinDistanceOfOne()
        {
            // Arrange
            var s = "aStringAStringA";
            var t = "aStringBStringA";

            // Act
            var distance = s.LevenshteinDistance(t);

            // Assert
            distance.Should().Be(1);
        }

        [Test]
        public void StringsWithTransposedLettersShouldHaveLevenshteinDistanceOfOne()
        {
            // Arrange
            var s = "StringABString";
            var t = "StringBAString";

            // Act
            var distance = s.LevenshteinDistance(t);

            // Assert
            distance.Should().Be(2);
        }

        [Test]
        public void StringsWithAdditionalCharacterAtTheEndShouldHaveLevenshteinDistanceOfOne()
        {
            // Arrange
            var s = "aStringAStringA";
            var t = "aStringAStringAA";

            // Act
            var distance = s.LevenshteinDistance(t);

            // Assert
            distance.Should().Be(1);
        }

        [Test]
        public void StringsWithVariousDifferenceShouldHaveCorrectLevenshteinDistance()
        {
            // Arrange
            var s = "aStringAStringA";
            var t = "bStringBStringB";

            // Act
            var distance = s.LevenshteinDistance(t);

            // Assert
            distance.Should().Be(3);
        }

        [Test]
        public void IdenticalStringsHaveDamerauLevenshteinDistanceOfZero()
        {
            // Arrange
            var s = "aStringAStringA";
            var t = "aStringAStringA";

            // Act
            var distance = s.DamerauLevenshteinDistance(t);

            // Assert
            distance.Should().Be(0);
        }

        [Test]
        public void StringsWithDifferentFirstLetterShouldHaveDamerauLevenshteinDistanceOfOne()
        {
            // Arrange
            var s = "aStringAStringA";
            var t = "bStringAStringA";

            // Act
            var distance = s.DamerauLevenshteinDistance(t);

            // Assert
            distance.Should().Be(1);
        }

        [Test]
        public void StringsWithDifferentLastLetterShouldHaveDamerauLevenshteinDistanceOfOne()
        {
            // Arrange
            var s = "aStringAStringA";
            var t = "aStringAStringB";

            // Act
            var distance = s.DamerauLevenshteinDistance(t);

            // Assert
            distance.Should().Be(1);
        }

        [Test]
        public void StringsWithDifferntLetterInTheMiddleShouldHaveDamerauLevenshteinDistanceOfOne()
        {
            // Arrange
            var s = "aStringAStringA";
            var t = "aStringBStringA";

            // Act
            var distance = s.DamerauLevenshteinDistance(t);

            // Assert
            distance.Should().Be(1);
        }

        [Test]
        public void StringsWithTransposedLettersShouldHaveDamerauLevenshteinDistanceOfOne()
        {
            // Arrange
            var s = "StringABString";
            var t = "StringBAString";

            // Act
            var distance = s.DamerauLevenshteinDistance(t);

            // Assert
            distance.Should().Be(1);
        }

        [Test]
        public void StringsWithAdditionalCharacterAtTheEndShouldHaveDamerauLevenshteinDistanceOfOne()
        {
            // Arrange
            var s = "aStringAStringA";
            var t = "aStringAStringAA";

            // Act
            var distance = s.DamerauLevenshteinDistance(t);

            // Assert
            distance.Should().Be(1);
        }

        [Test]
        public void StringsWithVariousDifferenceShouldHaveCorrectDamerauLevenshteinDistance()
        {
            // Arrange
            var s = "aStringAStringA";
            var t = "bStringBStringB";

            // Act
            var distance = s.DamerauLevenshteinDistance(t);

            // Assert
            distance.Should().Be(3);
        }
    }
}