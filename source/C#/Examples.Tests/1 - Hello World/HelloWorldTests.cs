using System;

using FluentAssertions;

using NUnit.Framework;

namespace Examples.Tests
{
    [TestFixture]
    public class HelloWorldTests
    {
        [Test]
        public void RunTest()
        {
            var helloWorld = new HelloWorld();
            Action action = () => helloWorld.Run();
            action.ShouldNotThrow();
        }
    }
}