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
    bool started;

    public DashingState(PlayerBean_Control owner, dashDir dir)
    {
        this.owner = owner;
        this.moveManager = owner.moveManager;
        this.jumpManager = owner.jumpManager;
        this.dashManager = owner.dashManager;
        this.playerRB = owner.playerRB;
        this.comboManager = owner.comboManager;
        this.comboCounter = owner.comboCounter;
        this.anim = owner.animator;
        this.dir = dir;
    }

    public void Enter()
    {
        Debug.Log("Entering Dashing State");

        playerRB.gravityScale = 0;
        playerRB.velocity = Vector3.zero;

        owner.DashStartCoroutineHelper(this);
    }

    public IEnumerator Starting()
    {
        anim.Play("startDash");
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        SetDirection();
        started = true;
    }

    private void SetDirection()
    {
        switch (dir)
        {
            case dashDir.left:
                currentDirValue = dashL;
                anim.Play("dashLeft");
                break;

            case dashDir.right:
                currentDirValue = dashR;
                anim.Play("dashRight");
                break;

            case dashDir.up:
                currentDirValue = dashU;
                anim.Play("dashUp");
                break;
        }
    }

    public void Execute()
    {
        if (started)
        {
            dashManager.Dash(currentDirValue);
            dashFrameCount++;
            if (dashFrameCount >= dashFrames)
            {
                owner.statemachine.ChangeState(new InAirState(owner));
                if(dashDir.left == dir) { anim.Play("EndDashLeft"); }
                else { anim.Play("EndDashRight"); }
                LastDash();
            }
        }
    }

    private void LastDash()
    {
        playerRB.AddForce(new Vector2(0, 100f));
    }
    public void Exit()
    {
        playerRB.gravityScale = 1f;
    }

    public void ABtnPressed()
    {
        if (!started) return;
        dir = dashDir.left;
        currentDirValue = dashL;
        anim.Play("dashLeft");
    }

    public void DBtnPressed()
    {
        if (!started) return;
        Debug.Log("Fehler");
        dir = dashDir.right;
        currentDirValue = dashR;
        anim.Play("dashRight");
    }

    public void WBtnPressed()
    {
        if (!started) return;
        dir = dashDir.up;
        currentDirValue = dashU;
        anim.Play("dashUp");
    }

    public void SpaceHolded()
    {
    }

    public void SpaceUp()
    {
        Debug.Log("Space up is called in dash state");
        Debug.Log(comboManager.timingWindowForBouncingOfWallOpen);

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
                anim.Play("dashRight");
                break;

            case dashDir.right:
                dir = dashDir.left;
                currentDirValue = dashL;
                anim.Play("dashLeft");
                break;

            case dashDir.up:
                anim.Play("dashUp");
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
        Debug.Log("Test2: DelayCount: " + delayCount);
        if(delayCount > delay)
        {
            Debug.Log("Crashed into bean while dashing");
            comboManager.timingWindowForBreakingBeanOpen = false;
            owner.statemachine.ChangeState(new DazedState(owner));
            delayCount = 0;
            AnimateCrash( (dir == dashDir.left) );
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
            AnimateCrash( (dir == dashDir.left) );
        }
    }

    private void AnimateCrash(bool left)
    {
        if(left)
        {
            anim.Play("DazedLeft");
        }
        else
        {
            anim.Play("DazedRight");
        }
    }
}