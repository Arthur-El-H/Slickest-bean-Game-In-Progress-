using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainManager : MonoBehaviour
{
    public string lastScene;
    int winsForOne = 0, WinsForTwo = 0;
    bool musicPlaying = true;
    AudioSource audio;

    public GameObject winScene;
    public GameObject looseScene;


    public void resetWins()
    {
        winsForOne = 0;
        WinsForTwo = 0;
    }

    public void win(bool p1)
    {
        if (p1) { winsForOne++; }
        else    { WinsForTwo++; }
    }

    public int getWinsForOne() { return winsForOne; }
    public int getWinsForTwo() { return WinsForTwo; }

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        Screen.fullScreen = true;
    }

    private void Start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Startscreen");
        audio.loop = true;
        DontDestroyOnLoad(this);
    }

    public void toggleMusic()
    {
        musicPlaying = !musicPlaying;
        if (!musicPlaying) { audio.Stop(); }
        else { audio.Play(); }
    }

    public void win()
    {
        StartCoroutine(winning());
    }

    IEnumerator winning()
    {
        Destroy(GameObject.Find("player_bean"));
        Camera.main.transform.position = new Vector3(0, 0, -20);
        Instantiate(winScene, new Vector3(0, 0, 0), Quaternion.identity) ;
        yield return new WaitForSeconds(4);
    }

    public void loose()
    {
        StartCoroutine(loosing());
    }
    IEnumerator loosing()
    {
        Destroy(GameObject.Find("player_bean"));
        Camera.main.transform.position = new Vector3(0, 0, -20);
        Instantiate(looseScene, new Vector3(0, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(4);
    }
}
