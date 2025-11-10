namespace __MAIN.Source.StateMachine.States.Conditions.Game {
  using Models;

  public class IsCurseActiveCondition : ICondition<GameModel> {

    public bool Evaluate(GameModel context) {
      return context.CurrentCurseState == CurseState.Curse;
    }
  }
}
