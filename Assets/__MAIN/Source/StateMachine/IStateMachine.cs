namespace __MAIN.Source.StateMachine {
  using System.Collections.Generic;

  public interface IStateMachine<TContext> : IState<TContext> {
    List<Transition<TContext>> Transitions { get; }
    IState<TContext> CurrentState { get; }
    IState<TContext> InitialState { get; }
  }
}
