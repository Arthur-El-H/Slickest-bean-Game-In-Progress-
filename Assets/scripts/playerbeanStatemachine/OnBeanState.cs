using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBeanState : IState
{
    PlayerBean_Control owner;
    move_Manager moveManager;
    jumpManager jumpManager;
    dashManager dashManager;

    float fallSpeed = 1f;   //Fallspeed of the non player beans!

    public OnBeanState(PlayerBean_Control owner)
    {
        this.owner = owner;
        this.moveManager = owner.moveManager;
        this.jumpManager = owner.jumpManager;
        this.dashManager = owner.dashManager;
    }

    public void Enter()
    {
        Debug.Log("Entering ONBEAN State");
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

    public void WBtnPressed()
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
        dashManager.loadDash();
        owner.statemachine.ChangeState(new LoadingDashState(owner));
    }

    public void SpaceUp()
    {
        // Dash through dash Manager
    }
}
