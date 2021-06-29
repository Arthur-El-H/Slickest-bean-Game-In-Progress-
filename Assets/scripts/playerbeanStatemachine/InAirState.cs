using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : IState
{
    PlayerBean_Control owner;
    jumpManager jumpManager;
    move_Manager moveManager;
    dashManager dashManager;

    bool doubleJumpAvailable = true;
    public InAirState(PlayerBean_Control owner)
    {
        this.owner = owner;
        this.moveManager = owner.moveManager;
        this.jumpManager = owner.jumpManager;
        this.dashManager = owner.dashManager;
    }
    public void Enter()
    {
        Debug.Log("Entering InAir State");
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }

    public void ABtnPressed()
    {
        moveManager.moveLeft();
    }

    public void DBtnPressed()
    {
        moveManager.moveRight();
    }

    public void WBtnPressed()
    {
        // ask TimingManager if timing is right
        if (!doubleJumpAvailable) { return; }

        // if yes:
        jumpManager.timedDoubleJump();

        // if no:
        jumpManager.doubleJump();

        doubleJumpAvailable = false;
    }
    public void SpaceHolded()
    {
        dashManager.loadDash();
        owner.statemachine.ChangeState(new LoadingDashState(owner));
    }

    public void SpaceUp()
    {
        owner.statemachine.ChangeState(new LoadingDashState(owner));
    }
}
