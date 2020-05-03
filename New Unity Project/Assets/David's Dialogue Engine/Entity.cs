using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int Health,Attack,WillPower;
    public float Speed;
    public string Name;
   [SerializeField] public List<Mission> Missions = new List<Mission>();

    public bool Alive() {
        if (Health >0) {
            return true;
        }
        return false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Speed = Health+1;
        Attack = Health;
    }

}
