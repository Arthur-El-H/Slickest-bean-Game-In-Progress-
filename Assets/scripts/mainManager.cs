using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainManager : MonoBehaviour
{
    public string lastScene;
    bool musicPlaying = true;
    AudioSource audio;

    public GameObject winScene;
    public GameObject looseScene;


    // Balancing statics
    //public float doubleJumpWindowTime;  not implemented yet

    public float dashWallWindowTime = 1f;
    public float landingWindowTime = .5f;
    public float dashResetTime = 6f;

    public float dazeTime = 1.5f;
    public int dashFrames = 180;

    public float dashSpeed = 5f;
    public float jumpPower = 250f;
    public float doubleJumpPower = 250f;
    public float wallJumpPower = 300f;
    public float timedJumpPower = 450f;

    public float dashEndPower = 50f;
    public float reboundPower = 10f;

    public float speed = 3f;

    //

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

    public void replay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
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
