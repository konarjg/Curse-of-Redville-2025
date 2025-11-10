namespace __MAIN.Source.StateMachine.States.StateMachines {
  using System.Collections.Generic;
  using Models;

  public class CurseStateMachine : StateMachineBase<GameModel> {

    public CurseStateMachine(List<Transition<GameModel>> transitions,
      IState<GameModel> initialState) : base(transitions,initialState) {
    }
  }
}
