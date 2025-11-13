namespace __MAIN.Source.Crafting.Views {
  using __MAIN.Source.Crafting.Presenters;
  using __MAIN.Source.Inventory.Items;
  using TMPro;
  using UnityEngine;
  using UnityEngine.UI;

  public class CraftingView : MonoBehaviour {
    [SerializeField]
    private CraftingPresenter _presenter;

    [SerializeField]
    private Slider _progressBar;

    [SerializeField]
    private TextMeshProUGUI _timerText;

    [SerializeField]
    private Image _outputItemIcon;

    private void OnEnable() {
      _presenter.OnCraftingStateChanged += OnCraftingStateChanged;
    }

    private void OnDisable() {
      _presenter.OnCraftingStateChanged -= OnCraftingStateChanged;
    }

    private void OnCraftingStateChanged() {
      _progressBar.value = _presenter.CraftingProgress;
      _timerText.text = _presenter.CraftingTimeRemaining.ToString("F1");
      _outputItemIcon.sprite = _presenter.OutputItemIcon;
    }

    public void OnIngredientSlotDrop(ItemData item) {
      // Drag-and-drop logic will be implemented later.
    }
  }
}