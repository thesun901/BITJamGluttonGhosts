using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScript : UniversalEnemy
{
    private Transform body;
    private SpriteRenderer sr;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        body = gameObject.transform.GetChild(0);
        sr = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        InvokeRepeating("animations", 0.05f, 0.05f);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    protected void animations()
    {

        distanceToHero = Vector2.Distance(transform.position, player.transform.position);


        if (primaryAttackTimer >= primaryAttackCooldown - 1.1f && distanceToHero <= attackRange)
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }

        if (player.transform.position.x > transform.position.x)
        {
            body.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            body.localScale = new Vector3(1, 1, 1);
        }

        if (distanceToHero > minimumDistance)
        {
            animator.SetBool("isWalking", true);
        }

        else if (distanceToHero < uncomfortableDistance)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
    


