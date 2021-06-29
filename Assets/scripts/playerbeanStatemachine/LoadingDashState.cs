using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingDashState : IState
{
    PlayerBean_Control owner;
    move_Manager moveManager;
    jumpManager jumpManager;
    dashManager dashManager;
    Animator anim;
    public Rigidbody2D playerRB;

    bool toLeft;
    bool toRight;

    public LoadingDashState(PlayerBean_Control owner) { this.owner = owner; }

    public void Enter()
    {
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

    public void wBtnPressed()
    {
    }

    public void SpaceHolded()
    {
    }

    public void SpaceUp()
    {
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

}
