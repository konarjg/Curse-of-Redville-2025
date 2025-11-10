namespace __MAIN.Source.Models {
  using UnityEngine;

  [CreateAssetMenu(menuName = "Curse of Redville/Models/MovementModel")]
  public class MovementStats : ScriptableObject {
    [SerializeField]
    private float _walkSpeed;
    [SerializeField]
    private float _runSpeed;

    public float WalkSpeed => _walkSpeed;
    public float RunSpeed => _runSpeed;
  }
}
