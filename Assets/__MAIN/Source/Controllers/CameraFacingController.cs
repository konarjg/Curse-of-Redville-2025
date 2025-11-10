namespace __MAIN.Source.Controllers {
  using UnityEngine;
  
  public class CameraFacingController : MonoBehaviour {
    [SerializeField]
    private Transform _body;
    
    private Camera _camera;

    private void Awake() {
      _camera = Camera.main;
    }

    private void LateUpdate() {
      Vector3 cameraForward = _camera.transform.forward;
      cameraForward.y = 0; 
      
      if (cameraForward.sqrMagnitude > 0.01f) {
        _body.rotation = Quaternion.LookRotation(cameraForward);
      }
    }
  }
}
