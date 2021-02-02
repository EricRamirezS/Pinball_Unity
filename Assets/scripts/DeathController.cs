using UnityEngine;

public class DeathController : MonoBehaviour
{
    
    private int _ballCount = 2;
    [SerializeField] private BallController ball;
    
    private void OnTriggerEnter2D(Collider2D other) {

        if (!other.CompareTag("Player")) return;
    
        _ballCount--;
        
        {
            ball.Reset();
        }
        if (_ballCount < 0) return;
        
    }
}
