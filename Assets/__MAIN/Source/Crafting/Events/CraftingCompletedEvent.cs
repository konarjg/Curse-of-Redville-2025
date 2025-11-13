namespace __MAIN.Source.Crafting.Events {
  using __MAIN.Source.Crafting.Recipes;
  using __MAIN.Source.Events;
  using UnityEngine;

  [CreateAssetMenu(fileName = "CraftingCompletedEvent", menuName = "Curse of Redville/Crafting/Events/CraftingCompletedEvent")]
  public class CraftingCompletedEvent : GameEvent<CraftingRecipe> {
  }
}
