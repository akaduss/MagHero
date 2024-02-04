using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRange = 10f;
    public float fireRate = 1.0f;

    private float timeSinceLastShot;

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
        _animator.SetBool(_isRunningHash, _movement.sqrMagnitude > 0);

        // Rotate the player based on input
        if (_movement.sqrMagnitude > 0)
        {
            float targetAngle = Mathf.Atan2(_movement.x, _movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        else if(_movement.sqrMagnitude == 0 && Time.time - timeSinceLastShot > fireRate)
        {
            timeSinceLastShot = Time.time;
            FireProjectile(FindClosestEnemy());
        }

    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(currentPosition, enemy.transform.position);

            if (distanceToEnemy < closestDistance && distanceToEnemy <= fireRange)
            {
                closestEnemy = enemy;
                closestDistance = distanceToEnemy;
            }
        }

        return closestEnemy;
    }

    void FireProjectile(GameObject enemy)
    {
        // Instantiate a new projectile at the current position and rotation of this GameObject
        GameObject go = Instantiate(projectilePrefab, transform.position + Vector3.up, Quaternion.LookRotation(enemy.transform.position - transform.position));
        go.GetComponent<Rigidbody>().velocity = (enemy.transform.position - transform.position).normalized * 10f;
    }
}
