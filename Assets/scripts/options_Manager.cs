using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class options_Manager : MonoBehaviour
{
    mainManager mainManager;
    public Text dasWallTiming;
    public Text dazeTime;
    public Text dashFrame;
    public Text dashReset;
    public Text perfextJumpTiming;
    public Text dashSpeed;
    public Text jumpPower;
    public Text doubleJumpPower;
    public Text wallJumpPower;
    public Text perfectJumpPower;
    public Text endOfDashPower;
    public Text ReboundPower;
    public Text speed;

    public InputField dasWallTimingI;
    public InputField dazeTimeI;
    public InputField dashFrameI;
    public InputField dashResetI;
    public InputField perfextJumpTimingI;
    public InputField dashSpeedI;
    public InputField jumpPowerI;
    public InputField doubleJumpPowerI;
    public InputField wallJumpPowerI;
    public InputField perfectJumpPowerI;
    public InputField endOfDashPowerI;
    public InputField ReboundPowerI;
    public InputField speedI;

    private void Awake()
    {
        mainManager = GameObject.Find("mainManager").GetComponent<mainManager>();
        initialize();
    }

    public void backToLastScene()
    {
        SceneManager.LoadScene(mainManager.lastScene);
    }

    public void toggleMusic()
    {
        mainManager.toggleMusic();
    }

    private void initialize()
    {
        dasWallTimingI.text =           mainManager.dashWallWindowTime.ToString();
        dazeTimeI.text =                mainManager.dazeTime.ToString();
        dashFrameI.text =               mainManager.dashFrames.ToString();
        dashResetI.text =               mainManager.dashResetTime.ToString();
        perfextJumpTimingI.text =       mainManager.landingWindowTime.ToString();
        dashSpeedI.text =               mainManager.dashSpeed.ToString();
        jumpPowerI.text =               mainManager.jumpPower.ToString();
        doubleJumpPowerI.text =         mainManager.doubleJumpPower.ToString();
        wallJumpPowerI.text =           mainManager.wallJumpPower.ToString();
        perfectJumpPowerI.text =        mainManager.timedJumpPower.ToString();
        endOfDashPowerI.text =          mainManager.dashEndPower.ToString();
        ReboundPowerI.text =            mainManager.reboundPower.ToString();
        speedI.text =                   mainManager.speed.ToString();
    }

    public void actualize()
    {
    dasWallTiming.text =        dasWallTimingI.text;
    dazeTime.text =             dazeTimeI.text;
    dashFrame.text =            dashFrameI.text;
    dashReset.text =            dashResetI.text;
    perfextJumpTiming.text =    perfextJumpTimingI.text;
    dashSpeed.text =            dashSpeedI.text;
    jumpPower.text =            jumpPowerI.text;
    doubleJumpPower.text =      doubleJumpPowerI.text;
    wallJumpPower.text =        wallJumpPowerI.text;
    perfectJumpPower.text =     perfectJumpPowerI.text;
    endOfDashPower.text =       endOfDashPowerI.text;
    ReboundPower.text =         ReboundPowerI.text;
    speed.text =                speedI.text;
    }

    public void submit()
    {
        mainManager.dashWallWindowTime = float.Parse(dasWallTimingI.text);
        mainManager.dazeTime = float.Parse(dazeTimeI.text);
        mainManager.dashFrames = int.Parse(dashFrameI.text);
        mainManager.dashResetTime = float.Parse(dashResetI.text);
        mainManager.dashSpeed = float.Parse(dashSpeedI.text);
        mainManager.jumpPower = float.Parse(jumpPowerI.text);
        mainManager.doubleJumpPower = float.Parse(doubleJumpPowerI.text);
        mainManager.wallJumpPower = float.Parse(wallJumpPowerI.text);
        mainManager.timedJumpPower = float.Parse(perfectJumpPowerI.text);
        mainManager.dashEndPower = float.Parse(endOfDashPowerI.text);
        mainManager.reboundPower = float.Parse(ReboundPowerI.text);
        mainManager.speed = float.Parse(speedI.text);

        actualize();
    }

}
