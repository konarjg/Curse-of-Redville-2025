namespace __MAIN.Source.Models {
  using UnityEngine;

  [RequireComponent(typeof(Rigidbody))]
  public class MovementModel : MonoBehaviour {
    [SerializeField]
    private MovementStats _movementStats;
    [SerializeField]
    private Transform _body;

    public MovementStats MovementStats => _movementStats;
    public Transform Body => _body;
    public Rigidbody Rigidbody { get; private set; }
    public Vector2 Input { get; set; }
    public bool IsRunning { get; set; }
    
    private void Awake() {
      Rigidbody = GetComponent<Rigidbody>();
    }
  }
}
