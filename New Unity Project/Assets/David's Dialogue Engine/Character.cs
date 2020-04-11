using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RpgEngine.Dialoug;


[Serializable]
public class Sentence{
    public string Diologue, Name;
    public Sprite Frame;
    [NonSerialized]public float FrameTime;

}

public class Character : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Talking == false)
                {
                    Talking = true;
                    StartConversation();
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
        Talking = false;
        End();
    }
}
