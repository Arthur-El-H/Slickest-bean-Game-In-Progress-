using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_Manager : MonoBehaviour
{
    public input_Manager ipManager;
    public Rigidbody2D playerRB;
    mainManager mainManager;

    internal void stopDashing()
    {
        dashFrameCounter = dashFrames;
        impacted = true;
        Debug.Log("You Hit The Wall");
    }

    internal void dazed()
    {
        StartCoroutine(ipManager.reset("isDazed", dazeTime));
    }


    int dashFrameCounter;

    Animator anim;

    Vector3 rightWallJumpDir = new Vector3(-1f, 1f, 0);
    Vector3 leftWallJumpDir = new Vector3(1f, 1f, 0);

    Vector2 dashL = new Vector2(-1f, 1f);
    Vector2 dashR = new Vector2(1f, 1f);
    Vector2 dashU = new Vector2(0, 1f);

    Vector2 dir = Vector2.zero;

    bool impacted;
    public bool onWall;


    // Balancing Statics! Beginning
    float dazeTime = 1.5f;
    int dashFrames = 180;

    float dashSpeed = 5f;
    float jumpPower = 250f;
    float doubleJumpPower = 250f;
    float wallJumpPower = 300f;
    float timedJumpPower = 450f;

    float dashEndPower = 50f;
    float reboundPower = 10f;

    float speed = 3f;
    // End
    private void Awake()
    {
        Application.targetFrameRate = 60; 
        anim = GetComponent<Animator>();

        mainManager = GameObject.Find("mainManager").GetComponent<mainManager>();
        dazeTime = mainManager.dazeTime;
        dashFrames = mainManager.dashFrames;
        dashSpeed = mainManager.dashSpeed;
        jumpPower = mainManager.jumpPower;
        doubleJumpPower = mainManager.doubleJumpPower;
        wallJumpPower = mainManager.wallJumpPower;
        timedJumpPower = mainManager.timedJumpPower;
        
        dashEndPower = mainManager.dashEndPower;
        reboundPower = mainManager.reboundPower;

        speed = mainManager.speed;
    }
    private void FixedUpdate()
    {
        //correction
        if (onWall && playerRB.velocity.y < -5f) { playerRB.velocity = new Vector2 (playerRB.velocity.x, -5f); }
        playerRB.velocity = new Vector2(0, playerRB.velocity.y);
    }

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

    IEnumerator delayJump(bool right, bool wall, float power)
    {
        if (!wall)
        {
            if (right)
            {
                anim.Play("jumpR"); Debug.Log("wie oft");
                for (int i = 0; i<10; i++)
                {
                    yield return new WaitForEndOfFrame();
                }
                playerRB.velocity = Vector3.zero;
                playerRB.AddForce(Vector3.up * power);
            }

            else
            {
                anim.Play("jumpR"); Debug.Log("wie oft");
                for (int i = 0; i < 10; i++)
                {
                    yield return new WaitForEndOfFrame();
                }
                playerRB.velocity = Vector3.zero;
                playerRB.AddForce(Vector3.up * power);
            }
        }

        else
        {
            if (right)
            {

            }

            else
            {

            }
        }
    }

    internal void loadDash()
    {
        playerRB.velocity = Vector3.zero;
        anim.Play("loadDash");
    }

    public void moveLeft()
    {
        anim.Play("moveLeft");
        transform.position = Vector2.MoveTowards((Vector2)transform.position, (Vector2)transform.position + Vector2.left, speed * Time.deltaTime);
    }   
    public void moveRight()
    {
        anim.Play("moveRight");
        transform.position = Vector2.MoveTowards((Vector2)transform.position, (Vector2)transform.position + Vector2.right, speed * Time.deltaTime);
    }

    internal void timedDashWallJump(bool rightWall)
    {
        dashFrameCounter = 0;
        Debug.Log("Combo! Changed dir!");
        //playerRB.velocity = Vector3.zero;
        if (rightWall) dir = dashL;
        else dir = dashR;
    }

    public void dash(string direction)
    {
        ipManager.isDashing = true;
        switch (direction)
        {
            case "left":  StartCoroutine(dashing("left")); Debug.Log("l"); anim.Play("dashLeft"); break;
            case "right": StartCoroutine(dashing("right")); Debug.Log("r"); anim.Play("dashRight"); break;
            case "up":    StartCoroutine(dashing("up")); Debug.Log("up"); anim.Play("dashUp"); break;
        }
    }

    IEnumerator dashing( string direction)
    {
        Debug.Log("Start dashing");
        playerRB.gravityScale = 0;
        playerRB.velocity = Vector3.zero;

        switch (direction)
        {
            case "left": dir = dashL; anim.Play("dashLeft"); break;
            case "right": dir = dashR; anim.Play("dashRight"); break;
            case "up": dir = dashU; anim.Play("dashUp"); break;
        }

        for (dashFrameCounter = 0; dashFrameCounter < dashFrames; dashFrameCounter++)
        {
            Vector2 target = (Vector2)transform.position + dir;
            transform.position = Vector2.MoveTowards((Vector2)transform.position, target, dashSpeed * Time.deltaTime);            
            yield return new WaitForEndOfFrame();
        }

        if(impacted)
        {
            impacted = false;
            Debug.Log("ouch");
            playerRB.AddForce( getReboundDir(ipManager.rightWall) * reboundPower);
        }

        else
        {
            Debug.Log("dashing finished");
            playerRB.AddForce(dir * dashEndPower);
        }
        anim.Play("idle"); //comes in 'else' after implementation of daze animation
        ipManager.isDashing = false;
        playerRB.gravityScale = 1f;
        dashFrameCounter = 0;
    }

    private Vector3 getReboundDir(bool rightWall)
    {
        if (rightWall) return rightWallJumpDir;
        else return leftWallJumpDir;
    }
}
