namespace __MAIN.Source.Crafting.Events {
  using __MAIN.Source.Crafting.Recipes;
  using __MAIN.Source.Events;
  using UnityEngine;

  [CreateAssetMenu(fileName = "CraftingStartedEvent", menuName = "Curse of Redville/Crafting/Events/CraftingStartedEvent")]
  public class CraftingStartedEvent : GameEvent<CraftingRecipe> {
  }
}
