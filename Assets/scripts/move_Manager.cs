using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_Manager : MonoBehaviour
{
    public input_Manager ipManager;
    public Rigidbody2D playerRB;
    float speed = 3f;
    float dashSpeed = 5f;
    float jumpPower = 250f;
    float doubleJumpPower = 250f;
    float wallJumpPower = 300f;
    float timedJumpPower = 450f;
    int dashFrames = 180;
    int dashFrameCounter;

    Animator anim;

    Vector3 rightWallJumpDir = new Vector3(-1f, 1f, 0);
    Vector3 leftWallJumpDir = new Vector3(1f, 1f, 0);

    Vector2 dashL = new Vector2(-1f, 1f);
    Vector2 dashR = new Vector2(1f, 1f);
    Vector2 dashU = new Vector2(0, 1f);
    float dashEndPower = 100f;

    Vector2 dir = Vector2.zero;

    Vector2 lastPos;
    Vector2 delta;
    private void Awake()
    {
        Application.targetFrameRate = 60; 
        lastPos = transform.position;
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        delta = (Vector2) transform.position - lastPos;
        Camera.main.transform.position += new Vector3(0, delta.y, 0);
        lastPos = (Vector2)transform.position;
    }

    public void jump(bool right)
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
        Debug.Log("dashing finished");
        anim.Play("idle");
        ipManager.isDashing = false;
        playerRB.gravityScale = 1f;
        playerRB.AddForce(dir * dashEndPower);
        dashFrameCounter = 0;
    }
}
