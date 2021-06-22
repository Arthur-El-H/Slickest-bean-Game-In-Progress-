using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBean_Control : MonoBehaviour
{
    public move_Manager moveManager;
    public void Impact()
    {
        moveManager.stopDashing();
        moveManager.dazed();
    }
}
