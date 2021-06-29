using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashManager : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D playerRB;

    public void dash()
    {

    }
    internal void loadDash()
    {
        playerRB.velocity = Vector3.zero;
        anim.Play("loadDash");
    }
}
