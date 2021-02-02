using UnityEngine;

public class ExitBlock : MonoBehaviour {
    private PolygonCollider2D _collider2D;

    private void Awake() {
        _collider2D = GetComponent<PolygonCollider2D>();
    }

    private void Update() {
        _collider2D.enabled = GameManager.CurrentPhase == Phase.PLAYING;
    }
}