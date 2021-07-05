using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DazedState : IState
{
    PlayerBean_Control owner;

    int dazeFrames = 60;
    int dazeFrameCount;

    public DazedState(PlayerBean_Control owner)
    {
        this.owner = owner;
    }

    public void Enter()
    {
        Debug.Log("Entering Dazed State");
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

    public void WBtnPressed()
    {
    }

    public void SpaceHolded()
    {
    }

    public void SpaceUp()
    {
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
