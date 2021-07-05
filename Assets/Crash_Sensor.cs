using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crash_Sensor : MonoBehaviour
{
    [SerializeField] input_Manager inputManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == 8) { Debug.Log("crash with bean"); inputManager.CrashedIntoBean(); }
    }
}
