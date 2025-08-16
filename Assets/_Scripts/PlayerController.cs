using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public event Action OnPlayerDeath;

    private bool isAlive;

    [SerializeField] private LayerMask groundLayer;

    private float rotationSpeed;

    private readonly int _isRunningHash = Animator.StringToHash("IsRunning");
    private Animator _animator;
    private PlayerActionMap _inputActions;
    private CharacterController _character;

    private Vector2 _moveInputs;
    private Vector3 _movement;

    private Vector3 velocity;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _animator = GetComponentInChildren<Animator>();
        _character = GetComponent<CharacterController>();
        _inputActions = new PlayerActionMap();
        _inputActions.Enable();
        _inputActions.Player.Movement.performed += ctx => _moveInputs = ctx.ReadValue<Vector2>();
        isAlive = true;
    }

    private void Update()
    {
        if (isAlive == false) return;

        HandleMovementInput();
        ApplyGravity();
        HandleRotation();
    }

    private void HandleMovementInput()
    {
        _movement = new Vector3(_moveInputs.x, 0f, _moveInputs.y);
        _movement *= Time.deltaTime * 5f;
        _character.Move(_movement);
        _animator.SetBool(_isRunningHash, _movement.sqrMagnitude > 0);
    }

    private void HandleRotation()
    {
        if (_movement.sqrMagnitude > 0)
        {
            float targetAngle = Mathf.Atan2(_movement.x, _movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    void ApplyGravity()
    {
        if (_character.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y -= Physics.gravity.sqrMagnitude * Time.deltaTime;
        _character.Move(velocity * Time.deltaTime);
    }

}
