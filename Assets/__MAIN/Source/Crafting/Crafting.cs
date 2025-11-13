namespace __MAIN.Source.Crafting {
  using System;
  using System.Threading.Tasks;
  using __MAIN.Source.Crafting.Recipes;
  using __MAIN.Source.Inventory.SlotInventory;
  using UnityEngine;

  public class Crafting : MonoBehaviour, ICrafting {
    [SerializeField]
    private SlotInventory _inventory;

    [SerializeField]
    private ScriptableObject _craftingRecipeCollection;

    private ICraftingRecipeCollection _craftingRecipeCollectionInterface;

    private void Awake() {
        _craftingRecipeCollectionInterface = _craftingRecipeCollection as ICraftingRecipeCollection;
    }

    public Task Execute(CraftingRecipe recipe, IProgress<float> progress) {
      return Task.CompletedTask;
    }
  }
}
