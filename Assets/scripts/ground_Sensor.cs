using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum position { ground, wall, air }
public class ground_Sensor : MonoBehaviour
{
    public input_Manager inputManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision with bean");
        if (collision.gameObject.layer == 8) { inputManager.gotToGround(); }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 15) { inputManager.GoIntoAir(); }
    }
}
