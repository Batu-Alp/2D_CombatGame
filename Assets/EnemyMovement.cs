using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    Animator animator;
    public float attackRange = 3f;
    public float moveRange = 6f;
    public float enemyHealth = 50;

    private bool isFlipped = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < moveRange && distance > attackRange)
        {
            animator.SetInteger("move", 1);
        }else if(distance < attackRange)
        {
            animator.SetTrigger("attack");
        }else if(enemyHealth <= 0)
        {
            animator.SetTrigger("death");
        }
        else
        {
            animator.SetInteger("move", 0);
        }
    }

    public void lookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }else if(transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}
