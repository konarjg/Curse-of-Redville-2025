namespace __MAIN.Source.Crafting {
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using System.Linq;
  using __MAIN.Source.Crafting.Recipes;
  using UnityEngine;

  [CreateAssetMenu(fileName = "CraftingRecipeCollection", menuName = "Curse of Redville/Crafting/Crafting Recipe Collection")]
  public class CraftingRecipeCollection : ScriptableObject, ICraftingRecipeCollection {
    [SerializeField]
    private List<CraftingRecipe> _recipes;

    private IReadOnlyDictionary<string, CraftingRecipe> _recipeDictionary;

    public IReadOnlyDictionary<string, CraftingRecipe> Items {
      get {
        if (_recipeDictionary == null) {
          _recipeDictionary = new ReadOnlyDictionary<string, CraftingRecipe>(
            _recipes.ToDictionary(recipe => recipe.Id, recipe => recipe)
          );
        }
        return _recipeDictionary;
      }
    }

    public bool TryGet(string searchQuery, out CraftingRecipe recipe) {
      return Items.TryGetValue(searchQuery, out recipe);
    }

    public bool TryUnlock(string recipeId) {
      return true;
    }
  }
}
