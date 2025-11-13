namespace __MAIN.Source.Crafting.Presenters {
  using System;
  using __MAIN.Source.Crafting.Events;
  using __MAIN.Source.Crafting.Recipes;
  using UnityEngine;

  public class CraftingPresenter : MonoBehaviour {
    [SerializeField]
    private CraftingStartedEvent _craftingStartedEvent;

    [SerializeField]
    private CraftingProgressEvent _craftingProgressEvent;

    [SerializeField]
    private CraftingCompletedEvent _craftingCompletedEvent;

    public float CraftingProgress { get; private set; }
    public string TimeRemainingText { get; private set; }
    public bool IsCrafting { get; private set; }

    public event Action OnCraftingStateChanged;

    private float _timeToComplete;

    private void OnEnable() {
      _craftingStartedEvent.AddListener(OnCraftingStarted);
      _craftingProgressEvent.AddListener(OnCraftingProgress);
      _craftingCompletedEvent.AddListener(OnCraftingCompleted);
    }

    private void OnDisable() {
      _craftingStartedEvent.RemoveListener(OnCraftingStarted);
      _craftingProgressEvent.RemoveListener(OnCraftingProgress);
      _craftingCompletedEvent.RemoveListener(OnCraftingCompleted);
    }

    private void OnCraftingStarted(CraftingRecipe recipe) {
      _timeToComplete = recipe.TimeToComplete;
      IsCrafting = true;
      CraftingProgress = 0f;
      TimeRemainingText = TimeSpan.FromSeconds(_timeToComplete).ToString(@"m\:ss");
      OnCraftingStateChanged?.Invoke();
    }

    private void OnCraftingProgress(float progress) {
      CraftingProgress = progress;
      float timeRemaining = _timeToComplete * (1 - progress);
      TimeRemainingText = TimeSpan.FromSeconds(timeRemaining).ToString(@"m\:ss");
      OnCraftingStateChanged?.Invoke();
    }

    private void OnCraftingCompleted(CraftingRecipe recipe) {
      IsCrafting = false;
      CraftingProgress = 1f;
      TimeRemainingText = TimeSpan.FromSeconds(0).ToString(@"m\:ss");
      OnCraftingStateChanged?.Invoke();
    }
  }
}
