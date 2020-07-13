﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject InfoPage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InfoPage.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            InfoPage.SetActive(false);
        }
    }
}