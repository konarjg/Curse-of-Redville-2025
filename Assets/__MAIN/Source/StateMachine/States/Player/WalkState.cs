namespace __MAIN.Source.StateMachine.States.Player {
  using __MAIN.Source.Models;

  public class WalkState<TContext> : MoveState<TContext> where TContext : IMovementProvider {

    protected override float GetSpeed(TContext context) {
      return context.MovementModel.MovementStats.WalkSpeed;
    }
  }
}
