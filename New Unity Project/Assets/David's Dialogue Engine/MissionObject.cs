using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int temp = -1;
            Player p = collision.gameObject.GetComponent<Player>();
            foreach (Mission m in p.Missions)
            {
                    p.money += m.Money;
                    p.exp += m.Exp;
                    temp = p.Missions.IndexOf(m);
            }
            if (temp != -1) {
                GameObject.Destroy(gameObject);
                p.Missions.RemoveAt(temp);
            }
        }
    }
}
