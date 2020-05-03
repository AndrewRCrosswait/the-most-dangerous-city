using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RpgEngine.Dialoug;


[Serializable]
public class Sentence{
    public string Diologue;
    [NonSerialized]public float FrameTime;
    public Image Icon;
}

public class Character : Entity
{
    public DialogueManager DM;
    [SerializeField] public Sentence[] Content;

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (DM.dialogueMenu.activeSelf == false)
                {
                    DM.talkingTo = gameObject.GetComponent<Character>();
                    DM.dialogueMenu.SetActive(true);
                    DM.startconvo();
                    print("works");
                }
                foreach (Mission m in Missions)
                {
                    Instantiate(m.Goal, m.t, Quaternion.identity);
                    collision.gameObject.GetComponent<Player>().Missions.Add(m);
                    DM.Notification.gameObject.SetActive(true);
                    DM.Notification.text = " Misson Added ";
                }
            }
        }

        else
        {
            print("Currently talking");
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        DM.Notification.gameObject.SetActive(false);
        DM.dialogueMenu.SetActive(false);
        //End();
    }
}
