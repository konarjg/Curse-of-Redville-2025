namespace __MAIN.Source.StateMachine.Factories {
  using System.Collections.Generic;
  using Models;
  using States;
  using States.Conditions;
  using States.Conditions.Player;
  using States.Player;
  using States.StateMachines;

  public static class PlayerStateMachineFactory {
    public static IStateMachine<PlayerModelFacade> CreatePlayerStateMachine() {
      ICondition<PlayerModelFacade> isMovingCondition = new IsMovingCondition<PlayerModelFacade>();
      ICondition<PlayerModelFacade> notMovingCondition = new NotCondition<PlayerModelFacade>(isMovingCondition);
      
      IState<PlayerModelFacade> idleState = new IdleState<PlayerModelFacade>();
      IState<PlayerModelFacade> movementState = CreatePlayerMovementStateMachine();

      List<Transition<PlayerModelFacade>> transitions = new() {
        new Transition<PlayerModelFacade>(idleState,movementState,isMovingCondition),
        new Transition<PlayerModelFacade>(movementState,idleState,notMovingCondition)
      };
      
      IStateMachine<PlayerModelFacade> playerStateMachine = new PlayerStateMachine(transitions, idleState);

      return playerStateMachine;
    }

    private static IStateMachine<PlayerModelFacade> CreatePlayerMovementStateMachine() {
      ICondition<PlayerModelFacade> isRunningCondition = new IsRunningCondition<PlayerModelFacade>();
      ICondition<PlayerModelFacade> notRunningCondition = new NotCondition<PlayerModelFacade>(isRunningCondition);
      
      IState<PlayerModelFacade> walkState = new WalkState<PlayerModelFacade>();
      IState<PlayerModelFacade> runState = new RunState<PlayerModelFacade>();
      
      List<Transition<PlayerModelFacade>> transitions = new() {
        new Transition<PlayerModelFacade>(walkState,runState,isRunningCondition),
        new Transition<PlayerModelFacade>(runState,walkState,notRunningCondition),
      };

      return new MovementStateMachine<PlayerModelFacade>(transitions,walkState);
    }
  }
}
