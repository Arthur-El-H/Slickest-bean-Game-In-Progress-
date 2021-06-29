using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpManager : MonoBehaviour
{
    public Rigidbody2D playerRB;

    float jumpPower = 250f;
    float doubleJumpPower = 250f;
    float timedDoubleJumpPower = 350f;
    float wallJumpPower = 300f;
    float timedWallJumpPower = 400f;
    float timedJumpPower = 450f;

    Vector3 rightWallJumpDir = new Vector3(-1f, 1f, 0);
    Vector3 leftWallJumpDir = new Vector3(1f, 1f, 0);

    public void jump()
    {
        playerRB.velocity = Vector3.zero;
        playerRB.AddForce(Vector3.up * jumpPower);
    }
    public void doubleJump()
    {
        playerRB.velocity = Vector3.zero;
        playerRB.AddForce(Vector3.up * doubleJumpPower);
    }
    public void timedDoubleJump()
    {
        playerRB.velocity = Vector3.zero;
        playerRB.AddForce(Vector3.up * timedDoubleJumpPower);
    }

    internal void timedJump()
    {
        playerRB.velocity = Vector3.zero;
        playerRB.AddForce(Vector3.up * timedJumpPower);
    }

    public void wallJump(bool right)
    {
        playerRB.velocity = Vector3.zero;
        if (right) { playerRB.AddForce(rightWallJumpDir * wallJumpPower); }
        else { playerRB.AddForce(leftWallJumpDir * wallJumpPower); }
    }

    public void timedWallJump(bool right)
    {
        playerRB.velocity = Vector3.zero;
        if (right) { playerRB.AddForce(rightWallJumpDir * timedWallJumpPower); }
        else { playerRB.AddForce(leftWallJumpDir * timedWallJumpPower); }
    }

}
