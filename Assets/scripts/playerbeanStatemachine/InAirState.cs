using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : IState
{
    PlayerBean_Control owner;
    jumpManager jumpManager;
    move_Manager moveManager;

    bool doubleJumpAvailable = true;

    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void ABtnPressed()
    {
        moveManager.moveLeft();
    }

    public void DBtnPressed()
    {
        moveManager.moveRight();
    }

    public void wBtnPressed()
    {
        // ask TimingManager if timing is right
        if (!doubleJumpAvailable) { return; }

        // if yes:
        jumpManager.timedDoubleJump();

        // if no:
        jumpManager.doubleJump();
    }
    public void SpaceHolded()
    {
        // change to LoadDash state
    }

    public void SpaceUp()
    {
        // Dash through dash Manager
    }
}
