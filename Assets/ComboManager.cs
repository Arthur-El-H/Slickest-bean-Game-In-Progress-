using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    /*
     * counting for:
     * landing on bean --> doublejump
     * landing on wall --> wallJump
     * midair after jump --> doublejump
     * 
     * 
     * when dashing:
     * breaking through bean
     * bouncing off wall
     * 
     */

    public bool timingWindowForBreakingBeanOpen;
    public bool timingWindowForBouncingOfWallOpen;
    public bool timingWindowForWallJumpOpen;
    public bool timingWindowForDoubleJumpOpen;      // Controlled by InAirState
    public bool timingWindowForBeanJumpOpen;


    public void OpenBeanBreaking()
    {
        StartCoroutine(OpenBreakingBean(1.5f));
    }
    public void OpenDoubleJump(float timeToWait)
    {
        StartCoroutine(OpeningDoubleJump(timeToWait));
    }
    public void OpenBeanJump(float timeToWait)
    {
        StartCoroutine(OpeningBeanJump(timeToWait));
    }
    public void OpenWallJump(float timeToWait)
    {
        StartCoroutine(OpeningWallJump(timeToWait));
    }

    public IEnumerator OpenBreakingBean (float timeToWait)
    {
        timingWindowForBreakingBeanOpen = true;
        Debug.Log("opening BreakingBean window");

        yield return new WaitForSeconds(timeToWait);
        timingWindowForBreakingBeanOpen = false;
        Debug.Log("Closing BreakingBean window");

    }

    public IEnumerator OpenBouncingOfWall (float timeToWait)
    {
        timingWindowForBouncingOfWallOpen = true;
        yield return new WaitForSeconds(timeToWait);
        timingWindowForBouncingOfWallOpen = false;
    }

    public IEnumerator OpeningWallJump (float timeToWait)
    {
        timingWindowForWallJumpOpen = true;
        yield return new WaitForSeconds(timeToWait);
        timingWindowForWallJumpOpen = false;
    }

    public IEnumerator OpeningDoubleJump (float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        Debug.Log("opening doublejump window");
        timingWindowForDoubleJumpOpen = true;
        yield return new WaitForSeconds(.2f);
        timingWindowForDoubleJumpOpen = false;
        Debug.Log("closing doublejump window");
    }

    public IEnumerator OpeningBeanJump (float timeToWait)
    {
        Debug.Log("opening beanjump window");
        timingWindowForBeanJumpOpen = true;
        yield return new WaitForSeconds(timeToWait);
        timingWindowForBeanJumpOpen = false;
        Debug.Log("closing beanjump window");
    }
}
