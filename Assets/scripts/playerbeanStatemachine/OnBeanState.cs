using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBeanState : IState
{
    PlayerBean_Control owner;
    move_Manager moveManager;
    jumpManager jumpManager;
    dashManager dashManager;
    ComboManager comboManager;

    float fallSpeed = 1f;   //Fallspeed of the non player beans!

    public OnBeanState(PlayerBean_Control owner)
    {
        this.owner = owner;
        this.moveManager = owner.moveManager;
        this.jumpManager = owner.jumpManager;
        this.dashManager = owner.dashManager;
        this.comboManager = owner.comboManager;
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
        if(comboManager.timingWindowForBeanJumpOpen)
        {
            comboManager.OpenDoubleJump(.5f);
            jumpManager.timedJump();
        }
        else
        {
            comboManager.OpenDoubleJump(.25f);
            jumpManager.jump();
        }
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
