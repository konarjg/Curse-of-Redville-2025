namespace __MAIN.Source.StateMachine.Factories {
  using System.Collections.Generic;
  using Models;
  using States.Conditions;
  using States.Conditions.Game;
  using States.Game;
  using States.StateMachines;
  using CurseState = States.Game.CurseState;

  public static class GameStateMachineFactory {
    public static IStateMachine<GameModel> CreateGameStateMachine() {
      ICondition<GameModel> isInterfaceVisibleCondition = new IsInterfaceVisibleCondition();
      ICondition<GameModel> isPausedCondition = new IsPausedCondition();
      ICondition<GameModel> notInterfaceVisibleCondition = new NotCondition<GameModel>(isInterfaceVisibleCondition);
      ICondition<GameModel> notPausedCondition = new NotCondition<GameModel>(isPausedCondition);

      IState<GameModel> unpausedState = CreateCurseStateMachine();
      IState<GameModel> interfaceVisibleState = new InterfaceVisibleState();
      IState<GameModel> pausedState = new PausedState();

      List<Transition<GameModel>> transitions = new() {
        new Transition<GameModel>(unpausedState,interfaceVisibleState,isInterfaceVisibleCondition),
        new Transition<GameModel>(unpausedState,pausedState,isPausedCondition),
        new Transition<GameModel>(interfaceVisibleState,unpausedState,notInterfaceVisibleCondition),
        new Transition<GameModel>(pausedState,unpausedState,notPausedCondition)
      };

      return new GameStateMachine(transitions, unpausedState);
    }

    private static IStateMachine<GameModel> CreateCurseStateMachine() {
      ICondition<GameModel> isCurseActiveCondition = new IsCurseActiveCondition();
      ICondition<GameModel> notCurseActiveCondition = new NotCondition<GameModel>(isCurseActiveCondition);

      IState<GameModel> graceState = new GraceState();
      IState<GameModel> curseState = new CurseState();

      List<Transition<GameModel>> transitions = new() {
        new Transition<GameModel>(graceState,curseState, isCurseActiveCondition),
        new Transition<GameModel>(curseState,graceState,notCurseActiveCondition)
      };

      return new CurseStateMachine(transitions,graceState);
    }
  }
}
