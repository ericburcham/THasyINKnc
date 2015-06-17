using System;
using System.Collections.Generic;
using System.Threading;

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
                    callCount++;
                    Thread.Sleep(1);
                    return i ^ 2;
                };

            // Act
            var memoized = getValueSquared.Memoize();

            var threads = new List<Thread>();
            for (var i = 0; i < 50; i++)
            {
                var job = new ThreadStart(
                    () =>
                        {
                            var firstResult = memoized(1);
                            var secondResult = memoized(2);
                            firstResult.Should().Be(1);
                            secondResult.Should().Be(4);
                        });

                threads.Add(new Thread(job));
            }

            threads.ForEach(thread => thread.Start());
            threads.ForEach(thread => thread.Join());

            // Assert
            callCount.Should().Be(2);
        }
    }
}