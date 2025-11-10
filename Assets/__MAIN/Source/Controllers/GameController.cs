namespace __MAIN.Source.Controllers {
  using Models;
  using StateMachine;
  using StateMachine.Factories;
  using UnityEngine;

  [RequireComponent(typeof(GameModel))]
  public class GameController : MonoBehaviour {
    private GameModel _context;

    private IStateMachine<GameModel> _gameStateMachine = GameStateMachineFactory.CreateGameStateMachine();
    
    private void Awake() {
      _context = GetComponent<GameModel>();
      _gameStateMachine.Enter(_context);
    }

    private void Update() {
      _gameStateMachine.Tick(_context);
    }
  }
}
