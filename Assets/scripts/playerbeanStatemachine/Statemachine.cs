﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statemachine 
{
    IState currentState;
    public IState getCurrentState() { return currentState; }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        if (currentState != null) currentState.Execute();
    }
}
