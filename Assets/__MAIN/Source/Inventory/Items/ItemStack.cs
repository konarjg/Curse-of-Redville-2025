namespace __MAIN.Source.Inventory.Items {
  using System;
  using UnityEngine;

  [Serializable]
  public class ItemStack {
    [SerializeField]
    [ReadOnly]
    private ItemData _item;
    [SerializeField]
    [ReadOnly]
    private int _quantity;

    public ItemData Item => _item;
    public int Quantity => _quantity;

    public ItemStack(ItemData item,
      int quantity) {

      if (item == null) {
        throw new ArgumentNullException(nameof(item), "Item in a stack cannot be null.");
      }

      if (quantity <= 0 || quantity > item.MaxStackSize) {
        throw new ArgumentOutOfRangeException(nameof(quantity), $"Quantity must be between 1 and {item.MaxStackSize}.");
      }

      _item = item;
      _quantity = quantity;
    }
    
    public bool TrySetQuantity(int quantity) {
      if (quantity <= 0 || quantity > _item.MaxStackSize) {
        return false;
      }
      
      _quantity = quantity;
      return true; 
    }
  }
}
