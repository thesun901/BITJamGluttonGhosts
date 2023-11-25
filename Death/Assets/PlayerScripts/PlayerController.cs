using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player state
    public int healthPoints;
    private bool isDead;
    private bool canDash;
    private bool isSlowed;

    // Movement
    [SerializeField] private float currentSpeed;
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float dashSpeedBonus;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashDuration;

    // Melee Attacks
    [SerializeField] public int meleeDamage;
    [SerializeField] public float meleeCooldown;
    private float meleeTimer;

    // Range Attacks
    [SerializeField] private GameObject rangeAttackObject;
    [SerializeField] public int rangeDamage;
    [SerializeField] public float rangeCooldown;
    private float rangeTimer;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        canDash = true;
        isSlowed = false;
        currentSpeed = walkingSpeed;

        meleeTimer = meleeCooldown;
        rangeTimer = 69;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfDead();
        Movement();
        Attack();
    }

    void Movement()
    {
        float moveInputHorizontal = Input.GetAxis("Horizontal");
        float moveInputVertical = Input.GetAxis("Vertical");
        Vector2 velocity = new Vector2(moveInputHorizontal * currentSpeed * Time.deltaTime, moveInputVertical * currentSpeed * Time.deltaTime);

        if (!isDead)
        {
            // Movement
            transform.Translate(velocity);

            // Dash
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Dash());
            }
        }
    }

    void Attack()
    {
        rangeTimer += Time.deltaTime;
        meleeTimer += Time.deltaTime;

        if (!isDead)
        {
            // Range attack
            if (Input.GetKeyDown(KeyCode.Mouse0) && rangeTimer >= rangeCooldown)
            {
                rangeTimer = 0;

                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 vectorToTarget = mousePosition - transform.position;
                Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 180) * vectorToTarget;
                GameObject.Instantiate(rangeAttackObject, transform.position, Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget * -90));
            }
        }
    }

    IEnumerator Dash()
    {
        if (canDash)
        {
            currentSpeed += dashSpeedBonus;
            canDash = false;

            yield return new WaitForSeconds(dashDuration);

            currentSpeed -= dashSpeedBonus;

            yield return new WaitForSeconds(dashCooldown);

            canDash = true;
        }
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
    }
}
