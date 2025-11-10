namespace __MAIN.Source.StateMachine.States.StateMachines {
  using System.Collections.Generic;
  using __MAIN.Source.Models;
  using __MAIN.Source.StateMachine;

  public class PlayerStateMachine : StateMachineBase<PlayerModelFacade> {

    public PlayerStateMachine(List<Transition<PlayerModelFacade>> transitions,
      IState<PlayerModelFacade> initialState) : base(transitions,initialState) {
    }
  }
}
