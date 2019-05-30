# Stateman

[![Build Status](https://dev.azure.com/ReiFujimura/Stateman/_apis/build/status/rf8409.Stateman?branchName=master)](https://dev.azure.com/ReiFujimura/Stateman/_build/latest?definitionId=2&branchName=master)

Stateman is simple state machine library for .NET languages.

## License

MIT License

## Usage

```csharp
var stateMachine = new StateMachine(new FooState());
stateMachine.Transited += sender => {
    // Raised when state changed.
}

// Transition from FooState to BarState
stateMachine.Transit<FooState, BarState>();

// Transition to previous state.
stateMachine.Previous();

// Transition to next state.
stateMachine.Next();
```
