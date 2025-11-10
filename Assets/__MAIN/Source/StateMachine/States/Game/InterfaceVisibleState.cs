namespace __MAIN.Source.StateMachine.States.Game {
  using Models;
  using UnityEngine;

  public class InterfaceVisibleState : IState<GameModel> {

    public void Enter(GameModel context) {
      context.CursorVisible = true;
    }
    
    public void Tick(GameModel context) {
      
    }
    
    public void Exit(GameModel context) {
      
    }
  }
}
