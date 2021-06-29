using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class input_Manager : MonoBehaviour
{
    public move_Manager moveManager;
    public PlayerBean_Control control;
    mainManager mainManager;
    [SerializeField] CorrectorOfFallingWithBeans corrector;

    bool jumpIsAble = true; 
    bool dashAble = true;
    bool secondJumpIsAble = true;
    void activateSecondJump(bool newState)
    {
        if(!newState) Debug.Log("I have been called!");
        secondJumpIsAble = newState; 
    }
    internal bool isDashing;

    internal bool isLoadingDash;
    internal bool rightWall;
    bool isDazed;

    internal void setIsDazed(bool dazed)
    {
        isDazed = dazed;
    }

    //float waitTimeForSecondJump = .1f;
    public position currentPositionState = position.ground;

    bool doubleJumpWindow;
    bool dashWallWindow;
    bool landingWindow;


    bool faceRight;


    // Balancing Statics! Beginning

    public float dashWallWindowTime = 1f;
    public float landingWindowTime = .5f;
    public float doubleJumpWindowTime;

    public float dashResetTime = 6f;

    // End


    private void Awake()
    {
        mainManager = GameObject.Find("mainManager").GetComponent<mainManager>();

        dashWallWindowTime = mainManager.dashWallWindowTime;
        landingWindowTime = mainManager.landingWindowTime;
        dashResetTime = mainManager.dashResetTime;
    }

    void Update()
    {
        if(currentPositionState == position.ground) { corrector.correct(); }
        if(isDazed) { return; }

        if (landingWindow && !isDashing)
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
            if (Input.GetKey(KeyCode.A) && (currentPositionState != position.wall ||  rightWall) ) { moveManager.moveLeft();  faceRight = false; }
            if (Input.GetKey(KeyCode.D) && (currentPositionState != position.wall || !rightWall) ) { moveManager.moveRight(); faceRight = true; }
            if (Input.GetKeyDown(KeyCode.W)) { jump(); }

            if (Input.GetKey("space"))   { if (dashAble) { moveManager.loadDash(); isLoadingDash = true; } }
            if (Input.GetKeyUp("space")) { if (dashAble) { dash(); isLoadingDash = false; } }
        }
    }

    private void timedDashWallJump()
    {
        Debug.Log("Combo!");
        moveManager.timedDashWallJump(rightWall);
    }

    private void timedJump()
    {
        Debug.Log("Combo!");
        moveManager.timedJump();
        activateSecondJump(true);
    }

    public IEnumerator reset(string timer, float timeToWait)
    {
        switch (timer)
        {
            case "jumpIsAble": jumpIsAble = false; break;
            case "secondJumpIsAble": activateSecondJump(false); break;
            case "dashAble": dashAble = false; break;
            case "isDazed": isDazed = true; break;
        }
        yield return new WaitForSeconds(timeToWait);
        switch (timer)
        {
            case "jumpIsAble": jumpIsAble = true; break;
            case "secondJumpIsAble": activateSecondJump(true); break;
            case "dashAble": dashAble = true; break;
            case "isDazed": isDazed = false; break;

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
        Debug.Log("currentPosition ist gerade " + currentPositionState);
        Debug.Log("secondJumpIsAble ist gerade " + secondJumpIsAble);
        switch (currentPositionState)
        {
            case position.ground:
                moveManager.jump();
                break;

            case position.air:
                if (secondJumpIsAble) { moveManager.doubleJump(); activateSecondJump(false); }
                break;

            case position.wall:
                moveManager.wallJump(rightWall); activateSecondJump(true);
                break;
        }
    }

    public void gotToGround()
    {
        currentPositionState = position.ground;
        jumpIsAble = true;
        activateSecondJump(true);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9) { onTheWall(true); }
        if (collision.gameObject.layer == 10) { onTheWall(false); }
    }

    private void onTheWall(bool right)
    {
        Debug.Log("On the wall!");
        rightWall = right;
        currentPositionState = position.wall;
        if (isDashing)
        {
            control.Impact();
        }
        moveManager.onWall = true;
    }

    public void offTheWall()
    {
        if (isDashing) { return; }
        Debug.Log("off the wall");
        currentPositionState = position.air;
        moveManager.onWall = false;
    }
}
