namespace __MAIN.Source.StateMachine.States.Game {
  using Models;

  public class CurseState : IState<GameModel> {

    public void Enter(GameModel context) {
      context.CursorVisible = false;
    }
    
    public void Tick(GameModel context) {
     
    }
    
    public void Exit(GameModel context) {
      
    }
  }
}
