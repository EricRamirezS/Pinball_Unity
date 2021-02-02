using UnityEngine;
using static UnityEngine.Screen;

internal enum Orientation {
    LEFT,
    RIGHT
}

public class PaddleController : MonoBehaviour {
    private Orientation _orientation;
    private HingeJoint2D _hj;
    private JointMotor2D _motor;
    private float _x;
    private float _y;
    private const float MotorSpeed = 650;

    private float _upperLimit = 0.53f;

    private AudioSource _sfx;
    private bool _playSfx;

    private void Awake() {
        _x = transform.position.x;
        _y = transform.position.y;

        _sfx = GetComponent<AudioSource>();
        _hj = GetComponent<HingeJoint2D>();

        // Limits application framerate up to 60 fps
        Application.targetFrameRate = 60;

        var isFlipped = transform.localScale.x < 0;

        _orientation = isFlipped ? Orientation.RIGHT : Orientation.LEFT;
        _motor = _hj.motor;

        if (_orientation != Orientation.RIGHT) return;
        //Inverting settings for Right Paddle
        var jointAngleLimits2D = _hj.limits;
        jointAngleLimits2D.min = 60;
        _hj.limits = jointAngleLimits2D;
        _upperLimit *= -1;
    }

    // Update is called once per frame
    private void Update() {
        // I can't tell why, but the object position is slowly changing, after some minutes
        // it is clear the Paddle is not in its original position. This fix that problem
        transform.position = new Vector3(_x, _y, 0);

        bool isTouching;

        if (GameManager.CurrentPhase != Phase.PLAYING) isTouching = false;
        else {
            // Keyboard Control
            // For debug purposes, since it is for android
            isTouching = KeyboardInput();

            // Touchscreen Control
            if (!isTouching) {
                isTouching = TouchInput();
            }
        }

        var rot = transform.rotation.z;

        if (!isTouching) {
            _playSfx = true;
        }
        else if (_playSfx) {
            _sfx.Play();
            _playSfx = false;
        }

        switch (_orientation) {
            case Orientation.LEFT:
                PaddleMovement(MotorSpeed, isTouching, rot <= 0, rot >= _upperLimit - 0.02);
                break;
            case Orientation.RIGHT:
                PaddleMovement(-MotorSpeed, isTouching, rot >= 0, rot <= _upperLimit + 0.02);
                break;
        }
    }

    private void PaddleMovement(float motorSpeed, bool isTouch, bool lowerLimit, bool upperLimit) {
        // Sets the motor speed to move paddle up if user is touching the screen, down otherwise
        float newSpeed = isTouch ? -motorSpeed : motorSpeed;

        if (!isTouch) { //Set speed to 0 if paddle's rotation is at rest position
            if (lowerLimit) {
                newSpeed = 0;
                _motor.motorSpeed = newSpeed;
                var rotation = transform.rotation;
                rotation = new Quaternion(rotation.x, rotation.y, 0, rotation.w);
                transform.rotation = rotation;
            }
        }
        else if (upperLimit) { //Set speed to 0 if paddle's rotation is at top position
            newSpeed = 0;
            _motor.motorSpeed = newSpeed;
            var rotation = transform.rotation;
            rotation = new Quaternion(rotation.x, rotation.y, this._upperLimit, rotation.w);
            transform.rotation = rotation;
        }

        _motor.motorSpeed = newSpeed;
        _hj.motor = _motor;
    }

    private bool KeyboardInput() {
        switch (_orientation) {
            case Orientation.LEFT:
                return Input.GetKey(KeyCode.LeftArrow);
            case Orientation.RIGHT:
                return Input.GetKey(KeyCode.RightArrow);
            default:
                return false;
        }
    }

    private bool TouchInput() {
        for (var i = 0; i < Input.touchCount; i++) {
            var touch = Input.GetTouch(i);
            // ReSharper disable once PossibleLossOfFraction
            if (touch.position.x < width / 2) {
                if (_orientation != Orientation.LEFT) continue;
                return true;
            }

            // ReSharper disable once PossibleLossOfFraction
            if (!(touch.position.x > width / 2)) continue;
            if (_orientation != Orientation.RIGHT) continue;
            return true;
        }

        return false;
    }
}