using UnityEngine;

public class SpringController : MonoBehaviour {
    private SpringJoint2D _spring;

    private const float A = -0.414f;
    private const float B = 6.586f;
    private const float Distance = B - A;

    private AudioSource _sfx;
    private bool _playSfx;

    [SerializeField] private Transform springTransform;

    private void Awake() {
        _spring = GetComponent<SpringJoint2D>();
        _sfx = GetComponent<AudioSource>();
    }

    private void Update() {
        // Scaling the image to fit space between bottom and top of the spring
        var currentDist = transform.localPosition.y - A;
        var springScale = currentDist / Distance;
        springTransform.localScale = new Vector3(1, springScale * 3.3f, 1);


        if (GameManager.CurrentPhase != Phase.THROW) return;

        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) {
            _spring.enabled = false;

            // retract the spring 
            var localPosition = transform.localPosition;
            var newY = Mathf.Clamp(localPosition.y - 0.03f, 0, 6.586f);
            localPosition = new Vector3(localPosition.x, newY, localPosition.z);
            transform.localPosition = localPosition;

            _playSfx = true;
        }
        else {
            _spring.enabled = true;

            if (!_playSfx) return;

            _sfx.Play();
            _playSfx = false;
        }
    }
}