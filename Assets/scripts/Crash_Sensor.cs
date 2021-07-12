using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crash_Sensor : MonoBehaviour
{
    [SerializeField] input_Manager inputManager;
    [SerializeField] beanManager beanManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == 8) 
        {
            beanManager.beanToBeDestroyed = collision.gameObject; 
            inputManager.CrashedIntoBean(); 
        }
    }
}
