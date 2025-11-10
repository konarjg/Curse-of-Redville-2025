namespace __MAIN.Source.Inventory.SlotInventory {
  using System.Collections.Generic;
  using System.Linq;
  using __MAIN.Source.Events;
  using __MAIN.Source.Inventory.Items;
  using UnityEngine;

  public class SlotInventoryPresenter : MonoBehaviour {
    [SerializeField] private GameEvent<IInventory> _onStructureChanged;
    [SerializeField] private GameEvent<IInventory> _onInventoryReordered;
    [SerializeField] private GameEvent<List<SlotDisplayInfo>> _onDisplayInfoChanged;

    private IInventory _inventory;
    private InventoryFilters _filters = new();
    private readonly HashSet<ItemData> _filteredItems = new();
    private readonly List<SlotDisplayInfo> _slotDisplayInfos = new();

    private void Awake() {
      _onStructureChanged.Subscribe(OnInventoryChanged);
      _onInventoryReordered.Subscribe(OnInventoryChanged);
    }

    private void OnDestroy() {
      _onStructureChanged.Unsubscribe(OnInventoryChanged);
      _onInventoryReordered.Unsubscribe(OnInventoryChanged);
    }

    public void SetFilters(InventoryFilters filters) {
      _filters = filters;
      UpdateFilteredItems();
      UpdateDisplayInfo();
    }

    private void OnInventoryChanged(IInventory inventory) {
      _inventory = inventory;
      UpdateFilteredItems();
      UpdateDisplayInfo();
    }

    private void UpdateFilteredItems() {
      _filteredItems.Clear();
      if (_inventory == null) return;

      IReadOnlyCollection<ItemStack> items = _inventory.GetItems(_filters);
      foreach (ItemStack item in items) {
        _filteredItems.Add(item.Item);
      }
    }
    
    private void UpdateDisplayInfo() {
      _slotDisplayInfos.Clear();
      if (_inventory == null) return;

      for (int i = 0; i < _inventory.Capacity; i++) {
        _inventory.TryGetStackAtIndex(i, out ItemStack stack);
        bool isVisible = stack != null && _filteredItems.Contains(stack.Item);
        _slotDisplayInfos.Add(new SlotDisplayInfo(stack, isVisible));
      }

      _onDisplayInfoChanged.Invoke(_slotDisplayInfos);
    }
  }
}