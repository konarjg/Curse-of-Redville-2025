namespace __MAIN.Source.Controllers {
  using System;
  using Models;
  using UnityEngine;

  [RequireComponent(typeof(PlayerModelFacade))]
  public class MovementInputController : MonoBehaviour {
    
    private PlayerModelFacade _context;
    private PlayerInput _input;

    private void Awake() {
      _input = new PlayerInput();
      _context = GetComponent<PlayerModelFacade>();
    }
    
    private void OnEnable() {
      _input.Player.Move.Enable();
      _input.Player.Run.Enable();
    }

    private void OnDisable() {
      _input.Player.Move.Disable();
      _input.Player.Run.Disable();
    }

    private void Update() {
      ReadInput();
    }

    private void ReadInput() {
      _context.MovementModel.Input = _input.Player.Move.ReadValue<Vector2>();
      _context.MovementModel.IsRunning = _input.Player.Run.IsPressed();
    }
  }
}
