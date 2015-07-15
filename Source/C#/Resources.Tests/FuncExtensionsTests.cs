using System;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using NUnit.Framework;

namespace Resources.Tests
{
    [TestFixture]
    public class FuncExtensionsTests
    {
        [Test]
        public void MemoizedFunctionShouldOnlyBeCalledOnce()
        {
            // Arrange
            var callCount = 0;

            Func<int, int> getValueSquared = i =>
                {
                    lock (_callCountLock)
                    {
                        callCount++;
                    }
                    Thread.Sleep(100);
                    var result = i * i;
                    return result;
                };

            var memoized = getValueSquared.Memoize();

            // Act
            Parallel.For(0, 5000, GetTwoSqured(memoized));

            // Assert
            callCount.Should().Be(1);
        }

        private static Action<int> GetTwoSqured(Func<int, int> memoized)
        {
            return delegate
                {
                    var secondResult = memoized(2);
                    secondResult.Should().Be(4);
                };
        }

        private readonly object _callCountLock = new object();
    }
}