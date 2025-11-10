namespace __MAIN.Source.StateMachine.States.Conditions {
  using System.Collections.Generic;
  using System.Linq;

  public class OrCondition<TContext> : ICondition<TContext> {

    private readonly List<ICondition<TContext>> _children;

    public OrCondition(params ICondition<TContext>[] children) {
      _children = new List<ICondition<TContext>>(children);
    }
    
    public bool Evaluate(TContext context) {
      return _children.Any(c => c.Evaluate(context));
    }
  }
}
