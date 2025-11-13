namespace __MAIN.Source.Crafting {
  using System;
  using System.Threading.Tasks;
  using __MAIN.Source.Crafting.Events;
  using __MAIN.Source.Crafting.Recipes;
  using __MAIN.Source.Inventory.Items;
  using __MAIN.Source.Inventory.SlotInventory;
  using UnityEngine;

  public class Crafting : MonoBehaviour, ICrafting {
    [SerializeField]
    private SlotInventory _inventory;

    [SerializeField]
    private ScriptableObject _craftingRecipeCollection;

    [SerializeField]
    private CraftingStartedEvent _craftingStartedEvent;

    [SerializeField]
    private CraftingCompletedEvent _craftingCompletedEvent;

    private ICraftingRecipeCollection _craftingRecipeCollectionInterface;

    private void Awake() {
      _craftingRecipeCollectionInterface = _craftingRecipeCollection as ICraftingRecipeCollection;
    }

    public async Task Execute(CraftingRecipe recipe, IProgress<float> progress) {
      foreach (ItemStack ingredient in recipe.Ingredients) {
        if (!_inventory.Contains(ingredient)) {
          return;
        }
      }

      foreach (ItemStack ingredient in recipe.Ingredients) {
        _inventory.TryRemove(ingredient);
      }

      _craftingStartedEvent.Fire(recipe);

      float elapsedTime = 0f;
      while (elapsedTime < recipe.TimeToComplete) {
        await Task.Delay(100);
        elapsedTime += 0.1f;
        float progressValue = Mathf.Clamp01(elapsedTime / recipe.TimeToComplete);
        progress.Report(progressValue);
      }

      progress.Report(1f);

      _inventory.TryAdd(recipe.Output);
      _craftingCompletedEvent.Fire(recipe);
    }
  }
}
