using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainButtonCanvas;
    public GameObject controlsCanvas;
   public void StartGame()
    {
        SceneManager.LoadScene("Test Scene");
    }

    public void ViewControls()
    {
        mainButtonCanvas.SetActive(false);
        controlsCanvas.SetActive(true);
    }

    public void ReturnToMenu()
    {
        mainButtonCanvas.SetActive(true);
        controlsCanvas.SetActive(false);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
