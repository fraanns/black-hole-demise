using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestorePosOnRestart : MonoBehaviour
{
    Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;


        GameObject.FindGameObjectWithTag("GameController").GetComponent<InputReader>().restartEvent += OnRestart;
    }

    private void OnRestart()
    {
        transform.position = startPos;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
            rb.velocity = Vector2.zero;
    }
}
