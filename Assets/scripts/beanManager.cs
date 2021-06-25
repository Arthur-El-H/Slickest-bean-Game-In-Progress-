using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beanManager : MonoBehaviour
{
    public GameObject bean;
    public GameObject newBean;
    float waitTime = 4f;
    [SerializeField] input_Manager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        fillMap();
        StartCoroutine(spawnBeans());
    }

    IEnumerator spawnBeans()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            spawnBean();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float currentY = 6f; //first bean
    float deltaY;
    float currentX;
    float size;

    void fillMap()
    {
        while( currentY < 100f)
        {
            spawnBean(true);
        }
    }

    void spawnBean(bool mapFilling = false)
    {
        if (mapFilling)
        {
            currentX = UnityEngine.Random.Range(-4.15f, 4.15f);
            deltaY = UnityEngine.Random.Range(3.5f, 6f);
            currentY += deltaY;
            size = UnityEngine.Random.Range(.3f, 1f);
            newBean = Instantiate(bean, new Vector3(currentX, currentY, 0), Quaternion.identity);
            newBean.transform.localScale = new Vector3(size, size, 1);
            //newBean.GetComponent<playerSensorBeans>().ipManager = inputManager;
        }

        else
        {
            currentX = UnityEngine.Random.Range(-4.15f, 4.15f);
            size = UnityEngine.Random.Range(.3f, 1f);
            newBean = Instantiate(bean, new Vector3(currentX, 100, 0), Quaternion.identity);
            newBean.transform.localScale = new Vector3(size, size, 1);

        }
    }
}
