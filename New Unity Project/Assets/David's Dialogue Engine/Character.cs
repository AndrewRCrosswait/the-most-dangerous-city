using System.Collections;
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

public class Character : MonoBehaviour
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
            }
        }

        else
        {
            print("Currently talking");
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        DM.dialogueMenu.SetActive(false);
        //End();
    }
}
