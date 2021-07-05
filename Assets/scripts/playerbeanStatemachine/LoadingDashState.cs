using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingDashState : IState
{
    PlayerBean_Control owner;
    dashManager dashManager;
    private ComboManager comboManager;
    Animator anim;

    bool toLeft;
    bool toRight;

    public LoadingDashState(PlayerBean_Control owner) 
    { 
        this.owner = owner;
        this.dashManager = owner.dashManager;
        this.comboManager = owner.comboManager;
        anim = owner.animator;
    }

    public void Enter()
    {
        Debug.Log("Entering loading Dash State");
    }

    public void Execute()
    {
        dashManager.loadDash();
        //could be necesarry to change that if execute is called AFTER ABtnPressed() or DBtnPressed()
        toRight = false;
        toLeft = false;
    }

    public void Exit()
    {
    }

    public void ABtnPressed()
    {
        toLeft = true;
    }

    public void DBtnPressed()
    {
        toLeft = false;
    }

    public void WBtnPressed()
    {
    }

    public void SpaceHolded()
    {
    }

    public void SpaceUp()
    {
        Debug.Log("Space up is called in loadingdash state");
        if (toLeft)
        {
            owner.statemachine.ChangeState(new DashingState(owner, dashDir.left));
        }
        else if (toRight)
        {
            owner.statemachine.ChangeState(new DashingState(owner, dashDir.right));
        }
        else 
        {
            owner.statemachine.ChangeState(new DashingState(owner, dashDir.up));
        }
    }

    public void OnTheGround()
    {
    }

    public void OffTheGround()
    {
    }

    public void OnTheWall(bool right)
    {
    }

    public void OffTheWall()
    {
    }

    public void CrashIntoBean()
    {
    }
}
