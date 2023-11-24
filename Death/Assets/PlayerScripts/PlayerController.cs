using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int healthPoints;
    [SerializeField] private float speed;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
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
        Vector2 velocity = new Vector2(moveInputHorizontal * speed * Time.deltaTime, moveInputVertical * speed * Time.deltaTime);

        if (!isDead)
        {
            transform.Translate(velocity);
        }
    }

    void CheckIfDead()
    {
        isDead = healthPoints <= 0;
    }
}
