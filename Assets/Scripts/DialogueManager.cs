using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public GameObject dialogueBox;

    private Queue<string> sentences = new Queue<string>();

    // Start is called before the first frame update
    void Start()
    {
        if (this.sentences == null)
            this.sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        this.dialogueBox.SetActive(true);
        this.nameText.text = dialogue.name;
        this.sentences.Clear();
        foreach (string sentence in dialogue.sentences)
            this.sentences.Enqueue(sentence);

        this.DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (this.sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = this.sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(this.TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        this.dialogueBox.SetActive(false);
    }
}
