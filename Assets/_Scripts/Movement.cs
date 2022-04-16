using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] int _walkingSpeed;

    [SerializeField] Rigidbody _playerRb;
    [SerializeField] AnimationController _animController;
    public FloatingJoystick JoyStick;

    private void Start() {
        Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), ObjectHolder.Instance.helper3.GetComponent<CapsuleCollider>());
        Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), ObjectHolder.Instance.helper2.GetComponent<CapsuleCollider>());
        Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), ObjectHolder.Instance.helper1.GetComponent<CapsuleCollider>());
    }

    private void Update() {

        #region move by joystick
        _playerRb.velocity = new Vector3(JoyStick.Horizontal * _walkingSpeed, _playerRb.velocity.y, JoyStick.Vertical * _walkingSpeed);

        if (JoyStick.Horizontal != 0 || JoyStick.Vertical != 0) {
            transform.rotation = Quaternion.LookRotation(_playerRb.velocity);
            _animController.StartWalkingAnim();
        } else {
            _animController.StopWalkingAnim();
        }
        #endregion
    }

}
