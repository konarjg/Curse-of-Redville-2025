namespace __MAIN.Source.StateMachine.States.Player {
  using __MAIN.Source.Models;
  using UnityEngine;

  public abstract class MoveState<TContext> : IState<TContext> where TContext : IMovementProvider {

    protected abstract float GetSpeed(TContext context);
    
    public void Enter(TContext context) {
      
    }
    
    public void Tick(TContext context) {
      Vector3 direction = Vector3.ClampMagnitude(context.MovementModel.Body.forward * context.MovementModel.Input.y
                                                 + context.MovementModel.Body.right * context.MovementModel.Input.x, 1f);

      Vector3 move = direction * GetSpeed(context);
      context.MovementModel.Rigidbody.velocity = new Vector3(move.x, context.MovementModel.Rigidbody.velocity.y, move.z);
    }
    
    public void Exit(TContext context) {
      context.MovementModel.Rigidbody.velocity = new Vector3(0f,context.MovementModel.Rigidbody.velocity.y,0f);
    }
  }
}
