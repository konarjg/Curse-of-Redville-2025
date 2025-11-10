namespace __MAIN.Source.StateMachine {
  using System.Collections.Generic;
  using System.Linq;
  using StateMachine;
  using UnityEditor;

  public class Transition<TContext> {
    public IState<TContext> From { get; private set; }
    public IState<TContext> To { get; private set; }
    private readonly ICondition<TContext> _condition;

    public Transition(IState<TContext> from, IState<TContext> to,
      ICondition<TContext> condition) {
      From = from;
      To = to;
      _condition = condition;
    }

    public bool CanTransition(TContext context) {
      return _condition.Evaluate(context);
    }
  }
}
