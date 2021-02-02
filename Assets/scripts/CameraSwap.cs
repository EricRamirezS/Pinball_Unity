using UnityEngine;

public class CameraSwap : MonoBehaviour {
    
    [SerializeField] private Camera prevCamera;
    [SerializeField] private Camera newCamera;

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        newCamera.enabled = true;
        prevCamera.enabled = false;
    }
    
}