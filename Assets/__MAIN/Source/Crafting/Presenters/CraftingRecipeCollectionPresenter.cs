namespace __MAIN.Source.Crafting.Presenters {
  using System.Collections.Generic;
  using System.Linq;
  using __MAIN.Source.Crafting.Recipes;
  using UnityEngine;

  public class CraftingRecipeCollectionPresenter : MonoBehaviour {
    [SerializeField]
    private ScriptableObject _craftingRecipeCollection;

    private ICraftingRecipeCollection CraftingRecipeCollection => (ICraftingRecipeCollection)_craftingRecipeCollection;

    public IReadOnlyList<CraftingRecipe> UnlockedRecipes { get; private set; }

    private void Awake() {
      UnlockedRecipes = CraftingRecipeCollection.Items.Values.ToList();
    }
  }
}
