namespace __MAIN.Source.Controllers {
  using System;
  using Models;
  using StateMachine;
  using StateMachine.Factories;
  using UnityEngine;

  [RequireComponent(typeof(PlayerModelFacade))]
  [RequireComponent(typeof(CameraFacingController))]
  [RequireComponent(typeof(MovementInputController))]
  public class PlayerController : MonoBehaviour {
    private PlayerModelFacade _context;
    private readonly IStateMachine<PlayerModelFacade> _stateMachine = PlayerStateMachineFactory.CreatePlayerStateMachine();

    private void Awake() {
      _context = GetComponent<PlayerModelFacade>();
      _stateMachine.Enter(_context);
    }

    private void FixedUpdate() {
      _stateMachine.Tick(_context);
    }
  }
}
