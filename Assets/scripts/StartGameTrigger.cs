using UnityEngine;

public class StartGameTrigger : MonoBehaviour {
    [SerializeField] private Phase phase;

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;

        GameManager.CurrentPhase = phase;
    }
}