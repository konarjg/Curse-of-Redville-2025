namespace __MAIN.Source.Events {
  using System;
  using UnityEngine;
  
  public class GameEvent<TPayload> : ScriptableObject {
    private event Action<TPayload> _onEventInvoked;

    public void Invoke(TPayload payload) {
      _onEventInvoked?.Invoke(payload);
    }

    public void Subscribe(Action<TPayload> listener) {
      _onEventInvoked += listener;
    }

    public void Unsubscribe(Action<TPayload> listener) {
      _onEventInvoked -= listener;
    }
  }
}
