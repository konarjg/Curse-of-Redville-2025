namespace __MAIN.Source.Inventory.Items {
  using System.Collections.Generic;
  using UnityEngine;

  [CreateAssetMenu(menuName = "Curse of Redville/Inventory/Item")]
  public class ItemData : ScriptableObject {
    [Header("Unique Identifier")]
    [ReadOnly]
    [SerializeField]
    private string _id;
    [Header("Core Information")]
    [SerializeField]
    private string _name;
    [TextArea]
    [SerializeField]
    private string _description;
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private ItemRarity _rarity = ItemRarity.Common;

    [Header("Gameplay")]
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    [Min(1)]
    private int _maxStackSize = 1; 
    
    [Header("Categorization & Functionality")]
    [SerializeField]
    private List<ItemCategory> _categories;
    [SerializeReference]
    [SubclassSelector]
    private List<IItemBehaviour> _behaviours = new();

    public string Id => _id;
    public string Name => _name;
    public string Description => _description;
    public ItemRarity Rarity => _rarity;
    public Sprite Icon => _icon;
    public int MaxStackSize => _maxStackSize;
    public GameObject Prefab => _prefab;
    public IReadOnlyList<ItemCategory> Categories => _categories;
    public IReadOnlyList<IItemBehaviour> Behaviours => _behaviours;
    
    public void GenerateId() {
      _id = System.Guid.NewGuid().ToString();
    }
  }
}
