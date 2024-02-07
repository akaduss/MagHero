using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour, IDeathHandler
{
    public PlayerStats PlayerStats = new();
    public float fireRange = 10f;
    public float fireRate = 1.0f;

    private float timeSinceLastShot;

    private bool isAlive;

    [SerializeField] private float facingEnemySpeed;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject targetMarkParticle;
    private GameObject enemy;
    public GameObject levelupParticle;
    public GameObject xpBar;

    private float rotationSpeed;

    private readonly int _isRunningHash = Animator.StringToHash("IsRunning");
    private Animator _animator;
    private PlayerActionMap _inputActions;
    private CharacterController _character;
    private SkillSystem _skillSystem;
    private Collider[] enemyColliders;
    private readonly int maxColliders = 20;

    private Vector2 _moveInputs;
    private Vector3 _movement;

    private Vector3 velocity;

    private void Awake()
    {
        enemyColliders = new Collider[maxColliders];
        _animator = GetComponentInChildren<Animator>();
        _character = GetComponent<CharacterController>();
        _skillSystem = GetComponent<SkillSystem>();
        _inputActions = new PlayerActionMap();
        _inputActions.Enable();
        _inputActions.Player.Movement.performed += ctx => _moveInputs = ctx.ReadValue<Vector2>();
        isAlive = true;

        EnemyDeath.OnEnemyDeath += EnemyDeath_OnEnemyDeath;
        xpBar.GetComponent<MoreMountains.Tools.MMProgressBar>().UpdateBar01(0);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Transform playerTransform = FindObjectOfType<PlayerController>().transform;
        playerTransform.position = transform.position;
    }

    private void EnemyDeath_OnEnemyDeath(float obj)
    {
        PlayerStats.xp += obj;
        print(xpBar.layer.ToString());
        var nor = PlayerStats.xp / PlayerStats.NextLevelXp;
        xpBar.GetComponent<MoreMountains.Tools.MMProgressBar>().UpdateBar01(nor);
        if(PlayerStats.xp > PlayerStats.NextLevelXp)
        {
            PlayerStats.xp -= PlayerStats.NextLevelXp;
            PlayerStats.level++;
            nor = PlayerStats.xp / PlayerStats.NextLevelXp;
            xpBar.GetComponent<MoreMountains.Tools.MMProgressBar>().UpdateBar01(nor);
            levelupParticle.GetComponent<ParticleSystem>().Play();

        }
    }

    private void Update()
    {
        if (isAlive == false) return;

        HandleMovementInput();
        ApplyGravity();

        enemy = FindClosestEnemy();

        HandleEnemyMarker();

        HandleRotation();

        HandleAttack();
    }

    private void HandleEnemyMarker()
    {
        if (enemy == null)
        {
            targetMarkParticle.SetActive(false);
            return;
        }

        targetMarkParticle.SetActive(true);
        targetMarkParticle.transform.position = enemy.transform.position;
    }

    private void HandleMovementInput()
    {
        _movement = new Vector3(_moveInputs.x, 0f, _moveInputs.y);
        _movement *= Time.deltaTime * PlayerStats.moveSpeed;
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
        else if (enemy != null)
        {
            Vector3 directionToEnemy = enemy.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, facingEnemySpeed * Time.deltaTime);
        }
    }

    private void HandleAttack()
    {
        if (enemy == null) return;

        bool canFire = _movement.sqrMagnitude == 0 && Time.time - timeSinceLastShot > fireRate;
        if (canFire)
        {
            timeSinceLastShot = Time.time;
            _skillSystem.DefaultAttack(enemy.transform, PlayerStats.AttackDamage);
        }
    }

    void ApplyGravity()
    {
        if (_character.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += PlayerStats.gravity * Time.deltaTime;
        _character.Move(velocity * Time.deltaTime);
    }

    private GameObject FindClosestEnemy()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        int colliderCount = Physics.OverlapSphereNonAlloc(transform.position, fireRange, enemyColliders, enemyLayer);

        if (colliderCount > 0)
        {
            foreach (var enemyCollider in enemyColliders.Take(colliderCount))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemyCollider.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemyCollider.gameObject;
                }
            }
        }

        return closestEnemy;
    }

    public void HandleDeath()
    {
        isAlive = false;
        _animator.SetTrigger("Die");
        targetMarkParticle.SetActive(false);

        throw new System.NotImplementedException();
    }

}
