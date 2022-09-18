using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : State
{
    protected D_Charge stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;



    public Charge(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_Charge stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }


    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();

    }



    public override void Enter()
    {
        base.Enter();
        isChargeTimeOver = false;


        //isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        //isDetectingLedge = entity.CheckLedge();
        //isDetectingWall = entity.CheckWall();

        entity.SetVelocity(stateData.chargeSpeed);


    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.chargeTime)
        {

            isChargeTimeOver = true;
        }


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        //isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        //isDetectingLedge = entity.CheckLedge();
        //isDetectingWall = entity.CheckWall();

    }


}
