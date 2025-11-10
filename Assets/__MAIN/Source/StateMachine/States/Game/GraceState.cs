namespace __MAIN.Source.StateMachine.States.Game {
  using Models;
  using UnityEngine;

  public class GraceState : IState<GameModel> {

    public void Enter(GameModel context) {
      context.CursorVisible = false;
    }
    
    public void Tick(GameModel context) {
     
    }
    
    public void Exit(GameModel context) {
      
    }
  }
}
