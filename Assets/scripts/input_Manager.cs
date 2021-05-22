using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class input_Manager : MonoBehaviour
{
    public move_Manager moveManager;
    bool jumpIsAble = true; 
    bool dashAble = true;
    bool secondJumpIsAble = true;
    internal bool isDashing;

    internal bool isLoadingDash;
    internal bool rightWall;
    float dashResetTime = 6f;
    //float waitTimeForSecondJump = .1f;
    public position currentPositionState = position.ground;

    bool doubleJumpWindow;
    bool dashWallWindow;
    bool landingWindow;
    internal float doubleJumpWindowTime;
    internal float dashWallWindowTime = 1f;
    internal float landingWindowTime = .5f;

    bool faceRight;


    void Update()
    {
        if (landingWindow)
        {
            if (Input.GetKeyDown(KeyCode.W)) { timedJump(); }
        }

        if (dashWallWindow)
        {
            if (Input.GetKey("space")) { timedDashWallJump(); Debug.Log("should work from here"); }
        }

        if (isLoadingDash)
        {
            if (Input.GetKey("space")) { moveManager.loadDash(); isLoadingDash = true; }
            if (Input.GetKeyUp("space")) { if (dashAble) dash(); isLoadingDash = false; }
        }

        else if (isDashing)
        {

        }

        else
        {
            if (Input.GetKey(KeyCode.A)) { moveManager.moveLeft(); faceRight = false; }
            if (Input.GetKey(KeyCode.D)) { moveManager.moveRight(); faceRight = true; }
            if (Input.GetKeyDown(KeyCode.W)) { jump(); }

            if (Input.GetKey("space"))   { if (dashAble) { moveManager.loadDash(); isLoadingDash = true; } }
            if (Input.GetKeyUp("space")) { if (dashAble) { dash(); isLoadingDash = false; } }
        }
    }

    private void timedDashWallJump()
    {
        Debug.Log("Combo!");
        moveManager.playerRB.velocity = Vector3.zero;
        moveManager.timedDashWallJump(rightWall);
    }

    private void timedJump()
    {
        Debug.Log("Combo!");
        moveManager.timedJump();
        secondJumpIsAble = true;
    }

    IEnumerator reset(string timer, float timeToWait)
    {
        switch (timer)
        {
            case "jumpIsAble": jumpIsAble = false; break;
            case "secondJumpIsAble": secondJumpIsAble = false; break;
            case "dashAble": dashAble = false; break;
        }
        yield return new WaitForSeconds(timeToWait);
        switch (timer)
        {
            case "jumpIsAble": jumpIsAble = true; break;
            case "secondJumpIsAble": secondJumpIsAble = true; break;
            case "dashAble": dashAble = true; break;
        }
    }
    internal IEnumerator openWindow(string window, float openTime)
    {
        Debug.Log("Open " + window);
        switch (window)
        {
            case "landingWindow": landingWindow = true; break;
            case "dashWallWindow": dashWallWindow = true; Debug.Log("window opened"); break;
            case "doubleJumpWindow": doubleJumpWindow = true; break;
        }
        yield return new WaitForSeconds(openTime);
        switch (window)
        {
            case "landingWindow": landingWindow = false; break;
            case "dashWallWindow": dashWallWindow = false; break;
            case "doubleJumpWindow": doubleJumpWindow = false; break;
        }
    }

    void dash()
    {
        if (Input.GetKey(KeyCode.D)) { Debug.Log("dash right"); moveManager.dash("right"); }
        else if (Input.GetKey(KeyCode.A)) { moveManager.dash("left"); }
        else { moveManager.dash("up"); }
        StartCoroutine(reset("dashAble", dashResetTime));
    }

    void jump()
    {
        switch (currentPositionState)
        {
            case position.ground:
                moveManager.jump(faceRight); secondJumpIsAble = false;
                //StartCoroutine(reset("secondJumpIsAble", waitTimeForSecondJump));
                break;

            case position.air: 
                if (secondJumpIsAble) moveManager.doubleJump(); secondJumpIsAble = false;
                break;

            case position.wall:
                moveManager.wallJump(rightWall); secondJumpIsAble = true;
                break;
        }
    }

    public void gotToGround()
    {
        currentPositionState = position.ground;
        jumpIsAble = true;
        secondJumpIsAble = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9) { onTheWall(true); }
        if (collision.gameObject.layer == 10) { onTheWall(false); }
    }

    private void onTheWall(bool right)
    {
        rightWall = right;
        currentPositionState = position.wall;
        moveManager.playerRB.gravityScale = .2f;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10) { offTheWall(); }
    }

    private void offTheWall()
    {
        currentPositionState = position.air;
        moveManager.playerRB.gravityScale = 1f;
    }
}
