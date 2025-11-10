namespace __MAIN.Source.Inventory {
  using System.Collections.Generic;
  using Items;
  using JetBrains.Annotations;

  public struct InventoryFilters {
    [CanBeNull]
    public IReadOnlyCollection<ItemCategory> Categories;
    [CanBeNull]
    public IReadOnlyCollection<ItemRarity> Rarities;
    [CanBeNull]
    public IReadOnlyCollection<ItemStack> Stacks;

    public InventoryFilters(IReadOnlyCollection<ItemCategory> categories = null, IReadOnlyCollection<ItemRarity> rarities = null, IReadOnlyCollection<ItemStack> stacks = null) {
      Categories = categories;
      Rarities = rarities;
      Stacks = stacks;
    }
  }
}
