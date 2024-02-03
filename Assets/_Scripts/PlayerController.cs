using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed; // Add a rotation speed variable

    private int _isRunningHash = Animator.StringToHash("IsRunning");
    private Animator _animator;
    private PlayerActionMap _inputActions;
    private CharacterController _character;

    private Vector2 _moveInputs;
    private Vector3 _movement;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _character = GetComponent<CharacterController>();
        _inputActions = new PlayerActionMap();
        _inputActions.Enable();
        _inputActions.Player.Movement.performed += ctx => _moveInputs = ctx.ReadValue<Vector2>();
    }

    void Update()
    {
        _movement = new Vector3(_moveInputs.x, 0f, _moveInputs.y);
        _movement *= Time.deltaTime * moveSpeed;
        _character.Move(_movement);

        // Rotate the player based on input
        if (_movement.sqrMagnitude > 0)
        {
            float targetAngle = Mathf.Atan2(_movement.x, _movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        _animator.SetBool(_isRunningHash, _movement.sqrMagnitude > 0);
    }
}
