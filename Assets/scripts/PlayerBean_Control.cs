using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBean_Control : MonoBehaviour
{
    public move_Manager moveManager;
    public dashManager dashManager;
    public jumpManager jumpManager;
    public Animator animator;
    public Rigidbody2D playerRB;

    public Statemachine statemachine;

    private void Start()
    {
        statemachine = new Statemachine();
        statemachine.ChangeState(new OnBeanState(this));
    }
    public void Impact()
    {
        moveManager.stopDashing();
        moveManager.dazed();
    }
}
