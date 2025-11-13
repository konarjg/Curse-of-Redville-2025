namespace __MAIN.Source.Crafting.Recipes {
  using System.Collections.Generic;
  using __MAIN.Source.Inventory.Items;
  using UnityEngine;

  [CreateAssetMenu(fileName = "CraftingRecipe", menuName = "Crafting/CraftingRecipe")]
  public class CraftingRecipe : ScriptableObject {
    [SerializeField]
    private string _name;

    [SerializeField]
    private string _id;

    [SerializeField]
    private string _description;

    [SerializeField]
    private List<ItemStack> _ingredients;

    [SerializeField]
    private ItemStack _output;

    [SerializeField]
    private float _timeToComplete;

    public string Name => _name;
    public string Id => _id;
    public string Description => _description;
    public List<ItemStack> Ingredients => _ingredients;
    public ItemStack Output => _output;
    public float TimeToComplete => _timeToComplete;
  }
}
