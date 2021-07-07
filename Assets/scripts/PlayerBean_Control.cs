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
    public ComboManager comboManager;
    public ComboCounter comboCounter;
    public beanManager beanManager;

    public Statemachine statemachine;

    private void Start()
    {
        statemachine = new Statemachine();
        statemachine.ChangeState(new OnBeanState(this));
    }

    private void Update()
    {
        statemachine.Update();
    }

    internal void DashStartCoroutineHelper(DashingState state)
    {
        StartCoroutine(state.Starting());
    }
}
