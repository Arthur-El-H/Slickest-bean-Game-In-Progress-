using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashManager : MonoBehaviour
{
    PlayerBean_Control playerBean_Control;

    //Change direction while dashing??
    Vector2 dashL = new Vector2(-1f, 1f);
    Vector2 dashR = new Vector2(1f, 1f);
    Vector2 dashU = new Vector2(0, 1f);
    float dazeTime = 1.5f;
    int dashFrames = 180;

    float dashSpeed = 5f;
    float dashEndPower = 50f;
    float reboundPower = 10f;

    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D playerRB;

    public void Dash(Vector2 dir)
    {
        Debug.Log("Yeah im dashing");
        Vector2 target = (Vector2)transform.position + dir;
        transform.position = Vector2.MoveTowards((Vector2)transform.position, target, dashSpeed * Time.deltaTime);
    }
    internal void loadDash()
    {
        playerRB.velocity = Vector3.zero;
        anim.Play("loadDash");
    }
    public void lastPush()
    {

    }
    public void BounceOff( dashDir directionFromWherePlayerHits)
    {
        Debug.Log("ouch");

        switch (directionFromWherePlayerHits)
        {
            case dashDir.left:
                playerRB.AddForce(dashR * reboundPower);
                break;

            case dashDir.right:
                playerRB.AddForce(dashL * reboundPower);
                break;

            case dashDir.up:
                break;
        }

        playerBean_Control.statemachine.ChangeState(new DazedState(playerBean_Control)); //without lastPush!

    }
}
