namespace __MAIN.Source.Inventory.SlotInventory.View {
  using TMPro;
  using UnityEngine;
  using UnityEngine.UI;

  public class SlotView : MonoBehaviour {
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _quantityText;

    public void UpdateView(SlotDisplayInfo displayInfo) {
      if (displayInfo.IsEmpty) {
        _icon.enabled = false;
        _quantityText.enabled = false;
        return;
      }

      _icon.enabled = true;
      _icon.sprite = displayInfo.Icon;
      _quantityText.enabled = displayInfo.Quantity > 1;
      _quantityText.text = displayInfo.Quantity.ToString();
    }
  }
}