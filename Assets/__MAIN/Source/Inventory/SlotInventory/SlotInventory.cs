namespace __MAIN.Source.Inventory.SlotInventory {
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using __MAIN.Source.Events;
  using __MAIN.Source.Inventory.Items;
  using UnityEngine;
  using UnityEngine.InputSystem.Utilities;

  public class SlotInventory : MonoBehaviour, IInventory {

    [SerializeField] private VoidEvent _onStructureChanged;
    [SerializeField] private VoidEvent _onInventoryReordered;
    [SerializeField] private int _slotsCount;

    private ItemStack[] _slots;
    private readonly Dictionary<ItemData, int> _totalQuantitiesLookup = new();

    public IReadOnlyCollection<ItemStack> Entries => new ReadOnlyArray<ItemStack>(_slots);
    public int Capacity => _slotsCount;
    public bool IsFull => _slots.All(s => s != null && s.Quantity == s.Item.MaxStackSize);

    private void Awake() {
      _slots = new ItemStack[_slotsCount];
      _onStructureChanged?.Subscribe(RecalculateTotals);
      RecalculateTotals();
    }

    private void OnDestroy() {
      _onStructureChanged?.Unsubscribe(RecalculateTotals);
    }

    public ItemStack TryAdd(ItemStack stackToAdd) {
      ItemStack remainingAfterMerge = TryMergeWithExistingStacks(stackToAdd);
      
      if (remainingAfterMerge == null) {
        _onStructureChanged?.Invoke();
        return null;
      }
      
      ItemStack finalRemainder = TryFillEmptySlots(remainingAfterMerge);
      _onStructureChanged?.Invoke();
      return finalRemainder;
    }

    public bool TryRemove(ItemData item, int quantity) {
      if (!Contains(item, quantity)) {
        return false;
      }

      int quantityToRemove = quantity;

      for (int i = Capacity - 1; i >= 0; i--) {
        ItemStack slot = _slots[i];
        if (slot == null || slot.Item != item) {
          continue;
        }

        int amountToTakeFromSlot = Math.Min(quantityToRemove, slot.Quantity);
        int newQuantityInSlot = slot.Quantity - amountToTakeFromSlot;

        _slots[i] = newQuantityInSlot != 0 ? new ItemStack(slot.Item, newQuantityInSlot) : null;
        quantityToRemove -= amountToTakeFromSlot;
        
        if (quantityToRemove == 0) {
          break;
        }
      }
      
      _onStructureChanged?.Invoke();
      return true;
    }

    public bool Contains(ItemData item, int quantity) {
      if (_totalQuantitiesLookup.TryGetValue(item, out int total)) {
        return total >= quantity;
      }
      return false;
    }

    public IReadOnlyCollection<ItemStack> GetItems(InventoryFilters filters) {
      IEnumerable<ItemStack> query = _slots.Where(s => s != null);

      if (filters.Stacks != null && filters.Stacks.Count > 0) {
        HashSet<ItemData> quantityMatchLookup = new HashSet<ItemData>();
        foreach (var requirement in filters.Stacks) {
          if (_totalQuantitiesLookup.TryGetValue(requirement.Item, out int total) && total >= requirement.Quantity) {
            quantityMatchLookup.Add(requirement.Item);
          }
        }
        query = query.Where(s => quantityMatchLookup.Contains(s.Item));
      }

      if (filters.Categories != null && filters.Categories.Count > 0) {
        query = query.Where(s => s.Item.Categories.Any(filters.Categories.Contains));
      }

      if (filters.Rarities != null && filters.Rarities.Count > 0) {
        query = query.Where(s => filters.Rarities.Contains(s.Item.Rarity));
      }

      return query.ToList();
    }

    public bool TryGetStackAtIndex(int index, out ItemStack stack) {
      stack = null;
      if (index < 0 || index >= Capacity) {
        return false;
      }
      stack = _slots[index];
      return true;
    }

    public bool TryRemoveStackAtIndex(int index, out ItemStack removedStack) {
      removedStack = null;
      if (index < 0 || index >= Capacity || _slots[index] == null) {
        return false;
      }
      
      removedStack = _slots[index];
      _slots[index] = null;
      _onStructureChanged?.Invoke();
      return true;
    }

    public bool TrySwap(int indexA, int indexB) {
      if (indexA < 0 || indexA >= Capacity || indexB < 0 || indexB >= Capacity) {
        return false;
      }
      if (indexA == indexB) {
        return true;
      }
      (_slots[indexA], _slots[indexB]) = (_slots[indexB], _slots[indexA]);
      _onInventoryReordered?.Invoke();
      return true;
    }

    private ItemStack TryMergeWithExistingStacks(ItemStack stackToAdd) {
      ItemStack remainingStack = stackToAdd;
      for (int i = 0; i < Capacity; i++) {
        if (remainingStack == null) {
          break;
        }

        ItemStack slot = _slots[i];
        
        if (slot == null || slot.Item != remainingStack.Item) {
          continue;
        }

        int maxStackSize = slot.Item.MaxStackSize;
        
        if (slot.Quantity >= maxStackSize) {
          continue;
        }

        int quantityToTransfer = Math.Min(remainingStack.Quantity, maxStackSize - slot.Quantity);
        _slots[i] = new ItemStack(slot.Item, slot.Quantity + quantityToTransfer);

        int newRemainingQuantity = remainingStack.Quantity - quantityToTransfer;
        remainingStack = newRemainingQuantity > 0 ? new ItemStack(remainingStack.Item, newRemainingQuantity) : null;
      }
      return remainingStack;
    }

    private ItemStack TryFillEmptySlots(ItemStack stackToAdd) {
      ItemStack remainingStack = stackToAdd;
      for (int i = 0; i < Capacity; i++) {
        if (remainingStack == null) {
          break;
        }
        if (_slots[i] != null) {
          continue;
        }

        int maxStackSize = remainingStack.Item.MaxStackSize;
        int quantityToPlace = Math.Min(remainingStack.Quantity, maxStackSize);

        _slots[i] = new ItemStack(remainingStack.Item, quantityToPlace);
        int newRemainingQuantity = remainingStack.Quantity - quantityToPlace;
        remainingStack = newRemainingQuantity > 0 ? new ItemStack(remainingStack.Item, newRemainingQuantity) : null;
      }
      return remainingStack;
    }

    private void RecalculateTotals() {
      _totalQuantitiesLookup.Clear();
      foreach (var stack in _slots) {
        if (stack != null) {
          _totalQuantitiesLookup.TryGetValue(stack.Item, out int currentSum);
          _totalQuantitiesLookup[stack.Item] = currentSum + stack.Quantity;
        }
      }
    }
  }
}