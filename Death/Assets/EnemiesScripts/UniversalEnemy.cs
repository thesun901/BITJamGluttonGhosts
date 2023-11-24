using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UniversalEnemy : MonoBehaviour, IDamagable
{
    public int healthPoints;

    [SerializeField]
    private float attackRange;
    private float distanceToHero;
    [SerializeField]
    private GameObject primaryAttack, secondaryAttack;
    [SerializeField]
    private float primaryAttackCooldown, secondaryAttackCooldown;
    private float primaryAttackTimer, secondaryAttackTimer;

    public void damage(int amount)
    {
        healthPoints -= amount;

        if(healthPoints<0)
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
        GameObject.Instantiate(primaryAttack);
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attacksControl();
    }
}
