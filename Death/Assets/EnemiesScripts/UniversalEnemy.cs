using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.GraphicsBuffer;

public class UniversalEnemy : MonoBehaviour, IDamagable
{
    public int healthPoints;


    protected GameObject player;
    [SerializeField]
    protected float attackRange;
    [SerializeField]
    protected float minimumDistance;
    [SerializeField]
    protected float uncomfortableDistance;
    [SerializeField] protected GameManager gm;
    [SerializeField] public bool spawnedByAbility;


    protected float distanceToHero;
    [SerializeField] protected GameObject primaryAttack, secondaryAttack;
    [SerializeField] protected float primaryAttackCooldown, secondaryAttackCooldown;
    protected float primaryAttackTimer, secondaryAttackTimer;
    [SerializeField] protected float speed;

    public void damage(int amount)
    {
        healthPoints -= amount;

        if (healthPoints < 0)
        {
            death();
        }
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    void death()
    {
        if(!spawnedByAbility)
        { 
            gm.killCount++;
        }
        Destroy(gameObject);
    }

    protected void firstAttack()
    {
        Vector3 vectorToTarget = player.transform.position - transform.position;
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 180) * vectorToTarget;
        Vector3 addedPosition = vectorToTarget.normalized;
        GameObject.Instantiate(primaryAttack, transform.position + addedPosition, Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget * -90));
        primaryAttackTimer = 0;
    }

    protected void secondAttack()
    {
        GameObject.Instantiate(secondaryAttack, transform.position, Quaternion.identity);
        primaryAttackTimer = 0;
        secondaryAttackTimer = 0;
    }


    protected void attacksControl()
    {
        primaryAttackTimer += Time.deltaTime;
        secondaryAttackTimer += Time.deltaTime;

        if (primaryAttackTimer >= primaryAttackCooldown && distanceToHero <= attackRange)
        {
            firstAttack();
        }

        if (secondaryAttackTimer >= secondaryAttackCooldown && distanceToHero <= attackRange)
        {
            secondAttack();
        }
    }

    protected void move()
    {
        distanceToHero = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToHero > minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        }

        else if (distanceToHero < uncomfortableDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        attacksControl();
    }
}
