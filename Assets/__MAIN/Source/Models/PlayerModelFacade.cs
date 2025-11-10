namespace __MAIN.Source.Models {
  using System;
  using UnityEngine;

  [RequireComponent(typeof(MovementModel))]
  public class PlayerModelFacade : MonoBehaviour, IMovementProvider {
    public MovementModel MovementModel { get; private set; }

    private void Awake() {
      MovementModel = GetComponent<MovementModel>();
    }
  }
}
