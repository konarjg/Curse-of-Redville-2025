namespace __MAIN.Source.StateMachine.States.Conditions {
  public class NotCondition<TContext> : ICondition<TContext> {
    private readonly ICondition<TContext> _child;
    
    public NotCondition(ICondition<TContext> child) {
      _child = child;
    }
    
    public bool Evaluate(TContext context) {
      return !_child.Evaluate(context);
    }
  }
}
