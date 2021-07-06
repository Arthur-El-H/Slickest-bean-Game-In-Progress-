using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSensorBeans : MonoBehaviour
{
    public input_Manager ipManager;
    public mainManager mainManager;
    public game_Manager gameManager;
    public ComboManager comboManager;
    public beanManager beanManager;

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
        if (collision.gameObject.layer == 11) //player
        { 
            if (bean) { }
            if (wallL) { comboManager.OpenBouncingOffWall(); }
            if (wallR) { comboManager.OpenBouncingOffWall(); }
            if (win)   { Debug.Log("win");   gameManager.Win(); }
            if (loose) { Debug.Log("loose"); gameManager.loose(); }
        }

        if (collision.gameObject.layer == 17) //playerhead
        {
            if (bean) { comboManager.OpenBeanBreaking(); beanManager.beanToBeDestroyed = gameObject.transform.parent.gameObject; } 
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (wallExit) { Debug.Log("off the wall"); ipManager.offTheWall(); }
    }
}
