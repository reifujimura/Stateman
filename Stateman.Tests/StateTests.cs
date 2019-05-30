using System;
using Stateman.Tests.TestStates;
using Xunit;

namespace Stateman.Tests
{
    public class StateTests
    {
        [Fact]
        public void GenerateTest()
        {
            var initialState = new TestState1()
            {
                Value = 10,
                TestValue = 10
            };
            var generatedState = initialState.Generate<TestState2>();
            Assert.Equal(initialState.Value, generatedState.Value);
            Assert.NotEqual(initialState.ReadOnlyValue, generatedState.ReadOnlyValue);
            Assert.NotEqual(initialState.GetPrivateValue(), generatedState.GetPrivateValue());
            Assert.Equal(generatedState.TestValue, 0);
        }
    }
}
