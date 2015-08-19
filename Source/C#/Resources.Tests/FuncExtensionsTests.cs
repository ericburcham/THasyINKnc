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
        public void MemoizedFunctionShouldOnlyBeCalledOnceFromParallelForLoop()
        {
            // Arrange
            var callCount = 0;

            Func<int, int> getValueSquared = i =>
                {
                    lock (this._callCountLock)
                    {
                        callCount++;
                    }

                    return i * i;
                };

            var memoized = getValueSquared.Memoize();

            // Act
            Parallel.For(0, 5000, AssertTwoSquaredIsFour(memoized));

            // Assert
            callCount.Should().Be(1);
        }

        [Test]
        public void SoftMemoizedFunctionShouldBeCalledFewerTimesThanCallingThreadCount()
        {
            // Arrange
            var callCount = 0;
            const int ThreadCount = 5000;

            Func<int, int> getValueSquared = i =>
            {
                lock (_callCountLock)
                {
                    callCount++;
                }
                return i * i;
            };

            // Act
            var memoized = getValueSquared.SoftMemoize();

            for (var i = 0; i < ThreadCount; i++)
            {
                var job = new ThreadStart(() =>
                {
                    var result = memoized(1);
                    result.Should().Be(1);
                });

                var thread = new Thread(job);
                thread.Start();
                thread.Join();
            }

            // Assert
            callCount.Should().BeLessThan(ThreadCount);
        }

        private static Action<int> AssertTwoSquaredIsFour(Func<int, int> func)
        {
            return delegate
                {
                    var result = func(2);
                    result.Should().Be(4);
                };
        }

        private readonly object _callCountLock = new object();
    }
}