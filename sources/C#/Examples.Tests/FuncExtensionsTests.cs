using System;
using System.Collections.Generic;
using System.Threading;

using FluentAssertions;

using NUnit.Framework;

namespace Examples.Tests
{
    [TestFixture]
    public class FuncExtensionsTests
    {
        [Test]
        public void MemoizedFunctionShouldOnlyBeCalledOnce()
        {
            // Arrange
            var callCount = 0;

            Func<int, int> getSameValue = i =>
                {
                    callCount++;
                    Thread.Sleep(50);
                    return i;
                };

            // Act
            var memoized = getSameValue.Memoize();

            var threads = new List<Thread>();
            for (var i = 0; i < 1000; i++)
            {
                var job = new ThreadStart(
                    () =>
                        {
                            var firstResult = memoized(1);
                            var secondResult = memoized(2);
                            firstResult.Should().Be(1);
                            secondResult.Should().Be(2);
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