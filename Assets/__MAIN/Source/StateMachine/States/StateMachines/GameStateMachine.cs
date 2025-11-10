namespace __MAIN.Source.StateMachine.States.StateMachines {
  using System.Collections.Generic;
  using Models;

  public class GameStateMachine : StateMachineBase<GameModel> {

    public GameStateMachine(List<Transition<GameModel>> transitions,
      IState<GameModel> initialState) : base(transitions,initialState) {
    }
  }
}
