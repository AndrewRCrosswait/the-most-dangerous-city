﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCMovement : MonoBehaviour
{
    public GameObject DialogueBox;
    public float Speed = 2;
    public float XDirection, YDirection;

    // Start is called before the first frame update
    void Start()
    {
        XDirection = range(-5,5);
        YDirection = range(-5,5);
    }

    // Update is called once per frame
    void Update()
    {
        if (!DialogueBox.activeSelf)
        {
            Debug.Log("Move");
            transform.Translate(XDirection * Time.deltaTime, YDirection * Time.deltaTime, 0);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        Speed *= -1;
        XDirection = range(-5, 5);
        YDirection = range(-5, 5);
    }
    public float range(float min, float max)
    {

        Random.InitState((int)System.DateTime.Now.Ticks * Random.Range(0,10));
        return Random.Range(min, max + 1);
    }
}
