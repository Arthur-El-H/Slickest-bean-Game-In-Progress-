using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class input_Manager : MonoBehaviour
{
    public move_Manager moveManager;
    public PlayerBean_Control control;
    [SerializeField] ComboManager comboManager;
    [SerializeField] beanManager beanManager;
    mainManager mainManager;
    [SerializeField] CorrectorOfFallingWithBeans corrector;



    bool jumpIsAble = true; 
    bool dashAble = true;
    bool secondJumpIsAble = true;

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
        if (Input.GetKeyDown (KeyCode.W))  { control.statemachine.getCurrentState()?.WBtnPressed(); }
        if (Input.GetKey     ("space"))    { control.statemachine.getCurrentState()?.SpaceHolded(); }
        if (Input.GetKeyUp   ("space"))    { control.statemachine.getCurrentState()?.SpaceUp(); }
        if (Input.GetKey     (KeyCode.A))  { control.statemachine.getCurrentState()?.ABtnPressed(); }
        if (Input.GetKey     (KeyCode.D))  { control.statemachine.getCurrentState()?.DBtnPressed(); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)  { onTheWall(true);  }
        if (collision.gameObject.layer == 10) { onTheWall(false); }
    }

    private void onTheWall(bool right)
    {
        control.statemachine.getCurrentState()?.OnTheWall(right);
    }

    public void offTheWall()
    {
        control.statemachine.getCurrentState()?.OffTheWall();
    }

    public void GoIntoAir()
    {
        control.statemachine.getCurrentState()?.OffTheGround();
    }

    public void gotToGround()
    {
        control.statemachine.getCurrentState()?.OnTheGround(); 
    }

    public void CrashedIntoBean()
    {
        control.statemachine.getCurrentState()?.CrashIntoBean();
    }
}
