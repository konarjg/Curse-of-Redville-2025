namespace __MAIN.Source.Models {
  using System;
  using UnityEngine;

  public enum CurseState {
    Curse,
    Grace
  }
  
  public class GameModel : MonoBehaviour {
    public bool IsInterfaceVisible { get; set; }
    public bool IsPaused { get; set; }
    public CurseState CurrentCurseState { get; set; }

    public bool CursorVisible {
      set {
        if (value) {
          Cursor.lockState = CursorLockMode.None;
          Cursor.visible = true;
          return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
      }
    }

    private void Awake() {
      IsInterfaceVisible = false;
      IsPaused = false;
      CurrentCurseState = CurseState.Grace;
      CursorVisible = false;
    }
  }
}
