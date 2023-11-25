using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangerScript : UniversalEnemy
{
    [SerializeField] Sprite relaxed;
    [SerializeField] Sprite ready;
    [SerializeField] Sprite shooting;
    private SpriteRenderer sr;
    private Animator animator;




    private void Start()
    {
        player = GameObject.Find("Player");
        sr = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        InvokeRepeating("animationDetails", 0.05f, 0.05f);
        InvokeRepeating("moveAnimations", 0.05f, 0.05f);
    }



    void animationDetails()
    {
        if ((primaryAttackTimer >= primaryAttackCooldown - 0.25f || secondaryAttackTimer >= secondaryAttackCooldown - 0.25f) && distanceToHero <= attackRange)
        {
            sr.sprite = shooting;
        }
        else if ((primaryAttackTimer >= primaryAttackCooldown - 0.5f || secondaryAttackTimer >= secondaryAttackCooldown - 0.5f) && distanceToHero <= attackRange)
        {
            sr.sprite = ready;
        }
        else
        {
            sr.sprite = relaxed;
        }
    }

    protected void moveAnimations()
    {
        distanceToHero = Vector2.Distance(transform.position, player.transform.position);

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
