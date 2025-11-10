namespace __MAIN.Source.StateMachine {
  using System.Collections.Generic;
  using System.Linq;

  public class StateMachineBase<TContext> : IStateMachine<TContext> {
    public List<Transition<TContext>> Transitions { get; private set; }
    public IState<TContext> CurrentState { get; private set; }
    public IState<TContext> InitialState { get; private set; }

    private List<Transition<TContext>> _priorityTransitions = new();
    private Dictionary<IState<TContext>,List<Transition<TContext>>> _transitionLookup = new();

    public StateMachineBase(List<Transition<TContext>> transitions, IState<TContext> initialState) {
      Transitions = transitions.ToList();
      InitialState = initialState;

      _priorityTransitions = transitions.Where(t => t.From == null).ToList();
      _transitionLookup = transitions.GroupBy(t => t.From)
                                     .ToDictionary(g => g.Key,
                                       g => g
                                         .ToList());
    }

    public virtual void Enter(TContext context) {
      if (CurrentState != null) {
        return;
      }
      
      SetCurrentState(InitialState,context);
    }
    
    public virtual void Tick(TContext context) {
      if (CurrentState == null) {
        return;
      }
      
      CurrentState.Tick(context);

      foreach (Transition<TContext> transition in _priorityTransitions) {
        if (transition.CanTransition(context)) {
          SetCurrentState(transition.To,context, true);
          return;
        }
      }

      if (!_transitionLookup.TryGetValue(CurrentState,out List<Transition<TContext>> transitions)) {
        return;
      }
      
      foreach (Transition<TContext> transition in transitions) {
        if (transition.CanTransition(context)) {
          SetCurrentState(transition.To,context,true);
          return;
        }
      }
    }

    public virtual void Exit(TContext context) {
      
    }

    private void SetCurrentState(IState<TContext> state, TContext context,
      bool exitPreviousState = false) {

      if (exitPreviousState) {
        CurrentState.Exit(context);
      }

      CurrentState = state;
      CurrentState.Enter(context);
    }
  }
}
