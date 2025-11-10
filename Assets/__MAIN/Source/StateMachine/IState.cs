namespace __MAIN.Source.StateMachine {

  public interface IState<in TContext> {
    void Enter(TContext context);
    void Tick(TContext context);
    void Exit(TContext context);
  }
}
