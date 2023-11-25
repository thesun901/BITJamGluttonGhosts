using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;

    // Player state
    public int healthPoints;
    private bool isDead;
    private bool isSlowed;

    // Movement
    [SerializeField] private float currentSpeed;
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float dashSpeedBonus;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashDuration;
    private float dashTimer;

    // Melee Attacks
    [SerializeField] private GameObject meleeAttackObject;
    [SerializeField] public int meleeDamage;
    [SerializeField] public float meleeCooldown;
    private float meleeTimer;

    // Range Attacks
    [SerializeField] private GameObject rangeAttackObject;
    [SerializeField] public int rangeDamage;
    [SerializeField] public float rangeCooldown;
    private float rangeTimer;

    // Animations
    private Animator animator;
    private Transform body;

    // Sounds
    public AudioSource playerAudio;
    [SerializeField] private AudioClip meleeSound;
    [SerializeField] private AudioClip rangeSound;
    [SerializeField] private AudioClip playerHurtSound;
    [SerializeField] private AudioClip dashSound;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        isDead = false;
        isSlowed = false;
        currentSpeed = walkingSpeed;

        body = gameObject.transform.GetChild(0);
        animator = body.GetComponent<Animator>();

        meleeTimer = meleeCooldown;
        rangeTimer = rangeCooldown;
        dashTimer = dashCooldown;

        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfDead();
        if (!isDead)
        {
            Movement();
            Attack();
            Dash();
        }
    }

    void Movement()
    {
        float moveInputHorizontal = Input.GetAxis("Horizontal");
        float moveInputVertical = Input.GetAxis("Vertical");
        Vector2 velocity = new Vector2(moveInputHorizontal * currentSpeed * Time.deltaTime, moveInputVertical * currentSpeed * Time.deltaTime);

        // Movement
        transform.Translate(velocity);

        //animation
        if (velocity != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
            if (velocity.x > 0)
            {
                body.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                body.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void Attack()
    {
        rangeTimer += Time.deltaTime;
        meleeTimer += Time.deltaTime;

        // Range attack
        if (Input.GetKeyDown(KeyCode.Mouse0) && rangeTimer >= rangeCooldown)
        {
            rangeTimer = 0;
            animator.SetTrigger("attack");
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 vectorToTarget = mousePosition - transform.position;
            vectorToTarget.z = 0;
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 180) * vectorToTarget;
            GameObject.Instantiate(rangeAttackObject, transform.position + vectorToTarget.normalized * 1, Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget * -90));
            playerAudio.PlayOneShot(rangeSound);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && meleeTimer >= meleeCooldown)
        {
            meleeTimer = 0;
            animator.SetTrigger("attack");
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 vectorToTarget = mousePosition - transform.position;
            vectorToTarget.z = 0;
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
            GameObject.Instantiate(meleeAttackObject, transform.position + vectorToTarget.normalized * 2, Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget));
            playerAudio.PlayOneShot(meleeSound);
        }
    }

    void Dash()
    {
        dashTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && dashTimer >= dashCooldown)
        {
            dashTimer = 0;
            playerAudio.PlayOneShot(dashSound, 3f);
            StartCoroutine(DashCoroutine());
        }

        gameManager.UpdateDashCooldown(Mathf.Min(dashTimer, dashCooldown)/dashCooldown);
    }

    IEnumerator DashCoroutine()
    {
        currentSpeed += dashSpeedBonus;

        yield return new WaitForSeconds(dashDuration);

        currentSpeed -= dashSpeedBonus;

        yield return new WaitForSeconds(dashCooldown);
    }

    IEnumerator SlowDown(float slowDuration, float slowStrength)
    {
        currentSpeed *= 1 - slowStrength;
        isSlowed = true;

        yield return new WaitForSeconds(slowDuration);

        currentSpeed = walkingSpeed;
        isSlowed = false;
    }

    public void GetSlowed(float slowDuration, float slowStrength)
    {
        if (!isSlowed)
        {
            StartCoroutine(SlowDown(slowDuration, slowStrength));
        }
    }

    void CheckIfDead()
    {
        isDead = healthPoints <= 0;
    }

    public void OnHit(int damage)
    {
        healthPoints -= damage;
        playerAudio.PlayOneShot(playerHurtSound);
        gameManager.UpdateHealthPoints();
    }
}
