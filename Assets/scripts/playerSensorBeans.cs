using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSensorBeans : MonoBehaviour
{
    public input_Manager ipManager;
    public mainManager mainManager;
    public game_Manager gameManager;

    public bool bean;
    public bool wallR;
    public bool wallL;
    public bool win;
    public bool loose;
    public bool wallExit;

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
            if (win) { Debug.Log("win"); mainManager.win(); gameManager.loose(); }
            if (loose) { Debug.Log("loose");mainManager.loose(); gameManager.loose(); }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (wallExit) { Debug.Log("off the wall"); ipManager.offTheWall(); }
    }
}
