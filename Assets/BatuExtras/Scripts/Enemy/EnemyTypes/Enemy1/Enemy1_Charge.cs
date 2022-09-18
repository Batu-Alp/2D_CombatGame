using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Charge : Charge
{
    protected Enemy1 enemy;

    public Enemy1_Charge(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_Charge stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        /*if (isChargeTimeOver)
        {

            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }

        }*/

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }

        //else if (!isDetectingLedge || isDetectingWall)
        else if (isDetectingWall)

        {

            //connect to look for player
            stateMachine.ChangeState(enemy.lookForPlayer);

        }

        else if (isChargeTimeOver)
        {

            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }

            else
            {
                stateMachine.ChangeState(enemy.lookForPlayer);
            }

        }


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
