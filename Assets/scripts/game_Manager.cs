using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
        SceneManager.LoadScene("GameOver");
    }
    public void Win()
    {
        SceneManager.LoadScene("Win");
    }
}
