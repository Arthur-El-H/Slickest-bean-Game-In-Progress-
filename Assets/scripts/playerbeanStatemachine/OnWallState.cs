using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWallState : IState
{
    PlayerBean_Control owner;
    move_Manager moveManager;
    jumpManager jumpManager;
    dashManager dashManager;
    Rigidbody2D playerRB;

    bool leftWall;   

    public OnWallState(PlayerBean_Control owner, bool leftWall) 
    { 
        this.owner = owner;
        this.moveManager = owner.moveManager;
        this.jumpManager = owner.jumpManager;
        this.dashManager = owner.dashManager;
        this.playerRB = owner.playerRB;
        this.leftWall = leftWall;
    }

    public void Enter()
    {
        Debug.Log("Entering OnWall State");
    }

    public void Execute()
    {
        if (playerRB.velocity.y < -5f) { playerRB.velocity = new Vector2(playerRB.velocity.x, -5f); }  //Correction for falling speed
        playerRB.velocity = new Vector2(0, playerRB.velocity.y); // Correction for push to side
    }

    public void Exit()
    {
    }

    public void ABtnPressed()
    {
        if(!leftWall) moveManager.moveLeft();
    }

    public void DBtnPressed()
    {
        if(leftWall) moveManager.moveRight();
    }

    public void WBtnPressed()
    {
        // Ask Timing manager if timing is right

        //yes:
        jumpManager.wallJump(!leftWall);

        //no:
        jumpManager.timedWallJump(!leftWall);
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
