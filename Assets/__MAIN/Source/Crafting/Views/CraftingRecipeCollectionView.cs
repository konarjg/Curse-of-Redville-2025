namespace __MAIN.Source.Crafting.Views {
  using System.Collections.Generic;
  using __MAIN.Source.Crafting.Presenters;
  using UnityEngine;

  public class CraftingRecipeCollectionView : MonoBehaviour {
    [SerializeField]
    private CraftingRecipeCollectionPresenter _presenter;

    [SerializeField]
    private GameObject _recipeUiPrefab;

    private List<RecipeListItemUI> _recipeUis = new();

    private void Start() {
      List<RecipeViewModel> recipeViewModels = _presenter.GetRecipeViewModels();
      foreach (RecipeViewModel recipeViewModel in recipeViewModels) {
        GameObject recipeUi = Instantiate(_recipeUiPrefab, transform);
        RecipeListItemUI recipeListItemUI = recipeUi.GetComponent<RecipeListItemUI>();
        recipeListItemUI.Setup(recipeViewModel);
        recipeListItemUI.OnRecipeSelected += OnRecipeSelected;
        _recipeUis.Add(recipeListItemUI);
      }
    }

    private void OnRecipeSelected(RecipeViewModel recipeViewModel) {
      _presenter.OnRecipeSelected(recipeViewModel);
    }
  }
}