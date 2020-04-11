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
        public GameObject dialogueMenu, Options;

        /// <summary>
        /// The top text box.
        /// </summary>
        public Text CharacterATextObject;
        public Text NameText;
        
        /// <summary>
        /// Set by the person of whome the player has interacted with.
        /// </summary>
        public Character talkingTo;

        /// <summary>
        /// What we are currently displaying.
        /// </summary>
        string currentMessage;

        
        public void DisplayMessage(string chaAMessage, string[] chaBMessage, Character talkingTo)
        {
            NameText.text = talkingTo.gameObject.name;
            timeStartedConversation = Time.time;

            selectedReply = 0;

            currentMessage = chaAMessage;

            replies = chaBMessage;

            this.talkingTo = talkingTo;
        }

    }
}