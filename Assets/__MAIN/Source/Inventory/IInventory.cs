namespace __MAIN.Source.Inventory {
  using System;
  using System.Collections.Generic;
  using Events;
  using Items;

  public interface IInventory {
    IReadOnlyCollection<ItemStack> Entries { get; }
    int Capacity { get; } 
    bool IsFull { get; }
    
    ItemStack TryAdd(ItemStack stackToAdd);
    bool TryRemove(ItemData item, int quantity);
    bool Contains(ItemData item, int quantity);
    IReadOnlyCollection<ItemStack> GetItems(InventoryFilters filters);
  }
}
