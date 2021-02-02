using UnityEngine;

public class BallController : MonoBehaviour {
    private float _x;
    private float _y;
    private float _z;

    // Start is called before the first frame update
    private void Start() {
        var position = transform.position;
        _x = position.x;
        _y = position.y;
        _z = position.z;
    }

    public void Reset() {
        transform.position = new Vector3(_x, _y, _z);
    }
}