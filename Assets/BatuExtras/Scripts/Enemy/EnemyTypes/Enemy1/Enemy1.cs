using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public Enemy1_IdleState idleState { get; private set; }
    public Enemy1_MoveState moveState { get; private set; }
    public Enemy1_DetectPlayer playerDetectedState { get; private set; }
    public Enemy1_Charge chargeState { get; private set; }
    public Enemy1_LookForPlayer lookForPlayer { get; private set; }
    public Enemey1_MeleeAttackState meleeAttackState { get; private set; }
    public Enemy1_StuntState stunState { get; private set; }
    public Enemy1_DeadState deadState { get; private set; }


    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_Charge chargeStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private Transform meleeAttackPosition;
    /*[SerializeField]
    private D_StuntState stunStateData;*/
    [SerializeField]
    private D_DeadState deadStateData;




    public override void Start()
    {

        base.Start();
        moveState = new Enemy1_MoveState(this, stateMachine, "move", moveStateData, this);
        //stateMachine.Initialize(moveState);   
        idleState = new Enemy1_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Enemy1_DetectPlayer(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new Enemy1_Charge(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayer = new Enemy1_LookForPlayer(this, stateMachine, "lookForPlayer", lookForPlayerData, this);
        meleeAttackState = new Enemey1_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        //stunState = new Enemy1_StuntState(this, stateMachine, "stun", stunStateData, this);
        deadState = new Enemy1_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.Initialize(moveState);

    }



    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);

    }

    public override void Damage_Enemy(AttackDetails attackDetails)
    {

        base.Damage_Enemy(attackDetails);

        if (isDead)
        {

            stateMachine.ChangeState(deadState);
        }

        /*else if (isStunned && stateMachine.currentState != stunState)
        {

            stateMachine.ChangeState(stunState);
        }*/

        /*if (isStunned && stateMachine.currentState != stunState)
        {

            stateMachine.ChangeState(stunState);
        }

        else if (isDead)
        {

            stateMachine.ChangeState(deadState);
        }*/

    }

}




