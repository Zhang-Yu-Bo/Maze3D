using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        SceneManager.LoadScene("FinalScene");
    }
}
