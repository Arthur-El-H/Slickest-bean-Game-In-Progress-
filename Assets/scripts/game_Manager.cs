using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_Manager : MonoBehaviour
{
    public mainManager mainManager;

    public GameObject retryBtn;

    void Start()
    {
        mainManager = GameObject.Find("mainManager").GetComponent<mainManager>();
        retryBtn.SetActive(false);
    }

    public void replay()
    {
        mainManager.replay();
    }

    public void loose()
    {
        retryBtn.SetActive(true);
    }

}
