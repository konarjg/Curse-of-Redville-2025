namespace __MAIN.Source.Inventory {
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
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

    public override string ToString() {
      StringBuilder builder = new();

      if (Categories != null) {
        builder.Append(string.Join(",", Categories.Select(c => c.Id).OrderBy(id => id)));
      }

      builder.Append(";");

      if (Rarities != null) {
        builder.Append(string.Join(",", Rarities.Select(r => r.Id).OrderBy(id => id)));
      }

      builder.Append(";");

      if (Stacks != null) {
        builder.Append(string.Join(",", Stacks.Select(s => $"{s.Item.Id}:{s.Quantity}").OrderBy(id => id)));
      }

      return builder.ToString();
    }
  }
}