using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSensorBeans : MonoBehaviour
{
    public input_Manager ipManager;
    public mainManager mainManager;

    public bool bean;
    public bool wallR;
    public bool wallL;
    public bool win;
    public bool loose;

    private void Awake()
    {
        mainManager = GameObject.Find("mainManager").GetComponent<mainManager>();
    }

    //public bool bean;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11) 
        { 
            if (bean) StartCoroutine(ipManager.openWindow("landingWindow", ipManager.landingWindowTime));
            if (wallL) { ipManager.rightWall = false; StartCoroutine(ipManager.openWindow("dashWallWindow", ipManager.dashWallWindowTime));  }
            if (wallR) { ipManager.rightWall = true;  StartCoroutine(ipManager.openWindow("dashWallWindow", ipManager.dashWallWindowTime));  }
            if (win) { Debug.Log("win"); mainManager.win(); }
            if (loose) { Debug.Log("loose");mainManager.loose(); }

        }
    }
}
