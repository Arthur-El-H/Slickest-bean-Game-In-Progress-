using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Vector2 lastPos;
    Vector2 delta;

    private void Awake()
    {
        lastPos = transform.position;
    }
    void Update()
    {
        delta = (Vector2)transform.position - lastPos;
        Camera.main.transform.position += new Vector3(0, delta.y, 0);
        lastPos = (Vector2)transform.position;
    }
}
