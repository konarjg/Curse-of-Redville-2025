namespace __MAIN.Source.Inventory.SlotInventory {
  using System.Collections.Generic;
  using __MAIN.Source.Events;
  using UnityEngine;

  public class SlotInventoryPresenter : MonoBehaviour {
    [SerializeField] private SlotInventory _inventory;
    [SerializeField] private VoidEvent _onStructureChanged;
    [SerializeField] private VoidEvent _onInventoryReordered;
    [SerializeField] private GameEvent<List<SlotDisplayInfo>> _onDisplayInfoChanged;

    private readonly List<SlotDisplayInfo> _slotDisplayInfos = new();

    private void Awake() {
      _onStructureChanged.Subscribe(UpdateDisplayInfo);
      _onInventoryReordered.Subscribe(UpdateDisplayInfo);
    }

    private void OnDestroy() {
      _onStructureChanged.Unsubscribe(UpdateDisplayInfo);
      _onInventoryReordered.Unsubscribe(UpdateDisplayInfo);
    }
    
    private void UpdateDisplayInfo() {
      _slotDisplayInfos.Clear();
      for (int i = 0; i < _inventory.Capacity; i++) {
        _inventory.TryGetStackAtIndex(i, out ItemStack stack);
        _slotDisplayInfos.Add(new SlotDisplayInfo(stack));
      }
      _onDisplayInfoChanged.Invoke(_slotDisplayInfos);
    }
  }
}