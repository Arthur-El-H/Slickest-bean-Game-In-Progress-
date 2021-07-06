using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingState : IState
{
    PlayerBean_Control owner;
    move_Manager moveManager;
    jumpManager jumpManager;
    dashManager dashManager;
    Animator anim;
    Rigidbody2D playerRB;
    ComboManager comboManager;
    ComboCounter comboCounter;

    private Vector2 currentDirValue;
    private static Vector2 dashL = new Vector2(-1f, 1f);
    private static Vector2 dashR = new Vector2(1f, 1f);
    private static Vector2 dashU = new Vector2(0, 1f);

    private dashDir dir;

    int dashFrames = 180;
    int dashFrameCount;

    public DashingState(PlayerBean_Control owner, dashDir dir)
    {
        this.owner = owner;
        this.moveManager = owner.moveManager;
        this.jumpManager = owner.jumpManager;
        this.dashManager = owner.dashManager;
        this.playerRB = owner.playerRB;
        this.comboManager = owner.comboManager;
        this.comboCounter = owner.comboCounter;
        this.dir = dir;
    }

    public void Enter()
    {
        Debug.Log("Entering Dashing State");

        playerRB.gravityScale = 0;
        playerRB.velocity = Vector3.zero;

        switch (dir)
        {
            case dashDir.left:
                currentDirValue = dashL;
                break;

            case dashDir.right:
                currentDirValue = dashR;
                break;

            case dashDir.up:
                currentDirValue = dashU;
                break;
        }
    }

    public void Execute()
    {
        dashManager.Dash(currentDirValue);
        dashFrameCount++;
        if(dashFrameCount >= dashFrames)
        {
            owner.statemachine.ChangeState(new InAirState(owner));
        }
    }

    public void Exit()
    {
        playerRB.gravityScale = 1f;
    }

    public void ABtnPressed()
    {
        dir = dashDir.left;
        currentDirValue = dashL;
    }

    public void DBtnPressed()
    {
        dir = dashDir.right;
        currentDirValue = dashR;
    }

    public void WBtnPressed()
    {
        dir = dashDir.up;
        currentDirValue = dashU;
    }

    public void SpaceHolded()
    {
    }

    public void SpaceUp()
    {
        Debug.Log("Space up is called in dash state");
        Debug.Log(comboManager.timingWindowForBouncingOfWallOpen);
        //get timingbool - get current position: Is a bean broken? Bounced of a wall?

        if(comboManager.timingWindowForBreakingBeanOpen)
        {
            dashFrameCount = dashFrameCount - 60;
            owner.beanManager.BreakBean();
            delayCount = 0;
        }

        else if (comboManager.timingWindowForBouncingOfWallOpen)
        {
            dashFrameCount = dashFrameCount - 60;
            ReverseDirection();
            delayCount = 0;
        }

        else
        {
            owner.statemachine.ChangeState(new InAirState(owner));
        }
    }

    private void ReverseDirection()
    {
        comboCounter.BouncedOffTheWall();
        switch (dir)
        {
            case dashDir.left:
                dir = dashDir.right;
                currentDirValue = dashR;
                break;

            case dashDir.right:
                dir = dashDir.left;
                currentDirValue = dashL;
                break;

            case dashDir.up:
                // could check at which wall the player is and push to the other direction
                break;
        }
    }

    public void OnTheGround()
    {
        owner.statemachine.ChangeState(new DazedState(owner));
    }

    public void OffTheGround()
    {
    }

    public void OffTheWall()
    {
    }

    int delayCount;
    int delay = 60;

    public void CrashIntoBean()
    {
        delayCount++;

        if(delayCount > delay)
        {
            comboManager.timingWindowForBreakingBeanOpen = false;
            owner.statemachine.ChangeState(new DazedState(owner));
            delayCount = 0;
        }
    }
    public void OnTheWall(bool right)
    {
        delayCount++;

        if (delayCount > delay)
        {
            comboManager.timingWindowForBouncingOfWallOpen = false;
            owner.statemachine.ChangeState(new DazedState(owner));
            delayCount = 0;
        }
    }
}