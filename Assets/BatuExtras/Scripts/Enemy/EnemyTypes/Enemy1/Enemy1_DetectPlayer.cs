using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_DetectPlayer : DetectPlayer
{
    private Enemy1 enemy;

    public Enemy1_DetectPlayer(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        /*
        else if (performLongRangeAction)
        {            
            stateMachine.ChangeState(enemy.chargeState);
        }*/

        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(enemy.chargeState);

        }
        else if (!isPlayerInMaxAgroRange)
        {
            //enemy.idleState.SetFlipAfterIdle(false);
            //stateMachine.ChangeState(enemy.idleState);
            //stateMachine.ChangeState(enemy.playerDetectedState);
            stateMachine.ChangeState(enemy.lookForPlayer);

        }
        /*else if (!isDetectingLedge)
        {
            core.Movement.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }*/
        else if (isLedgeDetected)
        {

            entity.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
