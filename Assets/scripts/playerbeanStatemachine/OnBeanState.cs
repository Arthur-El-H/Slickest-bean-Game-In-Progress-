using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBeanState : IState
{
    PlayerBean_Control owner;
    move_Manager moveManager;
    jumpManager jumpManager;
    float fallSpeed = 1f;   //Fallspeed of the non player beans!

    public OnBeanState(PlayerBean_Control owner) { this.owner = owner; }

    public void Enter()
    {
    }

    public void Execute()
    {
        owner.transform.position = Vector2.MoveTowards((Vector2)owner.transform.position, (Vector2)owner.transform.position + Vector2.down, fallSpeed * Time.deltaTime);
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

    public void wBtnPressed()
    {
        // If timing is decided by timer, start timer for doublejump here!

        // Ask timingManager if timing is right. This timer is started by sensor of npc bean!
        //yes:
        jumpManager.timedJump();

        //no:
        jumpManager.jump();
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
