using UnityEngine;

public class DeathController : MonoBehaviour {
    [SerializeField] private BallController ball;

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;

        if (GameManager.loseLife()) {
            ball.Reset();
        }
        else {
            GameManager.GameOver();
        }
    }
}