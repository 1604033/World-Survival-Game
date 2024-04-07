using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void start()
    {
         SceneManager.LoadScene("MainScene");
    }

    public void options()
    {
        
    }
    
    public void quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#endif
        Application.Quit();
    }
}
