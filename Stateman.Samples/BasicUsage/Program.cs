using System;
using BasicUsage.States;
using Stateman;

namespace BasicUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            var defaultState = new BasicState1()
            {
                Value = 10,
                UniqueValue = 11
            };

            // Create a state machine with an initial value of BasicState1.
            var stateMachine = new StateMachine(defaultState);
            stateMachine.Transited += sender => Console.WriteLine(sender.State.ToString());

            Console.WriteLine(defaultState);

            // Inherit the value of a writable public properties.
            stateMachine.Transit<BasicState1, BasicState2>();
            stateMachine.Transit<BasicState2, BasicState3>();

            // Does not transition if the current state is not TStateFrom.
            stateMachine.Transit<BasicState1, BasicState2>();
            stateMachine.Transit<BasicState2, BasicState1>();

            // Can transition in the same state.
            stateMachine.Transit<BasicState3, BasicState3>();
        }
    }
}
