namespace __MAIN.Source.Inventory.Items {
  using UnityEngine;

  [CreateAssetMenu(menuName = "Curse of Redville/Inventory/Item Category")]
  public class ItemCategory : ScriptableObject {
    [TextArea]
    private string _description;
    
    public string Description => _description;
  }
}
