namespace __MAIN.Source.Crafting {
  using System;
  using System.Threading.Tasks;
  using __MAIN.Source.Crafting.Recipes;

  public interface ICrafting {
    Task Execute(CraftingRecipe recipe, IProgress<float> progress);
  }
}
