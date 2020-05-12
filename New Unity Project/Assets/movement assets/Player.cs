﻿using UnityEngine;


public class Player : Entity
{
    public Animator Anime => gameObject.GetComponent<Animator>();
    public int money, exp, Lvl;
    public GameObject intro;


    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (!intro.activeSelf)
        {
            Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
            transform.position = transform.position + Movement * Speed * Time.deltaTime;
        }
    }

}
