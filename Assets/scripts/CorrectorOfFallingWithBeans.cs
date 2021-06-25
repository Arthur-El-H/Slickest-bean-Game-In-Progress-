using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectorOfFallingWithBeans : MonoBehaviour
{
    float fallSpeed = 1f;

    internal void correct()
    {
        transform.position = Vector2.MoveTowards((Vector2)transform.position, (Vector2)transform.position + Vector2.down, fallSpeed * Time.deltaTime);
    }
}
