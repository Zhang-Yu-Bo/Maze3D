using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{

    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }

    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void changeResultion(string type)
    {
        if (type == "type1")
            Screen.SetResolution(1600, 900, false);
        else if (type == "type2")
            Screen.SetResolution(1600, 1000, false);
        else if (type == "type3")
            Screen.SetResolution(1024, 768, false);
    }
}
