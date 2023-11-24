using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.GraphicsBuffer;

public class UniversalEnemy : MonoBehaviour, IDamagable
{
    public int healthPoints;


    private GameObject player;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float minimumDistance;
    [SerializeField]
    private float uncomfortableDistance;


    private float distanceToHero;
    [SerializeField] private GameObject primaryAttack, secondaryAttack;
    [SerializeField] private float primaryAttackCooldown, secondaryAttackCooldown;
    private float primaryAttackTimer, secondaryAttackTimer;
    [SerializeField] private float speed;

    public void damage(int amount)
    {
        healthPoints -= amount;

        if (healthPoints < 0)
        {
            death();
        }
    }

    void death()
    {
        Destroy(gameObject);
    }

    void firstAttack()
    {
        Vector3 vectorToTarget = player.transform.position - transform.position;
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 180) * vectorToTarget;
        Vector3 addedPosition = vectorToTarget.normalized;
        GameObject.Instantiate(primaryAttack, transform.position + addedPosition, Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget * -90));
        primaryAttackTimer = 0;
    }

    void secondAttack()
    {
        GameObject.Instantiate(primaryAttack);
        primaryAttackTimer = 0;
        secondaryAttackTimer = 0;
    }

    void attacksControl()
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

    void move()
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
    }

    // Update is called once per frame
    void Update()
    {
        move();
        attacksControl();
    }
}
