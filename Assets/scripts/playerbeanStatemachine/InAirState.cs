using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : IState
{
    PlayerBean_Control owner;
    jumpManager jumpManager;
    move_Manager moveManager;
    dashManager dashManager;
    ComboManager comboManager;
    Rigidbody2D playerRB;

    bool doubleJumpAvailable = true;
    public InAirState(PlayerBean_Control owner)
    {
        this.owner = owner;
        this.moveManager = owner.moveManager;
        this.jumpManager = owner.jumpManager;
        this.dashManager = owner.dashManager;
        this.comboManager = owner.comboManager;
        this.playerRB = owner.playerRB;
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

        if (comboManager.timingWindowForDoubleJumpOpen)
        {
            jumpManager.timedDoubleJump();
        }
        else
        {
            jumpManager.doubleJump();
        }
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

    public void OnTheWall(bool right)
    {
        owner.statemachine.ChangeState(new OnWallState(owner, !right));
        comboManager.OpenWallJump(.3f);
    }

    public void OffTheWall()
    {
    }

    public void OnTheGround()
    {
        owner.statemachine.ChangeState(new OnBeanState(owner));
        comboManager.OpenBeanJump(.1f);
    }

    public void OffTheGround()
    {
    }

    public void CrashIntoBean()
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
    }
}
