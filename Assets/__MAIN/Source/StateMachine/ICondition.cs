namespace __MAIN.Source.StateMachine {
  public interface ICondition<in TContext> {
    bool Evaluate(TContext context);
  }
}
