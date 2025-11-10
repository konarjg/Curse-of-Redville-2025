namespace __MAIN.Source.StateMachine.States.Conditions.Player {
  using __MAIN.Source.Models;

  public class IsRunningCondition<TContext> : ICondition<TContext> where TContext : IMovementProvider {

    public bool Evaluate(TContext context) {
      return context.MovementModel.IsRunning;
    }
  }
}
