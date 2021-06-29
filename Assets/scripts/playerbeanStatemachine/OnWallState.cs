using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWallState : IState
{
    PlayerBean_Control owner;
    move_Manager moveManager;
    jumpManager jumpManager;
    public Rigidbody2D playerRB;

    bool leftWall;   //Fallspeed of the non player beans!

    public OnWallState(PlayerBean_Control owner) { this.owner = owner; }

    public void Enter()
    {
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

    public void wBtnPressed()
    {
        // Ask Timing manager if timing is right

        //yes:
        jumpManager.wallJump(!leftWall);

        //no:
        jumpManager.timedWallJump(!leftWall);
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
