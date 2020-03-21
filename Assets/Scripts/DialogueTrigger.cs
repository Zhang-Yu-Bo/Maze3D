using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool isTrigger = false;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(this.dialogue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!this.isTrigger)
        {
            this.TriggerDialogue();
            this.isTrigger = true;
        }
    }
}
