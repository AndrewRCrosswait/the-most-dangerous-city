using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


namespace RpgEngine.Dialoug
{
    public class DialogueManager : MonoBehaviour
    {
        /// <summary>
        /// The parent of the dialogue objects.
        /// </summary>
        public GameObject dialogueMenu;

        /// <summary>
        /// The top text box.
        /// </summary>
        public Text Content,Notification;
        public Text NameText;
        
        /// <summary>
        /// Set by the person of whome the player has interacted with.
        /// </summary>
        public Character talkingTo;

        /// <summary>
        /// What we are currently displaying.
        /// </summary>
        public Image Icon;
        int CurrentIndex=0;

       public void startconvo() {
            if (talkingTo != null)
            {
                Debug.Log(talkingTo.name);
                // timeStartedConversation = Time.time;
                if (CurrentIndex != talkingTo.Content.Length)
                {
                    Debug.Log("We have content to run");
                    NameText.text = talkingTo.gameObject.name;
                    Content.text = talkingTo.Content[CurrentIndex].Diologue;
                    Icon = talkingTo.Content[CurrentIndex].Icon;
                }
                else
                {
                    talkingTo = null;
                    Content.text = "";
                    dialogueMenu.SetActive(false);
                }
            }
        }
        public void Next() {
            if (CurrentIndex != talkingTo.Content.Length-1)
            {
                CurrentIndex++;
                Content.text = talkingTo.Content[CurrentIndex].Diologue;
                Icon = talkingTo.Content[CurrentIndex].Icon;
            }
            else {
                CurrentIndex = 0;
                talkingTo = null;
                dialogueMenu.SetActive(false);
            }
        }

    }
}