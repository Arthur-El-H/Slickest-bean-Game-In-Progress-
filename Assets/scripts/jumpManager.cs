using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpManager : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] ComboCounter comboCounter;

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
    internal void timedJump()
    {
        playerRB.velocity = Vector3.zero;
        playerRB.AddForce(Vector3.up * timedJumpPower);
        comboCounter.BeanJumpWasTimed();
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
        comboCounter.DoubleJumpWasTimed();
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
        comboCounter.WallJumpWasTimed();
    }

}
