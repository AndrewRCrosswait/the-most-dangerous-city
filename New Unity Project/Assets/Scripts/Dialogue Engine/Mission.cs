using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type { 
    Goal,Kill,Item
}

[Serializable]
public class Mission
{
    public int Exp, Money;
    Player p => GameObject.FindObjectOfType<Player>();
    public Vector3 t;
    public GameObject Goal;
    Type type;
    public string Name;

    public bool Complete() {
        if (p.transform.position==t) {
            Debug.Log("Good");
            return true;
        }
        return false;
    }

}
