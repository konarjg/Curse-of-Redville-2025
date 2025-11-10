namespace __MAIN.Source.StateMachine.States.Game {
  using Models;
  using UnityEngine;

  public class PausedState : IState<GameModel> {

    public void Enter(GameModel context) {
      context.CursorVisible = true;
      Time.timeScale = 0f;
    }

    public void Tick(GameModel context) {
      
    }
    
    public void Exit(GameModel context) {
      Time.timeScale = 1f;
    }
  }
}
