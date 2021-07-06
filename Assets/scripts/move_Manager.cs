using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_Manager : MonoBehaviour
{
    public input_Manager ipManager;
    public Rigidbody2D playerRB;
    mainManager mainManager;


    Animator anim;
    float speed = 3f;

    private void Awake()
    {
        Application.targetFrameRate = 60; 
        anim = GetComponent<Animator>();

        mainManager = GameObject.Find("mainManager").GetComponent<mainManager>();

        speed = mainManager.speed;
    }
    private void FixedUpdate()
    {
        //correction
        if (playerRB.velocity.x < -1) { playerRB.velocity = new Vector2 (-1, playerRB.velocity.y); }
        if (playerRB.velocity.x >  1) { playerRB.velocity = new Vector2 ( 1, playerRB.velocity.y); }

        playerRB.velocity = new Vector2(0, playerRB.velocity.y);
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
}
