using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class game_Manager : MonoBehaviour
{
    public mainManager mainManager;
    [SerializeField] ComboCounter comboCounter;
    public GameObject retryBtn;

    void Start()
    {
        mainManager = GameObject.Find("mainManager").GetComponent<mainManager>();
        retryBtn.SetActive(false);
        begin = Time.time;
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
        CalculateHighscore();
        SceneManager.LoadScene("Win");
    }

    int usedTime;
    int halbwertszeit = 10;
    float begin;
        
    private int CalculateHighscore()
    {
        usedTime = (int) (Time.time - begin);
        Debug.Log("used Time: " + usedTime);
        int result = 0;
        int timeUsed = usedTime / halbwertszeit;
        Debug.Log("time used: " + timeUsed);
        double step = System.Math.Pow(.5, timeUsed);
        Debug.Log("step: " + step);

        Debug.Log("maxCombo: " + comboCounter.maxCombo);
        Debug.Log("totalComboCounter: " + comboCounter.totalComboCounter);

        result = (int) (step * comboCounter.maxCombo * comboCounter.maxCombo * comboCounter.totalComboCounter);
        Debug.Log("You win with " + result + " points");

        return result;
    }
}
