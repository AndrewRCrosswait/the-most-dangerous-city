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
        public Text Content;
        public Text NameText;
        
        /// <summary>
        /// Set by the person of whome the player has interacted with.
        /// </summary>
        public Character talkingTo;

        /// <summary>
        /// What we are currently displaying.
        /// </summary>
        public Image Icon;
        int x=0;
       public void startconvo() {
            if (talkingTo != null)
            {
                Debug.Log(talkingTo.name);
                // timeStartedConversation = Time.time;
                if (x != talkingTo.Content.Length)
                {
                    Debug.Log("We have content to run");
                    //  NameText.text = talkingTo.gameObject.name;
                    Content.text = talkingTo.Content[x].Diologue;
                    Icon = talkingTo.Content[x].Icon;
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
            if (x != talkingTo.Content.Length-1)
            {
                x++;
                Content.text = talkingTo.Content[x].Diologue;
                Icon = talkingTo.Content[x].Icon;
            }
            else {
                x = 0;
                talkingTo = null;
                dialogueMenu.SetActive(false);
            }
        }

    }
}