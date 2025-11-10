namespace __MAIN.Source.Inventory.SlotInventory {
  using System.Collections.Generic;
  using __MAIN.Source.Events;
  using __MAIN.Source.Inventory.Items;
  using UnityEngine;

  public class SlotInventoryPresenter : MonoBehaviour {
    [SerializeField] private GameEvent<IInventory> _onStructureChanged;
    [SerializeField] private GameEvent<IInventory> _onInventoryReordered;
    [SerializeField] private GameEvent<List<SlotDisplayInfo>> _onDisplayInfoChanged;

    private IInventory _inventory;
    private InventoryFilters _filters = new();
    private readonly Dictionary<string, HashSet<ItemData>> _filteredItemsCache = new();
    private readonly List<SlotDisplayInfo> _slotDisplayInfos = new();

    private void Awake() {
      _onStructureChanged.Subscribe(OnInventoryChanged);
      _onInventoryReordered.Subscribe(OnInventoryReordered);
    }

    private void OnDestroy() {
      _onStructureChanged.Unsubscribe(OnInventoryChanged);
      _onInventoryReordered.Unsubscribe(OnInventoryReordered);
    }

    public void SetFilters(InventoryFilters filters) {
      _filters = filters;
      UpdateDisplayInfo();
    }

    private void OnInventoryChanged(IInventory inventory) {
      if (_inventory == null) {
        _inventory = inventory;
      }
      _filteredItemsCache.Clear();
      UpdateDisplayInfo();
    }

    private void OnInventoryReordered(IInventory inventory) {
      if (_inventory == null) {
        _inventory = inventory;
      }
      UpdateDisplayInfo();
    }

    private HashSet<ItemData> GetFilteredItems() {
      string filterKey = _filters.ToString();
      if (_filteredItemsCache.TryGetValue(filterKey, out HashSet<ItemData> cachedItems)) {
        return cachedItems;
      }

      HashSet<ItemData> filteredItems = new();
      if (_inventory == null) {
        return filteredItems;
      }

      IReadOnlyCollection<ItemStack> items = _inventory.GetItems(_filters);
      foreach (ItemStack item in items) {
        filteredItems.Add(item.Item);
      }

      _filteredItemsCache[filterKey] = filteredItems;
      return filteredItems;
    }
    
    private void UpdateDisplayInfo() {
      _slotDisplayInfos.Clear();
      if (_inventory == null) {
        return;
      }

      HashSet<ItemData> filteredItems = GetFilteredItems();

      for (int i = 0; i < _inventory.Capacity; i++) {
        _inventory.TryGetStackAtIndex(i, out ItemStack stack);
        bool isVisible = stack == null || filteredItems.Contains(stack.Item);
        _slotDisplayInfos.Add(new SlotDisplayInfo(stack, isVisible));
      }

      _onDisplayInfoChanged.Invoke(_slotDisplayInfos);
    }
  }
}