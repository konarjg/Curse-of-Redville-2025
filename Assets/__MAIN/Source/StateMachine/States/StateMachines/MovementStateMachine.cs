namespace __MAIN.Source.StateMachine.States.StateMachines {
  using System.Collections.Generic;
  using Models;

  public class MovementStateMachine<TContext> : StateMachineBase<TContext> where TContext : IMovementProvider {

    public MovementStateMachine(List<Transition<TContext>> transitions,
      IState<TContext> initialState) : base(transitions,initialState) {
    }
  }
}
