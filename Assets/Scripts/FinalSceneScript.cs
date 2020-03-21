using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneScript : MonoBehaviour
{
    public AudioClip scream;
    public GameObject audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FindObjectOfType<DialogueTrigger>().TriggerDialogue();
        this.audioSource.GetComponent<AudioSource>().PlayOneShot(scream);
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
