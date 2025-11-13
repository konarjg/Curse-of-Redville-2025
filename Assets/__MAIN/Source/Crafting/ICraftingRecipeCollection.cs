namespace __MAIN.Source.Crafting {
  using System.Collections.Generic;
  using __MAIN.Source.Crafting.Recipes;

  public interface ICraftingRecipeCollection {
    IReadOnlyDictionary<string, CraftingRecipe> Items { get; }
    bool TryGet(string searchQuery, out CraftingRecipe recipe);
    bool TryUnlock(string recipeId);
  }
}
