namespace __MAIN.Source.StateMachine.States.Conditions.Game {
  using Models;

  public class IsInterfaceVisibleCondition : ICondition<GameModel> {

    public bool Evaluate(GameModel context) {
      return context.IsInterfaceVisible;
    }
  }
}
