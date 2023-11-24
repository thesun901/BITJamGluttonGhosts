using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int healthPoints;

    [SerializeField] private float currentSpeed;
    private bool canDash;
    private bool isDead;

    [SerializeField] private float walkingSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashDuration;


    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        canDash = true;
        currentSpeed = walkingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfDead();
        Movement();
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

    IEnumerator Dash()
    {
        if (canDash)
        {
            currentSpeed = dashSpeed;
            canDash = false;

            yield return new WaitForSeconds(dashDuration);

            currentSpeed = walkingSpeed;

            yield return new WaitForSeconds(dashCooldown);

            canDash = true;
        }
    }

    void CheckIfDead()
    {
        isDead = healthPoints <= 0;
    }
}
