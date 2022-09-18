using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private float inputTimer, attack1Radius, attack1Damage;
    [SerializeField]
    private Transform attack1HitBoxPos;
    [SerializeField]
    private LayerMask whatIsDamageable;
    [SerializeField]
    private float stunDamageAmount;

    private bool gotInput, isAttacking, isFirstAttack;

    private float lastInputTime = Mathf.NegativeInfinity;


    private AttackDetails attackDetails;
    //private float[] attackDetails = new float[2];

    private Animator anim;

    private PlayerController PC;
    private PlayerStats PS;

    private bool damageBoost = false;

    private float temp_damage;



    private void Start()
    {
        temp_damage = attack1Damage;
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        PC = GetComponent<PlayerController>();
        PS = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }

    public void DamageBoost(float value)
    {

        //attack1Damage += value;
        StartCoroutine(Damage_Coroutine(value));

    }

    private IEnumerator Damage_Coroutine(float value)
    {
        if (!damageBoost)
        {
            attack1Damage += value;
            //graphics.GetComponent<Animator>().SetTrigger("damage");
            damageBoost = true;
            StartCoroutine(IndicateImmortal());
            yield return new WaitForSeconds(30);
            damageBoost = false;
            attack1Damage = temp_damage;
        }
    }
    private IEnumerator IndicateImmortal()
    {
        while (damageBoost)
        {
            //spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            //spriteRenderer.enabled = true;
            //yield return new WaitForSeconds(.1f);
        }
    }

    private void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0)) // Check mouse button is click

        {
            if (combatEnabled)
            {
                //Attempt combat
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks()
    {
        if (gotInput)
        {
            //Perform Attack1
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                anim.SetBool("attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking);
            }
        }

        if (Time.time >= lastInputTime + inputTimer)
        {
            //Wait for new input
            gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        //attackDetails[0] = attack1Damage;
        //attackDetails[1] = transform.position.x;
        attackDetails.damageAmount = attack1Damage;
        attackDetails.position = transform.position;
        attackDetails.stuntDamageAmount = stunDamageAmount;

        foreach (Collider2D collider in detectedObjects)
        {
            //collider.transform.parent.SendMessage("Damage", attackDetails);
            collider.transform.parent.SendMessage("Damage_Enemy", attackDetails);

            //Instantiate hit particle
        }
    }

    private void FinishAttack1()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("attack1", false);
    }

    /*private void Damage(float[] attackDetails)
    {

        int direction;

        PS.DecreaseHealth(attackDetails[0]);

        if (attackDetails[1] < transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        PC.Knockback(direction);

    }
*/
    private void DamagePlayer(AttackDetails attackDetails)

    {


        int direction;

        PS.DecreaseHealth(attackDetails.damageAmount);

        if (attackDetails.position.x < transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        PC.Knockback(direction);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }

}
