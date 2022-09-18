using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : State
{
    protected D_PlayerDetected stateData;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performLongRangeAction;
    protected bool performCloseRangeAction;
    protected bool isLedgeDetected;


    public DetectPlayer(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isLedgeDetected = entity.CheckLedge();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();


    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(0f);

        //isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        //isPlayerInMaxAgroRange = entity.CheckPlayerInMinAgroRange();

        performLongRangeAction = false;
        //core.Movement.SetVelocityX(0f);     
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //core.Movement.SetVelocityX(0f);

        if (Time.time >= startTime + stateData.longRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        //isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        //isPlayerInMaxAgroRange = entity.CheckPlayerInMinAgroRange();
    }

}
