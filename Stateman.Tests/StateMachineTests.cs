using System;
using Stateman.Tests.TestStates;
using Xunit;

namespace Stateman.Tests
{
    public class StateMachineTests
    {
        [Fact]
        public void TransitTest()
        {
            var invokeCount = 0;
            var stateMachine = new StateMachine(new TestState1() { Value = 10 });
            stateMachine.Transited += sender =>
            {
                switch (sender.State)
                {
                    case TestStateBase testState:
                        Assert.Equal(10, testState.Value);
                        break;
                    case AnotherState anotherState:
                        Assert.Equal(0, anotherState.Value);
                        break;
                    case ConfigureState configureState:
                        Assert.Equal(100, configureState.Value);
                        break;
                }
                invokeCount++;
            };
            stateMachine.Transit<TestState1, TestState2>();
            Assert.Equal(typeof(TestState2), stateMachine.State.GetType());
            Assert.Equal(1, invokeCount);
            stateMachine.Transit<TestState1, TestState2>();
            Assert.Equal(typeof(TestState2), stateMachine.State.GetType());
            Assert.Equal(1, invokeCount);
            stateMachine.Transit<TestState2, TestState1>();
            Assert.Equal(typeof(TestState1), stateMachine.State.GetType());
            Assert.Equal(2, invokeCount);
            stateMachine.Transit<TestState1, AnotherState>();
            Assert.Equal(typeof(AnotherState), stateMachine.State.GetType());
            Assert.Equal(3, invokeCount);
            stateMachine.Transit<AnotherState, AnotherState>();
            Assert.Equal(typeof(AnotherState), stateMachine.State.GetType());
            Assert.Equal(4, invokeCount);
            stateMachine.Transit<AnotherState, ConfigureState>(state => state.Value = 100);
            Assert.Equal(5, invokeCount);
        }

        [Fact]
        public void PreviousTest()
        {
            var invokeCount = 0;
            var stateMachine = new StateMachine(new TestState1() { Value = 10 });
            stateMachine.Transited += sender => invokeCount++;
            stateMachine.Previous();
            Assert.Equal(typeof(TestState1), stateMachine.State.GetType());
            Assert.Equal(0, invokeCount);
            stateMachine.Transit<TestState1, TestState2>();
            stateMachine.Transit<TestState2, TestState1>();
            stateMachine.Previous();
            Assert.Equal(typeof(TestState2), stateMachine.State.GetType());
            Assert.Equal(3, invokeCount);
            stateMachine.Previous();
            Assert.Equal(typeof(TestState1), stateMachine.State.GetType());
            Assert.Equal(4, invokeCount);
            stateMachine.Previous();
            Assert.Equal(typeof(TestState1), stateMachine.State.GetType());
            Assert.Equal(4, invokeCount);
        }

        [Fact]
        public void NextTest()
        {
            var invokeCount = 0;
            var stateMachine = new StateMachine(new TestState1() { Value = 10 });
            stateMachine.Transited += sender => invokeCount++;
            stateMachine.Next();
            Assert.Equal(typeof(TestState1), stateMachine.State.GetType());
            Assert.Equal(0, invokeCount);
            stateMachine.Transit<TestState1, TestState2>();
            stateMachine.Transit<TestState2, TestState1>();
            stateMachine.Previous();
            stateMachine.Previous();
            stateMachine.Next();
            Assert.Equal(typeof(TestState2), stateMachine.State.GetType());
            Assert.Equal(5, invokeCount);
            stateMachine.Next();
            Assert.Equal(typeof(TestState1), stateMachine.State.GetType());
            Assert.Equal(6, invokeCount);
            stateMachine.Next();
            Assert.Equal(typeof(TestState1), stateMachine.State.GetType());
            Assert.Equal(6, invokeCount);
            stateMachine.Previous();
            stateMachine.Previous();
            stateMachine.Transit<TestState1, AnotherState>();
            stateMachine.Next();
            Assert.Equal(typeof(AnotherState), stateMachine.State.GetType());
            Assert.Equal(9, invokeCount);
        }

        [Fact]
        public void ClearHistoryTest()
        {
            var stateMachine = new StateMachine(new TestState1() { Value = 10 });
            stateMachine.Transit<TestState1, TestState2>();
            stateMachine.Transit<TestState2, TestState1>();
            stateMachine.Previous();
            stateMachine.ClearHistory();
            stateMachine.Previous();
            Assert.Equal(typeof(TestState2), stateMachine.State.GetType());
            stateMachine.Next();
            Assert.Equal(typeof(TestState2), stateMachine.State.GetType());
        }
    }
}
