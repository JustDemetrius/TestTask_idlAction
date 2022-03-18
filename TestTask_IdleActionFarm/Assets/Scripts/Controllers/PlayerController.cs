using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rb;

    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] internal int _curAmmount;
    [SerializeField] internal int _maxAmmount = 40;
    [SerializeField] internal int _Coins = 0;


    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    public void MovePlayer()
    {
        _rb.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rb.velocity.y, _joystick.Vertical * _moveSpeed);
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rb.velocity);
        }
        _animator.SetFloat("Speed", _rb.velocity.magnitude);
    }
}
