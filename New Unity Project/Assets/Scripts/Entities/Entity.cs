using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int Health=1,Attack,WillPower;
    public float Speed;
    public string Name; 

    public bool Alive() {
        if (Health > 0) {
            return true;
        }
        else
        {
            Debug.Log("died");
            return false;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Name = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Alive())
        {
            Debug.Log("destroyyyed:" + gameObject.name);
            GameObject.Destroy(gameObject);
        }
    }

}
