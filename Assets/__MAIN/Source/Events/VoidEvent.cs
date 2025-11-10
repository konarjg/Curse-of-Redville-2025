namespace __MAIN.Source.Events {
  using System;
  using System.Collections.Generic;
  using UnityEngine;

  public struct VoidPayload {
    
  }

  [CreateAssetMenu(menuName = "Curse of Redville/Events/Void Event")]
  public class VoidEvent : GameEvent<VoidPayload> {
    private readonly Dictionary<Action,Action<VoidPayload>> _listenerWrappers = new();
    
    public new void Invoke() {
      base.Invoke(new VoidPayload());
    }
    
    public new void Subscribe(Action listener) {
      if (_listenerWrappers.TryGetValue(listener, out _)) {
        return;
      }
      
      Action<VoidPayload> wrapper = (payload) => listener();
      _listenerWrappers[listener] = wrapper;
      
      base.Subscribe(wrapper);
    }

    public new void Unsubscribe(Action listener) {
      if (!_listenerWrappers.TryGetValue(listener,out Action<VoidPayload> wrapper)) {
        return;
      }

      _listenerWrappers.Remove(listener);
      base.Unsubscribe(wrapper);
    }
  }
}
