# Stateman

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
