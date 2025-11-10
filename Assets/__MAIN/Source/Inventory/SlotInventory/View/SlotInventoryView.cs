namespace __MAIN.Source.Inventory.SlotInventory.View {
  using System.Collections.Generic;
  using __MAIN.Source.Events;
  using UnityEngine;

  public class SlotInventoryView : MonoBehaviour {
    [SerializeField] private List<SlotView> _slotViews;
    [SerializeField] private GameEvent<List<SlotDisplayInfo>> _onDisplayInfoChanged;

    private void Awake() {
      _onDisplayInfoChanged.Subscribe(OnDisplayInfoChanged);
    }

    private void OnDestroy() {
      _onDisplayInfoChanged.Unsubscribe(OnDisplayInfoChanged);
    }

    private void OnDisplayInfoChanged(List<SlotDisplayInfo> displayInfos) {
      for (int i = 0; i < _slotViews.Count; i++) {
        if(i < displayInfos.Count) {
          _slotViews[i].UpdateView(displayInfos[i]);
        }
      }
    }
  }
}