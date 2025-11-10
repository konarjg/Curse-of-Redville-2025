namespace __MAIN.Source.StateMachine.States.Conditions.Player {
  using __MAIN.Source.Models;
  using UnityEngine;

  public class IsMovingCondition<TContext> : ICondition<TContext> where TContext : IMovementProvider {

    public bool Evaluate(TContext context) {
      return context.MovementModel.Input != Vector2.zero;
    }
  }
}
