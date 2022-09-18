using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_StuntState : StuntState
{
    private Enemy1 enemy;

    public Enemy1_StuntState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_StuntState stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isStunTimeOver)
        {
            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
            else if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.chargeState);
            }
            else
            {
                enemy.lookForPlayer.SetTurnImmediately(true);
                stateMachine.ChangeState(enemy.lookForPlayer);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
