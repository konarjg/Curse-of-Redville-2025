namespace __MAIN.Source.Inventory.SlotInventory {
  using Items;

  public readonly struct SlotDisplayInfo {
    public readonly ItemStack Stack;
    public readonly bool IsVisible;

    public bool IsEmpty => Stack == null;

    public SlotDisplayInfo(ItemStack stack,
      bool isVisible) {
      Stack = stack;
      IsVisible = isVisible;
    }
  }
}