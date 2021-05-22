using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bean : MonoBehaviour
{
    float fallSpeed = 1f;
    void Update()
    {
        transform.position = Vector2.MoveTowards((Vector2)transform.position, (Vector2)transform.position + Vector2.down, fallSpeed * Time.deltaTime);
    }
}
