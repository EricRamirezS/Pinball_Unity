using UnityEngine;

public class BumperController : MonoBehaviour {
    private Animator _animator;

    private static readonly int Hit = Animator.StringToHash("Hit");

    void Awake() {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        _animator.SetTrigger(Hit);
    }
}
