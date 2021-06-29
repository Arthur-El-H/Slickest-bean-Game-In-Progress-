using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DazedState : IState
{
    PlayerBean_Control owner;
    move_Manager moveManager;
    jumpManager jumpManager;
    dashManager dashManager;
    Animator anim;
    Rigidbody2D playerRB;

    int dazeFrames = 60;
    int dazeFrameCount;

    public DazedState(PlayerBean_Control owner)
    {
        this.owner = owner;
    }

    public void Enter()
    {
    }

    public void Execute()
    {
        dazeFrameCount++;
        if (dazeFrameCount >= dazeFrames)
        {
            owner.statemachine.ChangeState(new InAirState(owner));
            //or on bean? --> Check
            //or on wall? --> Check
        }
    }

    public void Exit()
    {
    }

    public void ABtnPressed()
    {
    }

    public void DBtnPressed()
    {
    }

    public void wBtnPressed()
    {
    }

    public void SpaceHolded()
    {
    }

    public void SpaceUp()
    {
    }
}
